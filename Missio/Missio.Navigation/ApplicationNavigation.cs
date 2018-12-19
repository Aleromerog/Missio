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
        public async Task<T> GoToPage<T>() where T : Page
        {
            var page = _pageFactory.MakePage<T>();
            await GoToPage(page);
            return page;
        }

        /// <inheritdoc />
        public async Task<TViewModel> GoToPage<T, TViewModel>() where T : Page
        {
            var page = await GoToPage<T>();
            return (TViewModel) page.BindingContext;
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