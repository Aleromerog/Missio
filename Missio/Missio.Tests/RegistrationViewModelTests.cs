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
        private RegistrationViewModel _registrationViewModel;
        private IDisplayAlertOnCurrentPage _fakeDisplayAlert;
        private IRegisterUser _fakeRegisterUser;
        private IDoesUserExist _fakeDoesUserExist;
        private IReturnToPreviousPage _fakeReturnToPreviousPage;

        [SetUp]
        public void SetUp()
        {
            _fakeDisplayAlert = Substitute.For<IDisplayAlertOnCurrentPage>();
            _fakeRegisterUser = Substitute.For<IRegisterUser>();
            _fakeDoesUserExist = Substitute.For<IDoesUserExist>();
            _fakeReturnToPreviousPage = Substitute.For<IReturnToPreviousPage>();
            _registrationViewModel = new RegistrationViewModel(_fakeDisplayAlert, _fakeRegisterUser, _fakeDoesUserExist, _fakeReturnToPreviousPage);
        }

        [Test]
        public void UserName_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", _registrationViewModel.UserName);
        }

        [Test]
        public void Password_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", _registrationViewModel.Password);
        }

        [Test]
        public void ConfirmPassword_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", _registrationViewModel.ConfirmPassword);
        }

        [Test]
        public void Email_IsNull_ReturnsEmptyString()
        {
            //Arrange
            //Act
            //Assert
            Assert.AreEqual("", _registrationViewModel.Email);
        }

        [Test]
        [TestCase("")]
        [TestCase("AA")]
        [TestCase("BBB")]
        public async void TryToRegister_UserNameIsTooShort_DisplaysAlert(string username)
        {
            //Arrange
            _registrationViewModel.UserName = username;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.Received(1).DisplayAlert(AppResources.UserNameTooShortTitle,
                AppResources.UserNameTooShortMessage, AppResources.Ok);
        }


        [Test]
        [TestCase("haha")]
        [TestCase("GG")]
        [TestCase("WP")]
        public async void TryToRegister_PasswordIsTooShort_DisplaysAlert(string password)
        {
            //Arrange
            _registrationViewModel.UserName = "Valid name";
            _registrationViewModel.Password = password;
            _registrationViewModel.ConfirmPassword = password;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.DisplayAlert(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("no", "si")]
        [TestCase("hola", "mundo")]
        public async void TryToRegister_PasswordsDontMatch_DisplaysAlert(string first, string second)
        {
            //Arrange
            _registrationViewModel.UserName = "Valid name";
            _registrationViewModel.Password = first;
            _registrationViewModel.ConfirmPassword = second;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.DisplayAlert(AppResources.PasswordsDontMatchTitle, AppResources.PasswordsDontMatchMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Un nombre")]
        [TestCase("Otro nombre")]
        public async void TryToRegister_UserNameAlreadyInUse_DisplaysAlert(string userName)
        {
            //Arrange
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = "Valid password";
            _registrationViewModel.ConfirmPassword = "Valid password";
            _fakeDoesUserExist.DoesUserExist(userName).Returns(true);
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.DisplayAlert(AppResources.UserNameAlreadyInUseTitle,
                AppResources.UserNameAlreadyInUseMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_RegistersUser(string userName, string password, string email)
        {
            //Arrange
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.ConfirmPassword = password;
            _registrationViewModel.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            _fakeRegisterUser.Received(1).RegisterUser(userName, password, email);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_DisplaysEmailWasSent(string userName, string password, string email)
        {
            //Arrange
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.ConfirmPassword = password;
            _registrationViewModel.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_ReturnsToPreviousPage(string userName, string password, string email)
        {
            //Arrange
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.ConfirmPassword = password;
            _registrationViewModel.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeReturnToPreviousPage.ReturnToPreviousPage();
        }
    }
}