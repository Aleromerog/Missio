using System;
using JetBrains.Annotations;
using Missio.LogIn;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogInPage
	{
        [Obsolete("Only for previewing with the Xamarin previewer", true)]
	    public LogInPage()
	    {
	        App.AssertIsPreviewing();
            InitializeComponent();
	    }

        [UsedImplicitly]
	    public LogInPage(LogInViewModel logInViewModel)
	    {
	        BindingContext = logInViewModel;
	        InitializeComponent();
	    }
	}
}