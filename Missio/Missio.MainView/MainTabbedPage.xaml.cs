using JetBrains.Annotations;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio.MainView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // ReSharper disable once MismatchedFileName
    public partial class MainTabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public MainTabbedPage(Page[] childPages)
        {
            foreach (var page in childPages)
                Children.Add(page);
            InitializeComponent();
        }

        /// <inheritdoc />
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}