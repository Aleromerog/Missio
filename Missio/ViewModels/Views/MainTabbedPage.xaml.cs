using Domain;
using JetBrains.Annotations;
using Missio.Navigation;
using Xamarin.Forms.Xaml;

namespace ViewModels.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage
    {
        public MainTabbedPage()
        {
            InitializeComponent();
        }

        [UsedImplicitly]
        public MainTabbedPage(IPageFactory pageFactory, NameAndPassword nameAndPassword)
        {
            Children.Add(pageFactory.MakePage<NewsFeedPage>(nameAndPassword));
            Children.Add(pageFactory.MakePage<CalendarPage>());
            Children.Add(pageFactory.MakePage<ToolsPage>());
            Children.Add(pageFactory.MakePage<ProfilePage>());
            InitializeComponent();
        }

        /// <inheritdoc />
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}