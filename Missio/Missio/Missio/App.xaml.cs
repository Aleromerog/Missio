using Xamarin.Forms;
using Mission.Model.LocalProviders;

namespace Missio
{
	public partial class App
	{
		public App ()
		{
#if USE_FAKE_DATA
            DependencyService.Register<INewsFeedPositionProvider, FakeNewsFeedPostProvider>();
            DependencyService.Register<IUserValidator, FakeUserValidator>();
#endif
			InitializeComponent();
            MainPage = new NavigationPage(new LogIn());
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
}
