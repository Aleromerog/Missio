using System;
using System.Linq;
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

namespace Missio
{
	public partial class App
	{
        private static bool _isPreviewing = true;

	    public App()
	    {
	        var kernel = new StandardKernel(new ModelModule(), new ViewModelModule(), new NewsFeedModule(), new PublicationPageModule(), new LogInModule(),  new MainViewModule(), new ProfilePageModule(), new CalendarPageModule(), new RegistrationPageModule(), new ApplicationNavigation());
            InitializeComponent();
            var appNavigation = kernel.Get<Navigation.ApplicationNavigation>();
            appNavigation.Pages = kernel.GetAll<Page>().ToArray();
            appNavigation.StartFromPage(kernel.Get<NewRegistraionPage>());
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
    
    public class ProfilePageModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<Page, ProfilePage>().To<ProfilePage>().InSingletonScope();
        }
    }

    public class CalendarPageModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<Page, CalendarPage>().To<CalendarPage>().InSingletonScope();
        }
    }

    public class RegistrationPageModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<RegistrationViewModel>().ToSelf().InSingletonScope();
            Bind<Page>().To<RegistrationPage>().InSingletonScope();
        }
    }

    public class MainViewModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<MainTabbedPageViewModel>().ToSelf().InSingletonScope();
            Bind<Page>().To<MainTabbedPage>().InSingletonScope();
        }
    } 

    public class ApplicationNavigation : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<IGoToPage, IGoToView, IReturnToPreviousPage, Navigation.ApplicationNavigation>().To<Navigation.ApplicationNavigation>().InSingletonScope();
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
            Bind<Page>().To<PublicationPage>().InSingletonScope();
        }
    }

    public class NewsFeedModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<IUpdateViewPosts, NewsFeedViewModel>().To<NewsFeedViewModel>().InSingletonScope();
            Bind<Page, NewsFeedPage>().To<NewsFeedPage>().InSingletonScope();
        }
    }

	public class LogInModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
            Bind<LogInViewModel>().ToSelf().InSingletonScope();
            Bind<Page>().To<LogInPage>().InSingletonScope();
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
