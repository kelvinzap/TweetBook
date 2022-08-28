using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;
using TweetBook.Data.Migrations;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
        private readonly DataContext _dataContext;

        public PostService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<bool> CreateAsync(Post post)
        { 
            var badTags = post.Tags.Where(x => string.IsNullOrWhiteSpace(x.TagName)).ToList();
            badTags.ForEach(x =>
            {
                post.Tags.Remove(x);
            });

            post.Tags?.ForEach(x =>
            {
                x.TagName = x.TagName.ToLower();
                x.TagName = x.TagName.Replace(" ", "");
            });
            
            
            CreateNewTag(post);
            
            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();
            
            return created > 0;
        }

        private void CreateNewTag(Post post)
        {
            post.Tags.ForEach(x =>
            {
                var tags = _dataContext.Tags.SingleOrDefault(i => i.Name == x.TagName && !string.IsNullOrWhiteSpace(i.Name));

                if (tags == null)
                {
                    _dataContext.Tags.Add(new Tag
                    {
                        Name = x.TagName,
                        CreatorId = post.UserId,
                        CreatedOn = DateTime.UtcNow
                    });
                }
            });
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> DeletePostAsync(Post post)
        {
            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> UpdatePostAsync(Post post)
        {
            _dataContext.Posts.Update(post);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> UserOwnsPost(Guid postId, string userId)
        {
            var post = await _dataContext.Posts.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);
            
            if (post == null)
            {
                return false;
            }

            return post.UserId == userId;


        }
    }
}