using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Domain.Repositories;
using Missio.Navigation;
using Ninject;
using Ninject.Modules;
using ViewModels;
using ViewModels.Factories;
using ViewModels.Views;
using INavigation = Missio.Navigation.INavigation;

namespace Missio
{
	public partial class App
	{
	    public App(string webServerBaseAddress)
	    {
	        InitializeComponent();
            var appNavigation = ResolveApplicationNavigation(webServerBaseAddress);
	        appNavigation.GoToPage<LogInPage>();
        }

	    public static ApplicationNavigation ResolveApplicationNavigation(string webServerBaseAddress)
	    {
	        var kernel = GetResolutionRoot(webServerBaseAddress);
	        return kernel.Get<ApplicationNavigation>();
	    }

	    public static StandardKernel GetResolutionRoot(string webServerBaseAddress)
	    {
	        return new StandardKernel(new ModelModule(webServerBaseAddress), new ViewModelModule(), new ApplicationNavigationModule());
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
            Bind<IMainTabbedPageFactory>().To<MainTabbedPageFactory>().InSingletonScope();
            Bind<ICommentsPageFactory>().To<CommentsPageFactory>().InSingletonScope();
            Bind<INewsFeedPageFactory>().To<NewsFeedPageFactory>().InSingletonScope();
            Bind<IPublicationPageFactory>().To<PublicationPageFactory>().InSingletonScope();
            Bind<LogInViewModel>().ToSelf();
            Bind<NewsFeedViewModel>().ToSelf();
            Bind<CommentsViewModel>().ToSelf();
            Bind<RegistrationViewModel>().ToSelf();
            Bind<PublicationPageViewModel>().ToSelf();
        }
    }

    public class ModelModule : NinjectModule
    {
        private readonly string _webServerBaseAddress;

        public ModelModule(string webServerBaseAddress)
        {
            _webServerBaseAddress = webServerBaseAddress;
        }
        /// <inheritdoc />
        public override void Load()
        {
            //TODO: Remove this when we have a valid SSL certificate
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var httpClient = new HttpClient {BaseAddress = new Uri(_webServerBaseAddress) };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Bind<HttpClient>().ToConstant(httpClient);
            Bind<IPostRepository>().To<WebPostsRepository>().InSingletonScope();
            Bind<IUserRepository>().To<WebUserRepository>().InSingletonScope();
        }
    }
}
