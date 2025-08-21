using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using EffortProject.Models;
using EffortProject.Services;

namespace EffortProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly IJokeService _jokeService;
        private readonly IPostService _postService;
        public BasicController(IPostService postService,IJokeService jokeService)
        {
            _jokeService=jokeService;
            _postService=postService;
        }
        [Route("FetchData")]
        [HttpGet]
        public async Task<IActionResult> FetchDataAsync()
        {
            var postTask  =   _postService.RetrievePostsAsync();
            var jokesTask =  _jokeService.RetrieveJokesAsync();
            await Task.WhenAll(postTask, jokesTask);
            var postsList = await postTask;
            var jokesList = await jokesTask;
            var aggregateData = new AggregatedData { JokeList=jokesList,PostList=postsList};
            return Ok(aggregateData);
        }
    }
}
