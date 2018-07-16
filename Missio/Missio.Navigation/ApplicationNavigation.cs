using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class ApplicationNavigation : INavigation
    {
        private readonly IPageFactory _pageFactory;

        public ApplicationNavigation(IPageFactory pageFactory)
        {
            _pageFactory = pageFactory;
        }

        /// <inheritdoc />
        public Task GoToPage<T>() where T : Page
        {
            return GoToPage(_pageFactory.MakePage<T>());
        }

        /// <inheritdoc />
        public Task GoToPage(Page page)
        {
            if (Application.Current.MainPage != null)
                return Application.Current.MainPage.Navigation.PushAsync(page);
            Application.Current.MainPage = page;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task ReturnToPreviousPage()
        {
            return Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}