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

        [HttpGet(ApiRoutes.Post.Get)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postService.GetAsync());
        }

        [HttpPost(ApiRoutes.Post.Create)]
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
            
            return Ok(new CreatePostResponse
            {
                Id = post.Id,
                Name = post.Name
            });
        }
    }
}