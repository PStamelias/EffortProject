using EffortProject.Models;

namespace EffortProject.ApiServices
{
    public interface IJokeApiService
    {
        public Task<List<Joke>> GetDataFromApi();
    }
}
