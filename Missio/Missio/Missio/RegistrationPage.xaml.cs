using Missio.Registration;

namespace Missio.LogInRes
{
    // ReSharper disable once MismatchedFileName
    public partial class RegistrationPage 
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        public RegistrationPage(RegistrationViewModel registrationViewModel) : this()
        {
            BindingContext = registrationViewModel;
        }
    }
}
