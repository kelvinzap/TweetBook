using Microsoft.AspNetCore.Mvc;
using TweetBook.Filter;

namespace TweetBook.Controllers;

public class SecretController : Controller
{
    [HttpGet("secret")]
    [ApiKeyAuth]
    public IActionResult GetSecret()
    {
        return Ok("I have No Secrets");
    }
}