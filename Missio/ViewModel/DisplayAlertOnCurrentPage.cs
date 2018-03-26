using System.Threading.Tasks;
using Xamarin.Forms;

namespace ViewModel
{
    public class DisplayAlertOnCurrentPage : IDisplayAlertOnCurrentPage
    {
        /// <inheritdoc />
        public Task DisplayAlert(string title, string message, string acceptMessage)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, acceptMessage);
        }
    }
}