using EffortProject.Models;

namespace EffortProject.ApiServices
{
    public interface IPostApiService
    {
        public Task<List<Post>> GetDataFromApi();
    }
}
