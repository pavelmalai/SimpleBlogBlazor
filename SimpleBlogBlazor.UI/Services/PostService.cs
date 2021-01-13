using SimpleBlogBlazor.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleBlogBlazor.UI.Services
{
    public interface IPostsService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<Post> GetPost(Guid postId);
        Task<Post> CreatePost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(Guid postId);
    }

    public class PostsService : IPostsService
    {
        private readonly HttpClient _httpClient;
        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _httpClient.GetFromJsonAsync<Post[]>("api/Posts");

        }
        public async Task<Post> GetPost(Guid postId)
        {
            return await _httpClient.GetFromJsonAsync<Post>($"api/Posts/{postId}");
        }
        public async Task<Post> CreatePost(Post post)
        {
            try
            {
                post.PostId = Guid.NewGuid();
                post.CreatedOn = DateTime.Now;
                var postJson = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"api/Posts", postJson);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Post createdPost = JsonSerializer.Deserialize<Post>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return createdPost;
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }
        public async Task UpdatePost(Post post)
        {
            var postJson = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"api/Posts/{post.PostId}", postJson);
        }
        public async Task DeletePost(Guid postId)
        {
            await _httpClient.DeleteAsync($"api/Posts/{postId}");
        }
    }
}
