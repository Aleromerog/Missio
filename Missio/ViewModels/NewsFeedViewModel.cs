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
    public class NewsFeedViewModel : ViewModel
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
        public ICommand GoToCommentsCommand { get; }

        [UsedImplicitly]
        public ICommand GoToLikesCommand { get; }

        [UsedImplicitly]
        public ObservableCollection<IPost> Posts { get; } = new ObservableCollection<IPost>();

        private readonly IPostRepository _postRepository;
        private readonly INavigation _navigation;
        private readonly INameAndPasswordService _nameAndPasswordService;

        public NewsFeedViewModel([NotNull] IPostRepository postRepository, INavigation navigation, INameAndPasswordService nameAndPasswordService)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _navigation = navigation;
            _nameAndPasswordService = nameAndPasswordService;
            GoToCommentsCommand = new Command<Post>(async post =>
            {
                var commentsViewModel = await navigation.GoToPage<CommentsPage, ICommentsViewModel>();
                commentsViewModel.Post = post;
            });
            GoToLikesCommand = new Command<Post>(async post =>
            {
                var likesViewModel = await navigation.GoToPage<LikesPage, ILikesViewModel>();
                likesViewModel.Post = post;
            });
            UpdatePostsCommand = new Command(async () => await UpdatePosts());
            GoToPublicationPageCommand = new Command(async() => await GoToPublicationPage());
            UpdatePostsCommand.Execute(null);
        }

        public async Task UpdatePosts()
        {
            Posts.Clear();
            foreach (var post in await _postRepository.GetMostRecentPostsInOrder(_nameAndPasswordService.NameAndPassword))
                Posts.Add(post);
            IsRefreshing = false;
        }

        private async Task GoToPublicationPage()
        {
            var publicationPageViewModel = await _navigation.GoToPage<PublicationPage, IPublicationPageViewModel>();
            publicationPageViewModel.PostPublishedEvent+= async (s, e) => await UpdatePosts();
        }
    }
}