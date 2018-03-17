using System;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    public class AppInitializer
    {
        /// <summary>
        /// Starts and the app on the given platform emulator
        /// </summary>
        /// <param name="platform"> The platform to use </param>
        /// <returns></returns>
        public static IApp StartApp(Platform platform)
        {
            IApp app;
            if (platform == Platform.Android)
            {
                app = ConfigureApp.Android.InstalledApp("com.ChomeDev.Missio").StartApp();
                return app;
            }

            if (Environment.OSVersion.Platform == PlatformID.Win32NT) // Cant run iOS test on windows
                Assert.Pass();
            app = ConfigureApp.iOS.StartApp();
            return app;
        }

        /// <summary>
        /// Logs in onto the app with an existing user, use <see cref="TryToLogIn"/> if you want to use an specific user
        /// </summary>
        /// <param name="app"></param>
        public static void LogIn(IApp app)
        {
            if (LocalUserDatabase.ValidUsers.Count == 0)
                throw new InvalidOperationException("There are no valid users");
            var validUser = LocalUserDatabase.ValidUsers[0];
            app.EnterText(c => c.Marked("UserNameEntry"), validUser.UserName);
            app.EnterText(c => c.Marked("PasswordEntry"), validUser.Password);
            app.Tap(c => c.Marked("LogInButton"));
        }

        /// <summary>
        /// Tries to login on the the app with the given user information
        /// </summary>
        /// <param name="app"> The app to login onto </param>
        /// <param name="user"> The user information </param>
        public static void TryToLogIn(IApp app, User user)
        {
            app.EnterText(c => c.Marked("UserNameEntry"), user.UserName);
            app.EnterText(c => c.Marked("PasswordEntry"), user.Password);
            app.Tap(c => c.Marked("LogInButton"));
        }
    }
}