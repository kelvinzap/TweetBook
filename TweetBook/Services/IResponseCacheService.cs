﻿using System;
using System.Threading.Tasks;

namespace TweetBook.Services;

public interface IResponseCacheService
{
    Task CacheResponseAsync(string cacheKey, object Response, TimeSpan timeToLive);
    Task<string> GetCachedResponseAsync(string cacheKey);
}