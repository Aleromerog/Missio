using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class ApplicationNavigation : IGoToView, IGoToPage, IReturnToPreviousPage
    {
        public ApplicationPages Pages;
        private Page CurrentPage { get; set; }

        public void StartFromPage(Page page)
        {
            CurrentPage = new NavigationPage(page);
            Application.Current.MainPage = CurrentPage;
        }

        /// <inheritdoc />
        public Task GoToPage(Page page)
        {
            return Application.Current.MainPage.Navigation.PushAsync(page);
        }

        /// <inheritdoc />
        public Task ReturnToPreviousPage()
        {
            return Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <inheritdoc />
        public Task GoToView(string viewTitle)
        {
            return GoToPage(Pages.AvailableViews.First(x => x.Title == viewTitle));
        }
    }
}