﻿using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class ApplicationNavigation : IGoToView, IGoToPage, IReturnToPreviousPage
    {
        public Page[] Pages;
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
        public Task GoToView<T>()
        {
            return GoToPage(Pages.First(x => x is T));
        }
    }
}