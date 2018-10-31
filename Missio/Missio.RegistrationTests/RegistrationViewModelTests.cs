using System.Collections.Generic;
using System.Threading.Tasks;
using Missio.Navigation;
using Missio.Registration;
using Missio.Users;
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
        public async Task TryToRegister_ExceptionWasThrown_DisplaysAlert()
        {
            var exceptionMessage = "The message";

            _fakeUserRepository.When(x => x.AttemptToRegisterUser(Arg.Any<CreateUserDTO>()))
                .Do(x => throw new UserRegistrationException(new List<string> { exceptionMessage }));

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(AppResources.TheRegistrationFailed, exceptionMessage, AppResources.Ok);
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

            await _fakeUserRepository.Received(1).AttemptToRegisterUser(new CreateUserDTO(userName, password, email));
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