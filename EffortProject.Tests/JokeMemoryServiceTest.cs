using EffortProject.ApiServices;
using EffortProject.MemoryServices;
using EffortProject.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace EffortProject.Tests
{
    public class JokeMemoryServiceTest
    {
        [Fact]
        public async Task Should_ReturnFromCache_WhenCacheIsEmpty()
        {
            var mockJokeApiService = new Mock<IJokeApiService>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();
            object cacheValue = null;
            mockMemoryCache
                .Setup(mc => mc.TryGetValue(It.IsAny<object>(), out cacheValue))
                .Returns(false);

            
            mockMemoryCache
                .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                .Returns(mockCacheEntry.Object);
            var fakeJokes = new List<Joke> { new Joke { type = "type0", setup = "setup0", punchline = "punchline0" } };
            mockJokeApiService.Setup(x => x.GetDataFromApi()).ReturnsAsync(fakeJokes);
            var service = new JokeMemoryService(mockJokeApiService.Object, mockMemoryCache.Object);
            var result = await service.GetJokesAsync();
            Assert.NotNull(result);
            mockJokeApiService.Verify(x=>x.GetDataFromApi(),Times.Once);


        }

        [Fact]
        public async Task Should_ReturnFromCache_WhenCacheIsNotEmpty()
        {
            var mockJokeApiService = new Mock<IJokeApiService>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            var fakeJokes = new List<Joke> { new Joke { type = "type0", setup = "setup0", punchline = "punchline0" } };
            object outValue = fakeJokes;


            mockMemoryCache
                .Setup(mc => mc.TryGetValue(It.IsAny<object>(), out outValue))
                .Returns(true);
            var service = new JokeMemoryService(mockJokeApiService.Object, mockMemoryCache.Object);
            var result = await service.GetJokesAsync();
            Assert.NotNull(result);
            mockJokeApiService.Verify(x => x.GetDataFromApi(), Times.Never);

        }
    }
}
