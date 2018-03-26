using System;
using JetBrains.Annotations;
using ViewModel;
using Xamarin.Forms.Xaml;

namespace Missio
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