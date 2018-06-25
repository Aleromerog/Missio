using System.Threading.Tasks;

namespace Missio.Navigation
{
    public interface IDisplayAlertOnCurrentPage
    {
        Task DisplayAlert(string title, string message, string acceptMessage);
        Task DisplayAlert(AlertTextMessage alertContents);
    }
}