using System.Collections;
using System.Collections.Generic;

namespace TweetBook.Contracts.V1.Response
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}