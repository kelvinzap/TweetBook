using System.Collections.Generic;

namespace TweetBook.Contracts.V1.Response;

public class ErrorResponse
{
    public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
}