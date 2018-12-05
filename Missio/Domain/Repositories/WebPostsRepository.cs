using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.DataTransferObjects;

namespace Domain.Repositories
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
        public async Task<IOrderedEnumerable<IPost>> GetMostRecentPostsInOrder(NameAndPassword nameAndPassword)
        {
            var allPosts = new List<IPost>();
            var response = await _httpClient.GetAsync($@"api/posts/getFriendsPosts/{nameAndPassword.UserName}/{nameAndPassword.Password}");
            if (response.StatusCode == HttpStatusCode.OK) //TODO: Display error when status is not ok!
            {
                var posts = await response.Content.ReadAsAsync<List<Post>>();
                allPosts.AddRange(posts.OrderByDescending(x => x.PublishedDate));
            }
            response = await _httpClient.GetAsync($@"api/posts/getStickyPosts");
            if (response.StatusCode == HttpStatusCode.OK)
                allPosts.AddRange(await response.Content.ReadAsAsync<List<StickyPost>>());
            return allPosts.OrderByDescending(x => x.GetPostPriority());
        }
    }
}