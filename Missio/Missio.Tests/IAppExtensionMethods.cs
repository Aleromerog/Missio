using System;
using Mission.Model.Data;
using Mission.Model.LocalServices;
using Xamarin.UITest;

namespace Missio.Tests
{
    public static class IAppExtensionMethods
    {
        /// <summary>
        /// Logs in onto the app with an existing user, use <see cref="LogInWithUser"/> if you want to use an specific user
        /// </summary>
        public static void LogInWithDefaultUser(this IApp app)
        {
            if (LocalUserDatabase.ValidUsers.Count == 0)
                throw new InvalidOperationException("There are no valid users");
            var user = LocalUserDatabase.ValidUsers[0];
            app.EnterText(c => c.Marked("UserNameEntry"), user.UserName);
            app.EnterText(c => c.Marked("PasswordEntry"), user.Password);
            app.DismissKeyboard();
            app.Tap(c => c.Marked("LogInButton"));
        }

        /// <summary>
        /// Tries to login on the the app with the given user information
        /// </summary>
        /// <param name="app"> The app to login onto </param>
        /// <param name="user"> The user information </param>
        public static void LogInWithUser(this IApp app, User user)
        {
            app.EnterText(c => c.Marked("UserNameEntry"), user.UserName);
            app.EnterText(c => c.Marked("PasswordEntry"), user.Password);
            app.DismissKeyboard();
            app.Tap(c => c.Marked("LogInButton"));
        }
    }
}