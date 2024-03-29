﻿using System;

namespace TweetBook.Contracts
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;
        
        public static class Posts
        {
            public const string GetAll = Base + "/posts";
            public const string Create = Base + "/Posts";
            public const string Get = Base + "/posts/{postId}";
            public const string Delete = Base + "/posts/{postId}";
            public const string Update = Base + "/posts/{postId}";
        }
        
        public static class Identity
        {
            public const string Register = Base + "/identity/register";
            public const string Login = Base + "/identity/login";
            public const string Refresh = Base + "/identity/refresh";
        }

        public static class Tags
        {
            public const string Create = Base + "/Tags";
            public const string GetAll = Base + "/Tags";
        }

    }
}