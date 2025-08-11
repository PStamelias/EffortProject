using EffortProject.Models;

namespace EffortProject.MemoryServices
{
    public interface IJokeMemoryService
    {
        public  Task<List<Joke>> GetJokesAsync();
    }
}
