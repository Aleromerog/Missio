using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Missio.LogIn;
using Missio.Navigation;
using Missio.Users;
using StringResources;

namespace Missio.LocalDatabase
{
    /// <summary>
    /// A fake user and password validator that checks the given parameters against the hardcoded data
    /// </summary>
    public class LocalUserRepository : IUserRepository
    {
        private readonly List<User> _validUsers = new List<User>();

        public LocalUserRepository()
        {
            using(var webClient = new WebClient())
            {
                var jorge = new User("Jorge Romero", "Yolo", webClient.DownloadData("https://scontent.felp1-1.fna.fbcdn.net/v/t1.0-9/26168930_10208309305130065_9014358028033259242_n.jpg?_nc_cat=0&oh=a6dc6203053aa3c830edffd107f346e4&oe=5BF1FC2B"));
                var greco = new User("Francisco Greco", "ElPass", webClient.DownloadData("https://scontent.felp1-1.fna.fbcdn.net/v/t1.0-9/18342049_1371435649562155_317149840395279012_n.jpg?_nc_cat=0&oh=74b6c0226537899a74f499c25b3ddb07&oe=5C00CF82"));
                _validUsers.Add(jorge);
                _validUsers.Add(greco);
            }
        }

    
       

        private bool DoesUserExist(string userName)
        {
            return _validUsers.Exists(x => x.UserName == userName);
        }

        /// <inheritdoc />
        public async Task AttemptToRegisterUser(User user)
        {
            var errors = user.GetOfflineErrors();
            if (errors.Count > 0)
                throw new UserRegistrationException(errors);
            if(DoesUserExist(user.UserName))
                throw new UserRegistrationException(new List<AlertTextMessage>
                {
                    new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok)
                });
            _validUsers.Add(user);
            await Task.CompletedTask;
        }

        public Task<User> GetUserIfValid(string userName, string password)
        {

            var userWithMatchingName = _validUsers.FirstOrDefault(x => x.UserName == userName);
            if (userWithMatchingName == null)
                throw new InvalidUserNameException();
            if (userWithMatchingName.Password != password)
                throw new InvalidPasswordException();
            return Task.FromResult(userWithMatchingName);
        }
    }
}