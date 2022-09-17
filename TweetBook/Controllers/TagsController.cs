using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Data;
using TweetBook.Domain;
using TweetBook.Extensions;
using TweetBook.Services;

namespace TweetBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IPostService _postService;

        public TagsController(IPostService postService, DataContext dataContext)
        {
            _postService = postService;
            _dataContext = dataContext;
        }

        [HttpPost(ApiRoutes.Tags.Create)]
        [ProducesResponseType(typeof(TagResponse), 201)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest tagRequest)
        {
            var tags = tagRequest.Tags.ToList();

            var badTags = tags.Where(string.IsNullOrWhiteSpace).ToList();
            badTags.ForEach(x =>
            {
                tags.Remove(x);
            });

           tags.ForEach(tagName =>
           {
               tagName = tagName.ToLower().Replace(" ", "");
               

               var tags = _dataContext.Tags.SingleOrDefault(i => i.Name == tagName && !string.IsNullOrWhiteSpace(i.Name));

               if (tags == null)
               {
                   _dataContext.Tags.Add(new Tag
                   {
                       Name = tagName,
                       CreatorId = HttpContext.GetUserById(),
                       CreatedOn = DateTime.UtcNow
                   });
               }
           });

            var created = await _dataContext.SaveChangesAsync();
            
            return created > 0 ? Ok(tags) : BadRequest();
        }

        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "WorksFromCompanyPolicy")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dataContext.Tags.ToListAsync());
        }
    }
}