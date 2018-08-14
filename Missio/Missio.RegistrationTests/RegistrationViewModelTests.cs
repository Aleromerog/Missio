using System.Threading.Tasks;
using Missio.LocalDatabase;
using Missio.Navigation;
using Missio.Registration;
using Missio.UserTests;
using NSubstitute;
using NUnit.Framework;
using StringResources;

namespace Missio.RegistrationTests
{
    [TestFixture]
    public class RegistrationViewModelTests
    {
        private RegistrationViewModel _registrationViewModel;
        private IUserRepository _fakeUserRepository;
        private INavigation _fakeNavigation;

        [SetUp]
        public void SetUp()
        {
            _fakeUserRepository = new LocalUserDatabase();
            _fakeNavigation = Substitute.For<INavigation>();
            _registrationViewModel = new RegistrationViewModel(_fakeUserRepository, _fakeNavigation);
        }

        [Test]
        public async void TryToRegister_FieldsLeftEmpty_DoesNotThrowNullReferenceException()
        {
            await _registrationViewModel.TryToRegister();
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.NamesAlreadyInUse))]
        public async void TryToRegister_UserNameAlreadyInUse_DisplaysAlert(string userName)
        {
            var userNameMessage = new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok);
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = "Valid password";

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(userNameMessage);
        }

        [Test]
        public async Task TryToRegister_PasswordIsTooShort_DisplaysAlert()
        {
            var passwordMessage = new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
            _registrationViewModel.UserName = "Some username";
            _registrationViewModel.Password = "ABC";

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(passwordMessage);
        }

        [Test]
        public async Task TryToRegister_UserNameIsTooShort_DisplaysAlert()
        {
            var userNameTooShortMessage = new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok);

            _registrationViewModel.UserName = "AB";
            _registrationViewModel.Password = "Some password";

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(userNameTooShortMessage);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_RegistersUser(string userName, string password, string email)
        {
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.Email = email;

            await _registrationViewModel.TryToRegister();

            var user = await _fakeUserRepository.GetUserByName(userName);
            Assert.AreEqual(userName, user.UserName);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(email, user.Email);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async void TryToRegister_EverythingIsOk_DisplaysEmailWasSent(string userName, string password, string email)
        {
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.Email = email;

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(AppResources.RegistrationSuccessfulTitle, AppResources.RegistrationSuccessfulMessage, AppResources.Ok);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async Task TryToRegister_EverythingIsOk_ReturnsToPreviousPage(string userName, string password, string email)
        {
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.Email = email;

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().ReturnToPreviousPage();
        }
    }
}