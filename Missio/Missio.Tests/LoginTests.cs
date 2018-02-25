using System;
using System.Collections.Generic;
using Mission.Model.Data;
using Mission.Model.LocalProviders;
using NUnit.Framework;
using StringResources;
using Xamarin.UITest;

namespace Missio.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class LoginTests
    {
        private IApp app;
        private readonly Platform platform;

        private static object[] GetLogIncorrectPasswordTestsCases()
        {
            var testData = new object[FakeUserValidator.ValidUsers.Count];
            for (int i = 0; i < FakeUserValidator.ValidUsers.Count; i++)
            {
                var user = FakeUserValidator.ValidUsers[i];
                testData[i] = new object[] { user.UserName, "" };
            }
            return testData;
        }

        private static object[] GetLogInIncorrectUserNameTestCases()
        {
            return FakeUserValidator.GetListOfUsersInTestForm(FakeUserValidator.InvalidUsers);
        }

        private static object[] GetLogInSuccessfulTestsCases()
        {
            return FakeUserValidator.GetListOfUsersInTestForm(FakeUserValidator.ValidUsers);
        }

        public LoginTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        [TestCaseSource(nameof(GetLogIncorrectPasswordTestsCases))]
        public void LogIn_GivenUserName_DisplaysIncorrectPassword(string userName, string password)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(AppResources.IncorrectPasswordMessage));
        }

        [Test]
        [TestCaseSource(nameof(GetLogInIncorrectUserNameTestCases))]
        public void LogIn_GivenUserName_DisplaysIncorrectUserName(string userName, string password)
        {
            // Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            // Act
            app.Tap(c => c.Marked("LogInButton"));
            // Assert
            app.WaitForElement(c => c.Text(AppResources.IncorrectUserNameMessage));
        }

        [Test]
        [TestCaseSource(nameof(GetLogInSuccessfulTestsCases))]
        public void LogIn_ValidUserNameAndPassword_DisplaysNewsFeed(string userName, string password)
        {
            //Arrange
            app.EnterText(c => c.Marked("UserNameEntry"), userName);
            app.EnterText(c => c.Marked("PasswordEntry"), password);
            //Act
            app.Tap(c => c.Marked("LogInButton"));
            //Assert
            app.WaitForElement(c => c.Marked("NewsFeedContentPage"));
        }
    }

    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class NewsFeedTests
    {
        private IApp app;
        private readonly Platform platform;

        public NewsFeedTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        private static object[] GetOnAppearTestData()
        {
            var testData = new object[FakeUserValidator.ValidUsers.Count];
            for (var i = 0; i < FakeUserValidator.ValidUsers.Count; i++)
            {
                var user = FakeUserValidator.ValidUsers[i];
                testData[i] = new object[] { user, FakeNewsFeedPostProvider.GetMostRecentPostsAsStrings(user) };
            }
            return testData;
        }

        [Test]
        [TestCaseSource(nameof(GetOnAppearTestData))]
        public void OnAppear_GivenNewsFeedPosts_DisplaysPosts(User user, List<string> expectedPosts)
        {
            //Arrange and act
            AppInitializer.TryToLogIn(app, user);

            //Assert
            foreach (var expectedPost in expectedPosts)
            {
                var posts = app.Query(c => c.Text(expectedPost));
                Assert.AreEqual(posts.Length, 1);
            }
        }
    }
}

