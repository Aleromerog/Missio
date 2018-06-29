using System.Threading.Tasks;

namespace Missio.Navigation
{
    public interface IGoToView
    {
        Task GoToView(string viewTitle);
    }
}