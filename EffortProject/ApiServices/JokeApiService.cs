using EffortProject.Models;
using Newtonsoft.Json;

namespace EffortProject.ApiServices
{
    public class JokeApiService:IJokeApiService
    {
        private readonly string jokeUrl= "https://official-joke-api.appspot.com/jokes/ten";
        private readonly IHttpClientFactory _httpClientFactory;
        public JokeApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<Joke>> GetDataFromApi()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var result = await client.GetStringAsync(jokeUrl);
                var jokeList = JsonConvert.DeserializeObject<List<Joke>>(result);
                return jokeList;
            }
            catch (Exception ex)
            {
                var jokeList = new List<Joke>();
                for (int i = 0; i < 10; i++)
                {
                    var joke = new Joke() { punchline = "punchline" + i, setup = "setup" + i, type = "type" + i };
                    jokeList.Add(joke);

                }
                return jokeList;
            }
        }
    }
}
