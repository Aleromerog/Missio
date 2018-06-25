using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class ApplicationNavigation : IGoToView, IGoToPage, IReturnToPreviousPage
    {
        private static Page[] AvailableViews { get; set; }
        private Page CurrentPage { get; set; }

        public ApplicationNavigation([NotNull] Page[] availableViews)
        {
            AvailableViews = availableViews ?? throw new ArgumentNullException(nameof(availableViews));
        }

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
            return GoToPage(AvailableViews.First(x => x.Title == viewTitle));
        }
    }
}