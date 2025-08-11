using EffortProject.MemoryServices;
using EffortProject.Models;
using EffortProject.Services;
using Moq;

namespace EffortProject.Tests
{
    public class PostServiceTest
    {
        [Fact]
        public async Task RetrievePostAsync_WithValidData_ReturnPostsResultsAsync()
        {
            var mockPostMemoryService = new Mock<IPostMemoryService>();
            var fakePosts = new List<Post> { new Post { title = "title0", body = "body0" } };
            mockPostMemoryService.Setup(ap=>ap.GetPostsAsync()).ReturnsAsync(fakePosts);

            var controller = new PostService(mockPostMemoryService.Object);

            var result = await controller.RetrievePostsAsync();
            Assert.Equal(result, fakePosts);
            mockPostMemoryService.Verify(s=>s.GetPostsAsync(),Times.Once);
        }
    }
}
