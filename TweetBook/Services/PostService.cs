using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _context;

        public PostService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<bool> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            var created = await _context.SaveChangesAsync();
            
            return created > 0;
        }
    }
}