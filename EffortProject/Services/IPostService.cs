using EffortProject.Models;

namespace EffortProject.Services
{
    public interface IPostService
    {
        public  Task<List<Post>> RetrievePostsAsync();
    }
}
