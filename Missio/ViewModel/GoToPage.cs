using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace ViewModel
{
    public class GoToPage : IGoToNextPage
    {
        private readonly Page _page;

        public GoToPage([NotNull] Page page)
        {
            _page = page ?? throw new ArgumentNullException(nameof(page));
        }

        /// <inheritdoc />
        public Task GoToNextPage()
        {
            return Application.Current.MainPage.Navigation.PushAsync(_page);
        }
    }
}