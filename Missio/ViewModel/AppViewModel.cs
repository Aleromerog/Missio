using System;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace ViewModel
{
    public class AppViewModel 
    {
        public static Page[] AvailableViews { get; private set; }
        public Page CurrentPage { get; private set; }

        public AppViewModel([NotNull] Page[] availableViews)
        {
            AvailableViews = availableViews ?? throw new ArgumentNullException(nameof(availableViews));
        }

        public void StartFromPage(Page page)
        {
            CurrentPage = new NavigationPage(page);
            Application.Current.MainPage = CurrentPage;
        }
        
    }
}