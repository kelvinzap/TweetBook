﻿using System.Collections;
using System.Collections.Generic;

namespace TweetBook.Domain
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}