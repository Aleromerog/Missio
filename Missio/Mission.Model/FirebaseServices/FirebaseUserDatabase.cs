using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Mission.Model.Data;
using Mission.Model.Services;

namespace Mission.Model.LocalServices
{
    public class ExternalDatabase : IDoesUserExist, IGetMostRecentPosts, INewsFeedPostsUpdater, IPublishPost, IRegisterUser, IValidateUser
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
        public void UpdatePosts(ObservableCollection<NewsFeedPost> posts)
        {
        }

        /// <inheritdoc />
        public void PublishPost(NewsFeedPost post)
        {
        }
    }

    public interface IGetChildQuery
    {
        //ChildQuery Child(string resourceName);
    }
}