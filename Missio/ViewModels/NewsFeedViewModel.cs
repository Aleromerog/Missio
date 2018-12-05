using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using Domain.Repositories;
using JetBrains.Annotations;
using ViewModels.Views;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModels
{
    public class NewsFeedViewModel : ViewModel, IUpdateViewPosts
    {
        private bool _isRefreshing;

        [UsedImplicitly]
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetField(ref _isRefreshing, value);
        }

        [UsedImplicitly]
        public ICommand UpdatePostsCommand { get; }

        [UsedImplicitly]
        public ICommand GoToPublicationPageCommand { get; }

        [UsedImplicitly]
        public ObservableCollection<IPost> Posts { get; } = new ObservableCollection<IPost>();

        private readonly IPostRepository _postRepository;
        private readonly INavigation _navigation;
        private readonly NameAndPassword _nameAndPassword;

        public NewsFeedViewModel([NotNull] IPostRepository postRepository, [NotNull] INavigation navigation, [NotNull] NameAndPassword nameAndPassword)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _nameAndPassword = nameAndPassword ?? throw new ArgumentNullException(nameof(nameAndPassword));
            UpdatePostsCommand = new Command(async () => await UpdatePosts());
            UpdatePostsCommand.Execute(null);
            GoToPublicationPageCommand = new Command(async() => await GoToPublicationPage());
        }

        public async Task UpdatePosts()
        {
            Posts.Clear();
            foreach (var post in await _postRepository.GetMostRecentPostsInOrder(_nameAndPassword))
                Posts.Add(post);
            IsRefreshing = false;
        }

        private async Task GoToPublicationPage()
        {
            await _navigation.GoToPage<PublicationPage>(_nameAndPassword);
        }
    }
}