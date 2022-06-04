using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TweetBook.Contracts.V1.Request;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}