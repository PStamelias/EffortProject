using EffortProject.ApiServices;
using EffortProject.MemoryServices;
using EffortProject.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffortProject.Tests
{
    public class PostMemoryServiceTest
    {
        [Fact]
        public async Task Should_ReturnFromCache_WhenCacheIsEmpty()
        {
            var mockPostApiService = new Mock<IPostApiService>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();

            object cacheValue = null;
            mockMemoryCache
                .Setup(mc => mc.TryGetValue(It.IsAny<object>(), out cacheValue))
                .Returns(false);


            mockMemoryCache
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Returns(mockCacheEntry.Object);

            var fakePosts = new List<Post> { new Post { title = "title0", body = "body0" } };
            mockPostApiService.Setup(x => x.GetDataFromApi()).ReturnsAsync(fakePosts);
            var service = new PostMemoryService(mockPostApiService.Object, mockMemoryCache.Object);
            var result = await service.GetPostsAsync();
            Assert.NotNull(result);
            mockPostApiService.Verify(x => x.GetDataFromApi(), Times.Once);
        }

        [Fact]
        public async Task Should_ReturnFromCache_WhenCacheIsNotEmpty()
        {
            var mockPostApiService = new Mock<IPostApiService>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            var fakePosts = new List<Post> { new Post { title = "title0", body = "body0" } };
            object outValue = fakePosts;


            mockMemoryCache
                .Setup(mc => mc.TryGetValue(It.IsAny<object>(), out outValue))
                .Returns(true);
            var service = new PostMemoryService(mockPostApiService.Object, mockMemoryCache.Object);
            var result = await service.GetPostsAsync();
            Assert.NotNull(result);
            mockPostApiService.Verify(x => x.GetDataFromApi(), Times.Never);
        }
    }
}
