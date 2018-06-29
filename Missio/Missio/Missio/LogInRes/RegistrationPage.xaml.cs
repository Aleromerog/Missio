using System;
using Missio.Registration;
using Xamarin.Forms.Xaml;

namespace Missio.LogInRes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegistrationPage
	{
	    [Obsolete("Only for previewing with the Xamarin previewer", true)]
        public RegistrationPage ()
		{
            App.AssertIsPreviewing();
            InitializeComponent();
		}

	    public RegistrationPage(RegistrationViewModel registrationViewModel)
	    {
            BindingContext = registrationViewModel;
            InitializeComponent();
	    }
	}
}