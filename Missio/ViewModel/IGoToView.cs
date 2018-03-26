using System.Threading.Tasks;

namespace ViewModel
{
    public interface IGoToView
    {
        Task GoToView(string viewTitle);
    }
}