using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TweetBook.Extensions
{
    public static class GeneralExtension
    {
        public static string GetUserById(this HttpContext httpContext)
        {
            return httpContext.User == null? string.Empty : httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}