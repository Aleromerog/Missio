using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public interface IGoToView
    {
        Task GoToView<T>() where T : Page;
    }
}