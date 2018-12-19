using Xamarin.Forms;

namespace Missio.Navigation
{
    public interface IPageFactory
    {
        T MakePage<T>() where T : Page;
    }
}