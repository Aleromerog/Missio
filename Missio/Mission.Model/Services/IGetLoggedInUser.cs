using Mission.Model.Data;

namespace Mission.Model.Services
{
    public interface IGetLoggedInUser
    {
        User LoggedInUser { get; }
    }
}