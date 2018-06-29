using System;
using System.Collections.Generic;
using Missio.NewsFeed;
using Missio.PostPublication;
using Missio.Registration;
using Missio.Users;

namespace Missio.ExternalDatabase
{
    public class ExternalDatabase : IDoesUserExist, IGetMostRecentPosts, IPublishPost, IRegisterUser, IValidateUser
    {
        private readonly IGetChildQuery _getChildQuery;

        public ExternalDatabase(IGetChildQuery getChildQuery)
        {
            _getChildQuery = getChildQuery;
        }

        public void ValidateUser(User user)
        {
        }

        public bool DoesUserExist(string userName)
        {
            throw new InvalidOperationException();
        }

        public void RegisterUser(RegistrationInfo registrationInfo)
        {
        }

        /// <inheritdoc />
        public List<NewsFeedPost> GetMostRecentPostsInOrder()
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public void PublishPost(NewsFeedPost post)
        {
        }
    }
}