using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Request;
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
        public async Task<IActionResult> Create([FromBody] CreateTagRequest tagRequest)
        {
            if (tagRequest.Tags == null)
            {
                return BadRequest();
            }

            var tags = tagRequest.Tags;
            
            foreach (var tagName in tags)
            {
                var tag = new Tag
                {
                    Name = tagName,
                    CreatedOn = DateTime.UtcNow,
                    CreatorId = HttpContext.GetUserById()
                };
                _dataContext.Tags.Add(tag);
            }

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