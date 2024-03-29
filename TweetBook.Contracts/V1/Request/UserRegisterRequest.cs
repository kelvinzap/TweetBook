﻿using System.ComponentModel.DataAnnotations;

namespace TweetBook.Contracts.V1.Request
{
    public class UserRegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}