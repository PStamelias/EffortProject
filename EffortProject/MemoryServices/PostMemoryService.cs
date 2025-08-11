using EffortProject.ApiServices;
using EffortProject.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EffortProject.MemoryServices
{
    public class PostMemoryService:IPostMemoryService
    {
        private readonly IPostApiService _postApiService;
        private readonly IMemoryCache _memoryCache;
        public PostMemoryService(IPostApiService postApiService,IMemoryCache memoryCache)
        {
            _postApiService = postApiService;
            _memoryCache = memoryCache;
        }
        public async Task<List<Post>> GetPostsAsync()
        {
            if(!_memoryCache.TryGetValue("Posts",out List<Post> postsLists))
            {
                var initialPostsList = await _postApiService.GetDataFromApi();
                _memoryCache.Set("Posts", initialPostsList, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
                return initialPostsList;
            }
            else
            {
                return postsLists;
            }
        }
    }
}
