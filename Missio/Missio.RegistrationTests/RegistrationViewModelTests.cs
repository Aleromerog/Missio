using System.Collections.Generic;
using System.Threading.Tasks;
using Missio.LocalDatabase;
using Missio.Navigation;
using Missio.Registration;
using Missio.Users;
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
            _fakeUserRepository = Substitute.For<IUserRepository>();
            _fakeNavigation = Substitute.For<INavigation>();
            _registrationViewModel = new RegistrationViewModel(_fakeUserRepository, _fakeNavigation);
        }

        [Test]
        public async Task TryToRegister_FieldsLeftEmpty_DoesNotThrowNullReferenceException()
        {
            await _registrationViewModel.TryToRegister();
        }

        [Test]
        [TestCaseSource(typeof(UserTestUtils), nameof(UserTestUtils.NamesAlreadyInUse))]
        public async Task TryToRegister_UserNameAlreadyInUse_DisplaysAlert(string userName)
        {
            var userNameMessage = new AlertTextMessage(AppResources.UserNameAlreadyInUseTitle, AppResources.UserNameAlreadyInUseMessage, AppResources.Ok);
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = "Valid password";

            _fakeUserRepository.When(x => x.AttemptToRegisterUser(Arg.Any<User>()))
                .Do(x => throw new UserRegistrationException(new List<AlertTextMessage> { userNameMessage }));

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(userNameMessage);
        }

        [Test]
        public async Task TryToRegister_PasswordIsTooShort_DisplaysAlert()
        {
            var passwordMessage = new AlertTextMessage(AppResources.PasswordTooShortTitle, AppResources.PasswordTooShortMessage, AppResources.Ok);
            _registrationViewModel.UserName = "Some username";
            _registrationViewModel.Password = "ABC";
            _fakeUserRepository.When(x => x.AttemptToRegisterUser(Arg.Any<User>()))
                .Do(x => throw new UserRegistrationException(new List<AlertTextMessage> { passwordMessage}));

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(passwordMessage);
        }

        [Test]
        public async Task TryToRegister_UserNameIsTooShort_DisplaysAlert()
        {
            var userNameTooShortMessage = new AlertTextMessage(AppResources.UserNameTooShortTitle, AppResources.UserNameTooShortMessage, AppResources.Ok);
            _registrationViewModel.UserName = "AB";
            _registrationViewModel.Password = "Some password";

            _fakeUserRepository.When(x => x.AttemptToRegisterUser(Arg.Any<User>()))
                .Do(x => throw new UserRegistrationException(new List<AlertTextMessage> { userNameTooShortMessage}));


            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(userNameTooShortMessage);
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async Task TryToRegister_EverythingIsOk_RegistersUser(string userName, string password, string email)
        {
            _registrationViewModel.UserName = userName;
            _registrationViewModel.Password = password;
            _registrationViewModel.Email = email;

            await _registrationViewModel.TryToRegister();

            await _fakeUserRepository.Received(1).AttemptToRegisterUser(Arg.Is<User>(x => x.UserName == userName && x.Password == password && x.Email == email));
        }

        [Test]
        [TestCase("Ana Gaxiola", "TeQuieroJorge", "ana@gmail.com")]
        [TestCase("ElAmorDeTuVida", "NoTeAmo", "elamordetuvida@gmail.com")]
        public async Task TryToRegister_EverythingIsOk_DisplaysEmailWasSent(string userName, string password, string email)
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