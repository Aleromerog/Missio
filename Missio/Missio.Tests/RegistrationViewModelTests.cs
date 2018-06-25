using Missio.Navigation;
using Missio.Registration;
using NSubstitute;
using NUnit.Framework;
using StringResources;

namespace Missio.Tests
{
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
            registrationInfo.Email = email;
            //Act
            await _registrationViewModel.TryToRegister();
            //Assert
            await _fakeReturnToPreviousPage.Received().ReturnToPreviousPage();
        }
    }
}