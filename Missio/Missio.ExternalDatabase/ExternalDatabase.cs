namespace Missio.ExternalDatabase
{
    //public class UserExternalDatabase : IDoesUserExist, IRegisterUser, IValidateUser
    //{
    //    private readonly MobileServiceClient _service;

    //    public UserExternalDatabase(MobileServiceClient service)
    //    {
    //        _service = service;
    //    }

    //    /// <inheritdoc />
    //    public bool DoesUserExist(string userName)
    //    {
    //    }

    //    /// <inheritdoc />
    //    public void RegisterUser(RegistrationInfo registrationInfo)
    //    {
    //    }

    //    /// <inheritdoc />
    //    public void ValidateUser(User user)
    //    {
    //    }
    //}

    //public class GenericPostAzureDatabase<T>
    //{
    //    private readonly IMobileServiceClient _service;

    //    public GenericPostAzureDatabase(IMobileServiceClient service)
    //    {
    //        _service = service;
    //    }

    //    public Task<List<IPost>> GetPosts()
    //    {
    //        return _service.GetTable<T>().ToListAsync());
    //    }
    //}

    //public class PostAzureDatabase : IGetMostRecentPosts, IPublishPost
    //{
    //    public PostAzureDatabase(List<GenericPostAzureDatabase>)
    //    {
    //    }

    //    /// <inheritdoc />
    //    public List<IPost> GetMostRecentPostsInOrder()
    //    {
    //    }

    //    /// <inheritdoc />
    //    public void PublishPost(IPost post)
    //    {
    //    }
    //}
}