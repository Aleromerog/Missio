using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace Missio.Tests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android.InstalledApp("com.ChomeDev.Missio")
                    .StartApp();
            }

            if (Environment.OSVersion.Platform == PlatformID.Win32NT) // Cant run iOS test on windows
                Assert.Pass();
            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

