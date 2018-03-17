using Xamarin.Forms;
using Mission.Model.LocalProviders;
using Ninject;
using Ninject.Modules;
using ViewModel;

namespace Missio
{
	public partial class App
	{
		public App ()
		{
            var kernel = new KernelConfiguration(new ModelModule(), new ViewModelModule(), new NewsFeedModule(), new LogInModule()).BuildReadonlyKernel();
            InitializeComponent();
            MainPage = new NavigationPage(kernel.Get<LogInPage>());
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

    public class NewsFeedModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<INewsFeedViewPosts, NewsFeedViewModel>().To<NewsFeedViewModel>().InSingletonScope();
            Bind<NewsFeedPage>().ToSelf().InSingletonScope();
            Bind<IGoToNextPage>().To<GoToPage>().Named("GoToNewsFeed").WithConstructorArgument("page", x => x.Kernel.Get<NewsFeedPage>());
        }
    }

	public class LogInModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
			Bind<AttemptToLogIn>().ToSelf().InSingletonScope();
            Bind<LogInViewModel>().ToSelf().InSingletonScope();
            Bind<LogInPage>().ToSelf().InSingletonScope();
		}
	}

    public class ModelModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
#if USE_FAKE_DATA
            Bind<INewsFeedPostsProvider>().To<LocalNewsFeedPostProvider>().InSingletonScope();
            Bind<IValidateUser>().To<LocalUserDatabase>().InSingletonScope();
            Bind<INewsFeedPostsUpdater>().To<NewsFeedPostsUpdater>().InSingletonScope();
#else
#endif
        }
    }
}
