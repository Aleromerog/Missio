using Xamarin.Forms.Xaml;

namespace ViewModels.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LikesPage 
    {
        public LikesPage()
        {
            InitializeComponent();
        }

        public LikesPage(LikesViewModel likesViewModel)
        {
            BindingContext = likesViewModel;
            InitializeComponent();
        }
    }
}