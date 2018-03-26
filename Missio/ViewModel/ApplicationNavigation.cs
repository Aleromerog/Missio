using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ViewModel
{
    public class ApplicationNavigation : IGoToView, IGoToPage, IReturnToPreviousPage
    {
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
            return GoToPage(AppViewModel.AvailableViews.First(x => x.Title == viewTitle));
        }
    }
}