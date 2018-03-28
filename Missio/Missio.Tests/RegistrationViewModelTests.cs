using Mission.Model.LocalProviders;
using NSubstitute;
using NUnit.Framework;
using StringResources;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class RegistrationViewModelTests
    {
        private RegistrationViewModel RegistrationViewModel;
        private IDisplayAlertOnCurrentPage FakeDisplayAlert;
        private IRegisterUser FakeRegisterUser;
        private IDoesUserExist FakeDoesUserExist;
        private IReturnToPreviousPage FakeReturnToPreviousPage;

        [SetUp]
        public void SetUp()
        {
            FakeDisplayAlert = Substitute.For<IDisplayAlertOnCurrentPage>();
            FakeRegisterUser = Substitute.For<IRegisterUser>();
            FakeDoesUserExist = Substitute.For<IDoesUserExist>();
            FakeReturnToPreviousPage = Substitute.For<IReturnToPreviousPage>();
            RegistrationViewModel = new RegistrationViewModel(FakeDisplayAlert, FakeRegisterUser, FakeDoesUserExist, FakeReturnToPreviousPage);
        }

        [Test]
        public void UserName_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", RegistrationViewModel.UserName);
        }

        [Test]
        public void Password_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", RegistrationViewModel.Password);
        }

        [Test]
        public void ConfirmPassword_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", RegistrationViewModel.ConfirmPassword);
        }

        [Test]
        public void Email_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", RegistrationViewModel.Email);
        }

        [Test]
        [TestCase("")]
        [TestCase("AA")]
        [TestCase("BBB")]
        public async void TryToRegister_UserNameIsTooShort_DisplaysAlert(string username)
        {
            //Arrange
            RegistrationViewModel.UserName = username;
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            await FakeDisplayAlert.Received(1).DisplayAlert(AppResources.UserNameTooShortTitle,
                AppResources.UserNameTooShortMessage, AppResources.Ok);
        }


        [Test]
        [TestCase("haha")]
        [TestCase("GG")]
        [TestCase("WP")]
        public async void TryToRegister_PasswordIsTooShort_DisplaysAlert(string password)
        {
            //Arrange
            RegistrationViewModel.UserName = "Valid name";
            RegistrationViewModel.Password = password;
            RegistrationViewModel.ConfirmPassword = password;
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            await FakeDisplayAlert.DisplayAlert(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("no", "si")]
        [TestCase("hola", "mundo")]
        public async void TryToRegister_PasswordsDontMatch_DisplaysAlert(string first, string second)
        {
            //Arrange
            RegistrationViewModel.UserName = "Valid name";
            RegistrationViewModel.Password = first;
            RegistrationViewModel.ConfirmPassword = second;
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            await FakeDisplayAlert.DisplayAlert(AppResources.PasswordsDontMatchTitle, AppResources.PasswordsDontMatchMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Un nombre")]
        [TestCase("Otro nombre")]
        public async void TryToRegister_UserNameAlreadyInUse_DisplaysAlert(string userName)
        {
            //Arrange
            RegistrationViewModel.UserName = userName;
            RegistrationViewModel.Password = "Valid password";
            RegistrationViewModel.ConfirmPassword = "Valid password";
            FakeDoesUserExist.DoesUserExist(userName).Returns(true);
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            await FakeDisplayAlert.DisplayAlert(AppResources.UserNameAlreadyInUseTitle,
                AppResources.UserNameAlreadyInUseMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_RegistersUser(string userName, string password, string email)
        {
            //Arrange
            RegistrationViewModel.UserName = userName;
            RegistrationViewModel.Password = password;
            RegistrationViewModel.ConfirmPassword = password;
            RegistrationViewModel.Email = email;
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            FakeRegisterUser.Received(1).RegisterUser(userName, password, email);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_DisplaysEmailWasSent(string userName, string password, string email)
        {
            //Arrange
            RegistrationViewModel.UserName = userName;
            RegistrationViewModel.Password = password;
            RegistrationViewModel.ConfirmPassword = password;
            RegistrationViewModel.Email = email;
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            await FakeDisplayAlert.DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_ReturnsToPreviousPage(string userName, string password, string email)
        {
            //Arrange
            RegistrationViewModel.UserName = userName;
            RegistrationViewModel.Password = password;
            RegistrationViewModel.ConfirmPassword = password;
            RegistrationViewModel.Email = email;
            //Act
            await RegistrationViewModel.TryToRegister();
            //Assert
            await FakeReturnToPreviousPage.ReturnToPreviousPage();
        }
    }
}