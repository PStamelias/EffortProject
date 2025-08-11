using System.ComponentModel;
using EffortProject.MemoryServices;
using EffortProject.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace EffortProject.Services
{
    public class JokeService:IJokeService
    {
        private readonly IJokeMemoryService _jokeMemoryService;
        public JokeService(IJokeMemoryService  jokeMemoryService) 
        {
           _jokeMemoryService = jokeMemoryService;
        }
        public async Task<List<Joke>> RetrieveJokesAsync()
        {
            return await _jokeMemoryService.GetJokesAsync();
        }
    
    }
}
