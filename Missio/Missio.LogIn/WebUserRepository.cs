using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Missio.Registration;
using Missio.Users;

namespace Missio.LogIn
{
    public class WebUserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public WebUserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public async Task AttemptToRegisterUser(CreateUserDTO createUserDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", createUserDTO);
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new UserRegistrationException(await response.Content.ReadAsAsync<List<string>>());
        }

        public async Task ValidateUser(string userName, string password)
        {
            var response = await _httpClient.GetAsync($@"api/users/{userName}/{password}");
            if (response.StatusCode == HttpStatusCode.OK)
                return;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new LogInException(await response.Content.ReadAsAsync<string>());
            throw new HttpRequestException(response.StatusCode + " " + response.ReasonPhrase);
        }
    }
}