using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EffortProject.MemoryServices;
using EffortProject.Models;
using EffortProject.Services;
using Moq;

namespace EffortProject.Tests
{
    public class JokeServiceTest
    {
        [Fact]
        public async Task RetrieveJokesAsync_WithValidData_ReturnJokesResultAsync()
        {
            var mockJokeMemoryService = new Mock<IJokeMemoryService>();
            var fakeJokes = new List<Joke> { new Joke { type = "type0", setup = "setup0", punchline = "punchline0" } };
            mockJokeMemoryService.Setup(s => s.GetJokesAsync()).ReturnsAsync(fakeJokes);

            var controller = new JokeService(mockJokeMemoryService.Object);

            var result= await controller.RetrieveJokesAsync();
            Assert.Equal(fakeJokes, result);
            mockJokeMemoryService.Verify(s => s.GetJokesAsync(), Times.Once);

        }
    }
}
