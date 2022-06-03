using System;

namespace TweetBook.Contracts.V1.Response
{
    public class CreatePostResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}