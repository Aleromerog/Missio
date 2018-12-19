using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public interface INavigation
    {
        Task<T> GoToPage<T>() where T : Page;
        Task<TViewModel> GoToPage<T, TViewModel>() where T : Page;
        Task GoToPage(Page page);
        Task ReturnToPreviousPage();
        Task DisplayAlert(string title, string message, string acceptMessage);
    }
}