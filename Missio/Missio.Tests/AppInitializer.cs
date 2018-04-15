using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    public static class AppInitializer
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
                Assert.Ignore();
            app = ConfigureApp.iOS.StartApp();
            return app;
        }
    }
}