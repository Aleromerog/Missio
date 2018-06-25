using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public interface IGoToPage  
    {
        Task GoToPage(Page page);
    }
}