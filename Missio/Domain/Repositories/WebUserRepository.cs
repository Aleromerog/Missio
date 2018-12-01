using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.DataTransferObjects;
using Domain.Exceptions;

namespace Domain.Repositories
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

        public async Task ValidateUser(NameAndPassword nameAndPassword)
        {
            var response = await _httpClient.GetAsync($@"api/users/{nameAndPassword.UserName}/{nameAndPassword.Password}");
            if (response.StatusCode == HttpStatusCode.OK)
                return;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new LogInException(await response.Content.ReadAsAsync<string>());
            throw new HttpRequestException(response.StatusCode + " " + response.ReasonPhrase);
        }
    }
}