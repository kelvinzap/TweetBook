﻿using System.Collections;
using System.Collections.Generic;

namespace TweetBook.Contracts.V1.Request
{
    public class CreatePostRequest
    {
        public string Name { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}