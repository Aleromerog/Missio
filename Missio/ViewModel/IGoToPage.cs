using System.Threading.Tasks;
using Xamarin.Forms;

namespace ViewModel
{
    public interface IGoToPage  
    {
        Task GoToPage(Page page);
    }
}