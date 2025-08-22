using System.ComponentModel;
using EffortProject.ApiServices;
using EffortProject.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EffortProject.MemoryServices
{
    public class JokeMemoryService:IJokeMemoryService
    {
        private readonly IJokeApiService _jokeApiService;
        private readonly IMemoryCache _memoryCache;

        public JokeMemoryService(IJokeApiService jokeApiService, IMemoryCache memoryCache)
        {
            _jokeApiService = jokeApiService;
            _memoryCache = memoryCache;
        }
        public async Task<List<Joke>> GetJokesAsync()
        {

            if (!_memoryCache.TryGetValue("Jokes", out List<Joke> jokeList))
            {
                var initialJokeList= await _jokeApiService.GetDataFromApi();
                _memoryCache.Set("Jokes", initialJokeList, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
                });
                return initialJokeList;
            }
            else
            {
                return jokeList;
            }
        }
    }
}
