using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using TweetBook.Domain;

namespace TweetBook.Contracts.V1.Response
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public virtual List<TagResponse> Tags { get; set; }
    }

  
}