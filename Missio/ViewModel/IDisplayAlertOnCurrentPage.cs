using System.Threading.Tasks;

namespace ViewModel
{
    public interface IDisplayAlertOnCurrentPage
    {
        Task DisplayAlert(string title, string message, string acceptMessage);
    }
}