using Domain;
using JetBrains.Annotations;
using Missio.Navigation;
using ViewModels.Factories;
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
        public MainTabbedPage(IPageFactory pageFactory, INewsFeedPageFactory newsFeedPageFactory, NameAndPassword nameAndPassword)
        {
            Children.Add(newsFeedPageFactory.CreatePage(nameAndPassword));
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