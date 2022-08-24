using System.Collections;
using System.Collections.Generic;

namespace TweetBook.Contracts.V1.Request
{
    public class CreateTagRequest
    {
        public IEnumerable<string> Tags { get; set; }
    }
}