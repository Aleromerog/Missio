using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class ApplicationNavigation : INavigation
    {
        private readonly IPageFactory _pageFactory;
        private Page _currentPage;

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
            if (_currentPage != null)
                return _currentPage.Navigation.PushAsync(page);
            _currentPage = new NavigationPage(page);
            Application.Current.MainPage = _currentPage;
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task ReturnToPreviousPage()
        {
            _currentPage = await _currentPage.Navigation.PopAsync();
        }

        /// <inheritdoc />
        public Task DisplayAlert(string title, string message, string acceptMessage)
        {
            return _currentPage.DisplayAlert(title, message, acceptMessage);
        }
    }
}