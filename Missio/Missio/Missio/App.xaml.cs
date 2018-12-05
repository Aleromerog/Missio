﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Domain.Repositories;
using Missio.Navigation;
using Ninject;
using Xamarin.Forms;
using Ninject.Modules;
using ViewModels;
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
	        var kernel = new StandardKernel(new ModelModule(webServerBaseAddress), new ToolsPageModule(),
	            new NewsFeedModule(), new PublicationPageModule(), new LogInModule(), new MainViewModule(),
	            new ProfilePageModule(), new CalendarPageModule(), new RegistrationPageModule(), new ApplicationNavigationModule());
	        return kernel.Get<ApplicationNavigation>();
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
