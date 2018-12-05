using System.Threading.Tasks;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using Missio.ApplicationResources;
using NSubstitute;
using NUnit.Framework;
using ViewModels;
using ViewModels.Factories;
using ViewModels.Views;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModelTests
{
    [TestFixture]
    public class LogInViewModelTests
    {
        private LogInViewModel _logInViewModel;
        private INavigation _fakeNavigation;
        private IMainTabbedPageFactory _mainTabbedPageFactory;
        private IUserRepository _fakeUserRepository;

        [SetUp]
        public void SetUp()
        {
            _fakeNavigation = Substitute.For<INavigation>();
            _fakeUserRepository = Substitute.For<IUserRepository>();
            _mainTabbedPageFactory = Substitute.For<IMainTabbedPageFactory>();
            _logInViewModel = new LogInViewModel(_fakeNavigation, _fakeUserRepository, _mainTabbedPageFactory);
        }

        [Test]
        public void Constructor_NormalConstructor_InitializesUserWithEmptyFields()
        {
            Assert.IsEmpty(_logInViewModel.UserName);
            Assert.IsEmpty(_logInViewModel.Password);
        }

        [Test]
        public void UserNameSetter_ValueIsNull_IsSetToEmptyString()
        {
            _logInViewModel.UserName = null;

            Assert.IsEmpty(_logInViewModel.UserName);
        }

        [Test]
        public void PasswordSetter_ValueIsNull_IsSetToEmptyString()
        {
            _logInViewModel.Password = null;

            Assert.IsEmpty(_logInViewModel.Password);
        }

        [Test]
        public void GoToRegistrationPageCommand_NormalExecute_GoesToRegistrationPage()
        {
            _logInViewModel.GoToRegistrationPageCommand.Execute(null);

            _fakeNavigation.Received(1).GoToPage<RegistrationPage>();
        }

        [Test]
        public async Task LogInCommand_ValidUser_GoesToNextPage()
        {
            _logInViewModel.UserName = "Someone";
            _logInViewModel.Password = "Password";

            await _logInViewModel.LogIn();

            await _mainTabbedPageFactory.Received(1).CreateAndNavigateToPage(Arg.Is<NameAndPassword>(x => x.UserName == "Someone" && x.Password == "Password"));
        }

        [Test]
        public async Task AttemptToLogin_LogInFailed_DisplaysAlert()
        {
            _fakeUserRepository.When(x => x.ValidateUser(Arg.Any<NameAndPassword>())).Throw(new LogInException(Strings.InvalidUserName));

            await _logInViewModel.LogIn();

            await _fakeNavigation.Received(1).DisplayAlert(Strings.TheLogInWasUnsuccessful, Strings.InvalidUserName, Strings.Ok);
        }
    }
}
