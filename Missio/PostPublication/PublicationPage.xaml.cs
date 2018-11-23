using System;
using JetBrains.Annotations;
using Xamarin.Forms.Xaml;

namespace Missio.PostPublication
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PublicationPage
	{
	    [Obsolete("Only for previewing with the Xamarin previewer", true)]
        public PublicationPage ()
		{
			InitializeComponent ();
		}

        [UsedImplicitly]
	    public PublicationPage(PublicationPageViewModel publicationPageViewModel)
	    {
            BindingContext = publicationPageViewModel;
            InitializeComponent();
	    }
	}
}