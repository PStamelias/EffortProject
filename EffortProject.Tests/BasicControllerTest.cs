using EffortProject.Controllers;
using EffortProject.Models;
using EffortProject.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EffortProject.Tests
{
    public class BasicControllerTest
    {
        [Fact]
        public async Task FetchDataAsync_WithValidData_ReturnAggregatedResultAsync()
        {
            //Mock Services
            var mockPostService = new Mock<IPostService>();
            var mockJokeService = new Mock<IJokeService>();


            //fake Data
            var fakePosts = new List<Post> { new Post { title="title0", body="body0"} };
            var fakeJokes = new List<Joke> { new Joke { type = "type0", setup="setup0",punchline="punchline0" } };

            //call the functions of services 
            mockPostService.Setup(s => s.RetrievePostsAsync()).ReturnsAsync(fakePosts);
            mockJokeService.Setup(s => s.RetrieveJokesAsync()).ReturnsAsync(fakeJokes);

            
            var controller = new BasicController(mockPostService.Object,mockJokeService.Object);


            var result = await controller.FetchDataAsync();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedData = Assert.IsType<AggregatedData>(okResult.Value);
            Assert.Equal(fakeJokes,returnedData.JokeList);
            Assert.Equal(fakePosts,returnedData.PostList);

            mockPostService.Verify(s => s.RetrievePostsAsync(), Times.Once);
            mockJokeService.Verify(s => s.RetrieveJokesAsync(), Times.Once);

        }
    }
}
