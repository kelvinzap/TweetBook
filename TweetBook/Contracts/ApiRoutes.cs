using System;

namespace TweetBook.Contracts
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;
        
        public static class Post
        {
            public const string Get = Base + "/posts";
            public const string Create = Base + "/Posts";
        }

    }
}