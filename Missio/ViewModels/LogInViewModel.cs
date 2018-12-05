using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using Domain.Exceptions;
using Domain.Repositories;
using JetBrains.Annotations;
using Missio.ApplicationResources;
using ViewModels.Factories;
using ViewModels.Views;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModels
{
    public class LogInViewModel
    {
        private string _userName;
        private string _password;

        [UsedImplicitly]
        public string UserName
        {
            get => _userName ?? "";
            set => _userName = value;
        }

        [UsedImplicitly]
        public string Password
        {
            get => _password ?? "";
            set => _password = value;
        }

        [UsedImplicitly]
        public ICommand LogInCommand { get; }

        [UsedImplicitly]
        public ICommand GoToRegistrationPageCommand { get; }

        private readonly IUserRepository _userRepository;
        private readonly IMainTabbedPageFactory _mainTabbedPageFactory;
        private readonly INavigation _navigation;

        public LogInViewModel([NotNull] INavigation navigation, [NotNull] IUserRepository userRepository, [NotNull] IMainTabbedPageFactory mainTabbedPageFactory)
        {
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mainTabbedPageFactory = mainTabbedPageFactory ?? throw new ArgumentNullException(nameof(mainTabbedPageFactory));
            GoToRegistrationPageCommand = new Command(() => navigation.GoToPage<RegistrationPage>());
            LogInCommand = new Command(async() => await LogIn());
        }

        /// <summary>
        /// Attempts to login the user with the given username and password
        /// </summary>
        public async Task LogIn()
        {
            try
            {
                var nameAndPassword = new NameAndPassword(UserName, Password);
                await _userRepository.ValidateUser(nameAndPassword);
                await _mainTabbedPageFactory.CreateAndNavigateToPage(nameAndPassword);
            }
            catch (LogInException e)
            {
                await _navigation.DisplayAlert(Strings.TheLogInWasUnsuccessful, e.ErrorMessage, Strings.Ok);
            }
        }
    }
}