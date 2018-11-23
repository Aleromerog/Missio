using JetBrains.Annotations;

namespace Missio.Registration
{
    public partial class RegistrationPage 
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public RegistrationPage(RegistrationViewModel registrationViewModel) : this()
        {
            BindingContext = registrationViewModel;
        }
    }
}
