using Mission.Model.Data;

namespace ViewModel
{
    public interface IGetLoggedInUser
    {
        User LoggedInUser { get; }
    }
}