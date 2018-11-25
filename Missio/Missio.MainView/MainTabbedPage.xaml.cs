using JetBrains.Annotations;
using Missio.Navigation;
using Missio.NewsFeed;
using Missio.Users;
using Xamarin.Forms.Xaml;

namespace Missio.MainView
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