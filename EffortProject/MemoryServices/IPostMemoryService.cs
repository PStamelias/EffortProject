using EffortProject.Models;

namespace EffortProject.MemoryServices
{
    public interface IPostMemoryService
    {
        public Task<List<Post>> GetPostsAsync();
    }
}
