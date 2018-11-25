using System.Threading.Tasks;
using Missio.Users;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public interface INavigation
    {
        Task GoToPage<T>() where T : Page;
        Task GoToPage<T>(NameAndPassword nameAndPassword) where T : Page;
        Task GoToPage(Page page);
        Task ReturnToPreviousPage();
        Task DisplayAlert(string title, string message, string acceptMessage);
        Task DisplayAlert(AlertTextMessage alertContents);
    }
}