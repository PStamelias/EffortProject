using EffortProject.Models;
using Newtonsoft.Json;

namespace EffortProject.ApiServices
{
    public class PostApiService:IPostApiService
    {
        private readonly string postUrl= "https://jsonplaceholder.typicode.com/posts";
        private readonly IHttpClientFactory _httpClientFactory;
        public PostApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<Post>> GetDataFromApi()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var result = await client.GetStringAsync(postUrl);
                var postsList = JsonConvert.DeserializeObject<List<Post>>(result);
                return postsList;
            }
            catch (Exception ex)
            {
                var postList = new List<Post>();
                for (int i = 0; i < 20; i++)
                {
                    var post = new Post() { body = "body" + i, title = "title" + i, userId = i };
                    postList.Add(post);
                }
                return postList;
            }
        }
    }
}
