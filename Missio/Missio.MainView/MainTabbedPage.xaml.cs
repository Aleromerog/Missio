using Foundation;
using JetBrains.Annotations;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Missio
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
            UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
                statusBar.BackgroundColor = UIColor.FromRGB(88, 3, 1);
            }


        }

        /// <inheritdoc />
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}