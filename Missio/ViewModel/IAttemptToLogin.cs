using System.Threading.Tasks;
using Mission.Model.Data;

namespace ViewModel
{
    public interface IAttemptToLogin
    {
        Task AttemptToLoginWithUser(User user);
    }
}