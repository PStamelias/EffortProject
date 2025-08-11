using EffortProject.Models;

namespace EffortProject.Services
{
    public interface IJokeService
    {
        public Task<List<Joke>> RetrieveJokesAsync();
        
    }
}
