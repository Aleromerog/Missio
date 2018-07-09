using Missio.Registration;

namespace Missio.LogInRes
{
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
