using JetBrains.Annotations;

namespace ViewModels.Views
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
