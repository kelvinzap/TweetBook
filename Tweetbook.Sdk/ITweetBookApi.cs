using Refit;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;

namespace Tweetbook.Sdk;

[Headers("Authorization : Bearer")]
public interface ITweetBookApi
{
     [Get("/api/v1/posts")]
     Task<ApiResponse<List<PostResponse>>> GetAllPostAsync();
     
     [Get("/api/v1/posts/{postId}")]
     Task<ApiResponse<PostResponse>> GetPostAsync(Guid postId);

     [Post("/api/v1/posts")]
     Task<ApiResponse<PostResponse>> CreatePostAsync([Body] CreatePostRequest createPostRequest);
     
     [Put("/api/v1/posts/{postId}")]
     Task<ApiResponse<PostResponse>> UpdatePostAsync(Guid postId, [Body] UpdatePostRequest updatePostRequest);
     
     [Delete("/api/v1/posts/{postId}")]
     Task<ApiResponse<PostResponse>> DeletePostAsync(Guid postId);
}