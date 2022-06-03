using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<bool> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            var created = await _context.SaveChangesAsync();
            
            return created > 0;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }
    }
}