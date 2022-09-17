using Refit;
using TweetBook.Contracts.V1;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;

namespace Tweetbook.Sdk;

public interface IIdentityApi
{
    [Post("/api/v1/identity/register")]
    Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegisterRequest userRegisterRequest);
  
    [Post("/api/v1/identity/login")]
    Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest userLoginRequest);

    [Post("/api/v1/identity/refresh")]
    Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshTokenRequest);
}