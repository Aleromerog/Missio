namespace Mission.Model.LocalProviders
{
    public interface IRegisterUser
    {
        void RegisterUser(string userName, string password, string email);
    }
}