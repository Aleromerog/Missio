using System;
using Missio.LogInRes;
using Mission.Model.LocalServices;
using Mission.Model.Services;
using Xamarin.Forms;
using Ninject;
using Ninject.Modules;
using ViewModel;

namespace Missio
{
	public partial class App
	{
        private static bool _isPreviewing = true;

	    public App()
	    {
	        var kernel = new KernelConfiguration(new ModelModule(), new ViewModelModule(), new NewsFeedModule(), new PublicationPageModule(), new LogInModule(),  new MainViewModule(), new ProfilePageModule(), new CalendarPageModule(), new RegistrationPageModule(), new AppViewModule()).BuildReadonlyKernel();
            InitializeComponent();
	        kernel.Get<AppViewModel>().StartFromPage(kernel.Get<LogInPage>());
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

    public class AppViewModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<AppViewModel>().ToSelf().InSingletonScope();
            Bind<IGoToPage, IGoToView, IReturnToPreviousPage>().To<ApplicationNavigation>().InSingletonScope();
        }
    }

	public class ViewModelModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
			Bind<IDisplayAlertOnCurrentPage>().To<DisplayAlertOnCurrentPage>().InSingletonScope();
			Bind<IOnUserLoggedIn, IGetLoggedInUser, ISetLoggedInUser, GlobalUser>().To<GlobalUser>().InSingletonScope();
            Bind<IAttemptToLogin>().To<AttemptToLogIn>().InSingletonScope();
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
			Bind<AttemptToLogIn>().ToSelf().InSingletonScope();
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
            Bind<INewsFeedPostsUpdater>().To<NewsFeedPostsUpdater>().InSingletonScope();
#else
            
#endif
        }
    }
}
