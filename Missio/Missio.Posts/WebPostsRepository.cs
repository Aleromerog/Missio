using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Missio.Posts
{
    public class WebPostsRepository : IPostRepository
    {
        private readonly HttpClient _httpClient;

        public WebPostsRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        //TODO: Display message if post fails
        /// <inheritdoc />
        public async Task PublishPost(CreatePostDTO post)
        {
            await _httpClient.PostAsJsonAsync("api/posts", post);
        }

        /// <inheritdoc />
        public async Task<List<IPost>> GetMostRecentPostsInOrder(string userName, string password)
        {
            var allPosts = new List<IPost>();
            var response = await _httpClient.GetAsync($@"api/posts/getFriendsPosts/{userName}/{password}");
            if (response.StatusCode == HttpStatusCode.OK)
                allPosts.AddRange(await response.Content.ReadAsAsync<List<Post>>());
            response = await _httpClient.GetAsync($@"api/posts/getStickyPosts");
            if (response.StatusCode == HttpStatusCode.OK)
                allPosts.AddRange(await response.Content.ReadAsAsync<List<StickyPost>>());
            return allPosts;
        }
    }
}