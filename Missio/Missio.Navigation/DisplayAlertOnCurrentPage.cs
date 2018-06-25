using System.Threading.Tasks;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class DisplayAlertOnCurrentPage : IDisplayAlertOnCurrentPage
    {
        /// <inheritdoc />
        public Task DisplayAlert(string title, string message, string acceptMessage)
        {
            return Application.Current.MainPage.DisplayAlert(title, message, acceptMessage);
        }

        /// <inheritdoc />
        public Task DisplayAlert(AlertTextMessage alertContents)
        {
            return Application.Current.MainPage.DisplayAlert(alertContents.Title, alertContents.Message, alertContents.AcceptMessage);
        }
    }
}