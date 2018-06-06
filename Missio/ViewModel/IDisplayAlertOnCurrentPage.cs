using System.Threading.Tasks;
using Mission.Model.Services;

namespace ViewModel
{
    public interface IDisplayAlertOnCurrentPage
    {
        Task DisplayAlert(string title, string message, string acceptMessage);
        Task DisplayAlert(AlertTextMessage alertContents);
    }
}