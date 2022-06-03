using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<bool> CreateAsync(Post post);
        Task<Post> GetPostByIdAsync(Guid postId);
        Task<bool> DeletePostAsync(Post post);

        Task<bool> UpdatePostAsync(Post post);
    }
}