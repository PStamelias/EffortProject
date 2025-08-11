using EffortProject.MemoryServices;
using EffortProject.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace EffortProject.Services
{
    public class PostService: IPostService
    {
        private readonly IPostMemoryService _postMemoryService;

        public PostService(IPostMemoryService postMemoryService) 
        {
            _postMemoryService = postMemoryService;
        }
        public async Task<List<Post>> RetrievePostsAsync()
        {
            return await _postMemoryService.GetPostsAsync();
        }

      
    }
}
