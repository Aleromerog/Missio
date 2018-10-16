using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Missio.ExternalDatabase;
using Missio.LocalDatabase;
using Missio.LogIn;
using Missio.LogInRes;
using Missio.Navigation;
using Missio.NewsFeed;
using Missio.PostPublication;
using Missio.Registration;
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
            var appNavigation = ResolveApplicationNavigation();
	        appNavigation.GoToPage<LogInPage>();
        }

	    public static ApplicationNavigation ResolveApplicationNavigation()
	    {
	        var kernel = new StandardKernel(new ModelModule(), new ToolsPageModule(), new ViewModelModule(),
	            new NewsFeedModule(), new PublicationPageModule(), new LogInModule(), new MainViewModule(),
	            new ProfilePageModule(), new CalendarPageModule(), new RegistrationPageModule(), new ApplicationNavigationModule());
	        return kernel.Get<ApplicationNavigation>();
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
            Bind<Page>().To<NewsFeedPage>().WhenInjectedExactlyInto<MainTabbedPage>();
            Bind<Page>().To<CalendarPage>().WhenInjectedExactlyInto<MainTabbedPage>();
            Bind<Page>().To<ToolsPage>().WhenInjectedExactlyInto<MainTabbedPage>();
            Bind<Page>().To<ProfilePage>().WhenInjectedExactlyInto<MainTabbedPage>();
        }
    } 

    public class ApplicationNavigationModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
            Bind<IPageFactory>().To<PageFactory>();
            Bind<INavigation, ApplicationNavigation>().To<ApplicationNavigation>().InSingletonScope();
        }
    }

	public class ViewModelModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
			Bind<ILoggedInUser, GlobalUser>().To<GlobalUser>().InSingletonScope();
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
            Bind<IUpdateViewPosts, NewsFeedViewModel>().To<NewsFeedViewModel>().InSingletonScope();
        }
    }

	public class LogInModule : NinjectModule
	{
		/// <inheritdoc />
		public override void Load()
		{
            Bind<LogInViewModel>().ToSelf().InSingletonScope();
		}
	}

    public class ModelModule : NinjectModule
    {
        /// <inheritdoc />
        public override void Load()
        {
#if USE_FAKE_DATA
            Bind<IPostRepository>().To<LocalNewsFeedPostRepository>().InSingletonScope();
            Bind<IUserRepository>().To<LocalUserRepository>().InSingletonScope();
#else
            //TODO: Remove this when we have a valid SSL certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var httpClient = new HttpClient {BaseAddress = new Uri("https://10.0.2.2:44333/")};
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Bind<HttpClient>().ToConstant(httpClient);
            Bind<IPostRepository>().To<LocalNewsFeedPostRepository>().InSingletonScope();
            Bind<IUserRepository>().To<WebUserRespository>().InSingletonScope();
#endif
        }
    }
}
