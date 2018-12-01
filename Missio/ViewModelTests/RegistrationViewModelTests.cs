using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DataTransferObjects;
using Domain.Exceptions;
using Domain.Repositories;
using Missio.ApplicationResources;
using Missio.Navigation;
using NSubstitute;
using NUnit.Framework;
using ViewModels;

namespace ViewModelTests
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
            _fakeUserRepository.When(x => x.AttemptToRegisterUser(Arg.Any<CreateUserDTO>()))
                .Do(x => throw new UserRegistrationException(new List<string> { "Some message", "Another message" }));

            await _registrationViewModel.TryToRegister();

            await _fakeNavigation.Received().DisplayAlert(Strings.TheRegistrationFailed, "Some message" + Environment.NewLine + "Another message", Strings.Ok);
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

            await _fakeUserRepository.Received(1).AttemptToRegisterUser(Arg.Is<CreateUserDTO>(x => x.UserName == userName && x.Password == password && x.Email == email));
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

            await _fakeNavigation.Received().DisplayAlert(Strings.RegistrationSuccessfulTitle, Strings.RegistrationSuccessfulMessage, Strings.Ok);
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