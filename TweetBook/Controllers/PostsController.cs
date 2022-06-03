using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Contracts;
using TweetBook.Data;

namespace TweetBook.Controllers
{
    public class PostsController : Controller
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(ApiRoutes.Post.Get)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Posts.ToListAsync());
        }
    }
}