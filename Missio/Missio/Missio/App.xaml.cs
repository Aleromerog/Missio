using System;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.LogInRes;
using Missio.Navigation;
using Missio.NewsFeed;
using Missio.PostPublication;
using Missio.Registration;
using Missio.Users;
using Mission.ViewModel;
using Ninject;
using Xamarin.Forms;
using Ninject.Modules;
using INavigation = Missio.Navigation.INavigation;

namespace Missio
{
	public partial class App
	{
        private static bool _isPreviewing = true;

	    public App()
	    {
	        InitializeComponent();
            var kernel = new StandardKernel(new ModelModule(), new ToolsPageModule(), new ViewModelModule(), new NewsFeedModule(), new PublicationPageModule(), new LogInModule(), new MainViewModule(), new ProfilePageModule(), new CalendarPageModule(), new RegistrationPageModule(), new ApplicationNavigation());
            var appNavigation = kernel.Get<Navigation.ApplicationNavigation>();
            appNavigation.GoToPage<LogInPage>();
        }

        public static void AssertIsPreviewing()
        {
            if (!_isPreviewing)
                throw new InvalidOperationException("Application is not in preview mode, make sure you used the right constructor");
        }

        protected override void OnStart ()
		{
		    _isPreviewing = false;
        }
	}

    public class ToolsPageModule : NinjectModule
    {
        public override void Load()
        {
        }
    }

    public class ProfilePageModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
        }
    }

    public class CalendarPageModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
        }
    }

    public class RegistrationPageModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<RegistrationViewModel>().ToSelf().InSingletonScope();
        }
    }

    public class MainViewModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<Page>().To<CalendarPage>().WhenInjectedExactlyInto<MainTabbedPage>();
            Bind<Page>().To<NewsFeedPage>().WhenInjectedExactlyInto<MainTabbedPage>();
            Bind<Page>().To<ProfilePage>().WhenInjectedExactlyInto<MainTabbedPage>();
            Bind<MainTabbedPageViewModel>().ToSelf().InSingletonScope();
        }
    } 

    public class ApplicationNavigation : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<IPageFactory>().To<PageFactory>();
            Bind<INavigation, Navigation.ApplicationNavigation>().To<Navigation.ApplicationNavigation>().InSingletonScope();
        }
    }

	public class ViewModelModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
			Bind<IDisplayAlertOnCurrentPage>().To<DisplayAlertOnCurrentPage>().InSingletonScope();
			Bind<IOnUserLoggedIn, IGetLoggedInUser, ISetLoggedInUser, GlobalUser>().To<GlobalUser>().InSingletonScope();
        }
	}

    public class PublicationPageModule : NinjectModule
    {
        public override void Load()
        {
            Bind<PublicationPageViewModel>().ToSelf().InSingletonScope();
        }
    }

    public class NewsFeedModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<IUpdateViewPosts, NewsFeedViewModel<PublicationPage>>().To<NewsFeedViewModel<PublicationPage>>().InSingletonScope();
        }
    }

	public class LogInModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
            Bind<LogInViewModel<RegistrationPage>>().ToSelf().InSingletonScope();
		}
	}

    public class ModelModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
#if USE_FAKE_DATA
            Bind<IGetMostRecentPosts, IPublishPost>().To<LocalNewsFeedPostDatabase>().InSingletonScope();
            Bind<IValidateUser, IDoesUserExist, IRegisterUser>().To<LocalUserDatabase>().InSingletonScope();
#else
            
#endif
        }
    }
}
