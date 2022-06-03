using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAsync();
        Task<bool> CreateAsync(Post post);


    }
}