using System.Collections.Generic;
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

        /// <inheritdoc />
        public async Task ValidateUser(User user)
        {
            var response = await _httpClient.GetAsync($"api/users/name={user.UserName}&password={user.Password}");
            response.EnsureSuccessStatusCode();
            var status = await response.Content.ReadAsAsync<LogInStatus>();
            switch (status)
            {
                case LogInStatus.InvalidPassword:
                    throw new InvalidPasswordException();
                case LogInStatus.InvalidUserName:
                    throw new InvalidUserNameException();
            }
        }
    }
}