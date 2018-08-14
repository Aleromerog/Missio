using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Missio.LocalDatabase;
using Missio.Users;

namespace Missio.ExternalDatabase
{
    public class UserExternalDatabase : IUserRepository
    {
        private readonly IMobileServiceClient _service;

        public UserExternalDatabase(IMobileServiceClient service)
        {
            _service = service;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _service.GetTable<User>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task<User> GetUserByName(string userName)
        {
            var users = await _service.GetTable<User>().Where(x => x.UserName == userName).ToListAsync();
            return users[0];
        }

        /// <inheritdoc />
        public void AttemptToRegisterUser(User user)
        {
            _service.GetTable<User>().InsertAsync(user);
        }

        /// <inheritdoc />
        public void ValidateUser(User user)
        {
        }
    }

    //public class PostAzureDatabase : IPostRepository 
    //{
    //    private IMobileServiceClient _mobileServiceClient;

    //    public PostAzureDatabase(IMobileServiceClient mobileServiceClient)
    //    {
    //        _mobileServiceClient = mobileServiceClient;
    //    }

    //    /// <inheritdoc />
    //    public void PublishPost(IPost post)
    //    {
    //    }

    //    /// <inheritdoc />
    //    public void PublishPost(User user, IPost post)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    /// <inheritdoc />
    //    public List<IPost> GetMostRecentPostsInOrder(User user)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}