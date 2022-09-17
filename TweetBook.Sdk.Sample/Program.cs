// See https://aka.ms/new-console-template for more information

using Refit;
using TweetBook.Contracts.V1.Request;
using Tweetbook.Sdk;


var cachedToken = string.Empty;
var identityApi = RestService.For<IIdentityApi>("https://localhost:5001");
var tweetBookApi = RestService.For<ITweetBookApi>("https://localhost:5001", new RefitSettings
{
    AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
});
var registerResponse = await identityApi.RegisterAsync(new UserRegisterRequest
{
Email = "sdkmail@gmail.com",
Password = "Password1@"
});

var loginResponse = await identityApi.LoginAsync(new UserLoginRequest
{
Email = "test@gmail.com",
Password = "Password1@"
});

cachedToken = loginResponse.Content.Token;

var getAllResponse = await tweetBookApi.GetAllPostAsync();
var createPostResponse = await tweetBookApi.CreatePostAsync(new CreatePostRequest
{
    Name = "New post from SDK",
    Tags = new[] { "SDK tag" }
});
var retrievedPostResponse = await tweetBookApi.GetPostAsync(createPostResponse.Content.Id);
var updatePostResponse = await tweetBookApi.UpdatePostAsync(createPostResponse.Content.Id, new UpdatePostRequest
{
    Name = "Updated post from SDK"
});
var deletePostResponse = await tweetBookApi.DeletePostAsync(createPostResponse.Content.Id);
