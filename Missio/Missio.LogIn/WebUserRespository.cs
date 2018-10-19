using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.Navigation;
using Missio.Users;
using StringResources;

namespace Missio.ExternalDatabase
{
    public class WebUserRespository : IUserRepository
    {
        private readonly HttpClient _httpClient;

        public WebUserRespository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <inheritdoc />
        public async Task AttemptToRegisterUser(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/users", user);
            response.EnsureSuccessStatusCode();
            var status = await response.Content.ReadAsAsync<RegistrationStatus>();
            if (status == RegistrationStatus.UserNameAlreadyInUse)
                throw new UserRegistrationException(new List<AlertTextMessage>
                {
                    new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle,
                        AppResources.UserNameAlreadyInUseMessage, AppResources.Ok)
                });
        }

        public async Task<User> GetUserIfValid(string userName, string password)
        {
            var response = await _httpClient.GetAsync($"api/users/name={userName}&password={password}");
            if(response.StatusCode == HttpStatusCode.OK)
                return await response.Content.ReadAsAsync<User>();
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new LogInException(response.ReasonPhrase);
            throw new HttpRequestException(response.StatusCode + " " + response.ReasonPhrase);
        }
    }
}