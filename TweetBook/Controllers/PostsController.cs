using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Data;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(DataContext context, IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetAllAsync());
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new {error = "Invalid Request" });
            
            var post = new Post
            {
                Name = request.Name
            };

            var created = await _postService.CreateAsync(post);
            
            if (!created)
                return BadRequest(new {error = "Invalid Request" });

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.GetAll.Replace("{postId}", post.Id.ToString());
            
            var response = new CreatePostResponse
            {
                Id = post.Id,
                Name = post.Name
            };
            
            return Created(locationUri, response);
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            
            return (post == null) ? NotFound() : Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);

            if (post == null)
                return NotFound();
            
            var deleted = await _postService.DeletePostAsync(post);

            return (deleted) ? NoContent() : NotFound();
        }
    }
    
}