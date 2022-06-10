using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using TweetBook.Contracts;
using TweetBook.Contracts.V1.Request;
using TweetBook.Domain;
using Xunit;

namespace TweetBookIntegrationTest
{
    public class PostControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPost_ReturnsEmptyResponse()
        {
            //Arrange
            await AuthenticateAsync();
            
            //Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
         //Arrange 
         await AuthenticateAsync();
         var createdResponse = await CreatePostAsync(new CreatePostRequest { Name = "Test Name" });

         //Act
         var response =
             await TestClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", createdResponse.Id.ToString()));
         

         //Assert
         response.StatusCode.Should().Be(HttpStatusCode.OK);
         var returnedPost = await response.Content.ReadAsAsync<Post>();
         returnedPost.Id.Should().Be(createdResponse.Id);
         returnedPost.Name.Should().Be("Test Name");
        }
    }
}