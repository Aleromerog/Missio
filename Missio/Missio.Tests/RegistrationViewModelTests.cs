﻿using Mission.Model.Exceptions;
using Mission.Model.Services;
using NSubstitute;
using NUnit.Framework;
using StringResources;
using ViewModel;

namespace Missio.Tests
{
    [TestFixture]
    public class RegistrationInfoTests
    {
        private RegistrationInfo _registrationInfo;

        [SetUp]
        public void SetUp()
        {
            _registrationInfo = new RegistrationInfo("", "", "", "");
        }

        [Test]
        [TestCase("")]
        [TestCase("AA")]
        [TestCase("BBB")]
        public void GetOfflineErrorMessages_UserNameIsTooShort_DisplaysAlert(string username)
        {
            //Arrange
            var expectedError = new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok);
            _registrationInfo.UserName = username;
            //Act
            var errorMessages = _registrationInfo.GetOfflineErrorMessages();
            //Assert
            Assert.Contains(expectedError, errorMessages);
        }

        [Test]
        [TestCase("haha")]
        [TestCase("GG")]
        [TestCase("WP")]
        public void GetOfflineErrorMessages_PasswordIsTooShort_DisplaysAlert(string password)
        {
            //Arrange
            var expectedError = new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
            _registrationInfo.Password = password;
            _registrationInfo.ConfirmPassword = password;
            //Act
            var errorMessages = _registrationInfo.GetOfflineErrorMessages();
            //Assert
            Assert.Contains(expectedError, errorMessages);
        }

        [Test]
        [TestCase("no", "si")]
        [TestCase("hola", "mundo")]
        public void GetOfflineErrorMessages_PasswordsDontMatch_DisplaysAlert(string first, string second)
        {
            //Arrange
            var expectedError = new AlertTextMessage(AppResources.PasswordsDontMatchTitle, AppResources.PasswordsDontMatchMessage, AppResources.Ok);
            _registrationInfo.Password = first;
            _registrationInfo.ConfirmPassword = second;
            //Act
            var errorMessages = _registrationInfo.GetOfflineErrorMessages();
            //Assert
            Assert.Contains(expectedError, errorMessages);
        }
    }

    [TestFixture]
    public class RegistrationViewModelTests
    {
        private RegistrationViewModel _registrationViewModel;
        private IDisplayAlertOnCurrentPage _fakeDisplayAlert;
        private IRegisterUser _fakeRegisterUser;
        private IReturnToPreviousPage _fakeReturnToPreviousPage;

        [SetUp]
        public void SetUp()
        {
            _fakeDisplayAlert = Substitute.For<IDisplayAlertOnCurrentPage>();
            _fakeRegisterUser = Substitute.For<IRegisterUser>();
            _fakeReturnToPreviousPage = Substitute.For<IReturnToPreviousPage>();
            _registrationViewModel = new RegistrationViewModel(_fakeDisplayAlert, _fakeRegisterUser, _fakeReturnToPreviousPage);
        }

        [Test]
        public void Constructor_NormalConstruction_AllFieldsAreEmptyStrings()
        {
            //Assert
            Assert.IsEmpty(_registrationViewModel.RegistrationInfo.UserName);
            Assert.IsEmpty(_registrationViewModel.RegistrationInfo.Email);
            Assert.IsEmpty(_registrationViewModel.RegistrationInfo.Password);
            Assert.IsEmpty(_registrationViewModel.RegistrationInfo.ConfirmPassword);
        }

        [Test]
        [TestCaseSource(typeof(UserNamesAlreadyInUse), nameof(UserNamesAlreadyInUse.NamesAlreadyInUse))]
        public async void TryToRegister_UserNameAlreadyInUse_DisplaysAlert(string userName)
        {
            //Arrange
            var userNameException = new UserNameAlreadyInUseException();
            _fakeRegisterUser.When(x => x.RegisterUser(Arg.Is<RegistrationInfo>(c => c.UserName == userName))).Do(x => throw userNameException);
            var registrationInfo = _registrationViewModel.RegistrationInfo;
            registrationInfo.UserName = userName;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.Received().DisplayAlert(Arg.Is(userNameException.AlertMessage));
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_RegistersUser(string userName, string password, string email)
        {
            //Arrange
            var registrationInfo = _registrationViewModel.RegistrationInfo;
            registrationInfo.UserName = userName;
            registrationInfo.Password = password;
            registrationInfo.ConfirmPassword = password;
            registrationInfo.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            _fakeRegisterUser.Received(1).RegisterUser(registrationInfo);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_DisplaysEmailWasSent(string userName, string password, string email)
        {
            //Arrange
            var registrationInfo = _registrationViewModel.RegistrationInfo;
            registrationInfo.UserName = userName;
            registrationInfo.Password = password;
            registrationInfo.ConfirmPassword = password;
            registrationInfo.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeDisplayAlert.Received().DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_ReturnsToPreviousPage(string userName, string password, string email)
        {
            //Arrange
            var registrationInfo = _registrationViewModel.RegistrationInfo;
            registrationInfo.UserName = userName;
            registrationInfo.Password = password;
            registrationInfo.ConfirmPassword = password;
            registrationInfo.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeReturnToPreviousPage.Received().ReturnToPreviousPage();
        }
    }
}