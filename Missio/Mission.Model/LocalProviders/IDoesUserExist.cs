namespace Mission.Model.LocalProviders
{
    public interface IDoesUserExist
    {
        bool DoesUserExist(string userName);
    }
}