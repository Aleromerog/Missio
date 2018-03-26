using Xamarin.Forms;
using Mission.Model.LocalProviders;
using Ninject;
using Ninject.Modules;
using ViewModel;

namespace Missio
{
	public partial class App
	{
	    public App()
	    {
	        var kernel = new KernelConfiguration(new ModelModule(), new ViewModelModule(), new NewsFeedModule(), new PublicationPageModule(), new LogInModule(), new AppViewModule()).BuildReadonlyKernel();
            InitializeComponent();
	        kernel.Get<AppViewModel>().StartFromPage(kernel.Get<LogInPage>());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
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
            Bind<INewsFeedViewPosts, IUpdateViewPosts, NewsFeedViewModel>().To<NewsFeedViewModel>().InSingletonScope();
            Bind<Page>().To<NewsFeedPage>().InSingletonScope();
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
            Bind<IGetMostRecentPosts, IPublishPost>().To<LocalNewsFeedPostProvider>().InSingletonScope();
            Bind<IValidateUser>().To<LocalUserDatabase>().InSingletonScope();
            Bind<INewsFeedPostsUpdater>().To<NewsFeedPostsUpdater>().InSingletonScope();
#else
#endif
        }
    }
}
