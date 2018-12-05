using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain;
using Domain.Repositories;
using JetBrains.Annotations;
using ViewModels.Factories;
using Xamarin.Forms;

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
        public ObservableCollection<IPost> Posts { get; } = new ObservableCollection<IPost>();

        private readonly IPostRepository _postRepository;
        private readonly IPublicationPageFactory _publicationPageFactory;
        private readonly NameAndPassword _nameAndPassword;

        public NewsFeedViewModel([NotNull] IPostRepository postRepository, [NotNull] IPublicationPageFactory publicationPageFactory, ICommentsPageFactory commentsPageFactory, [NotNull] NameAndPassword nameAndPassword)
        {
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _nameAndPassword = nameAndPassword ?? throw new ArgumentNullException(nameof(nameAndPassword));
            _publicationPageFactory = publicationPageFactory ?? throw new ArgumentNullException(nameof(publicationPageFactory));
            GoToCommentsCommand = new Command<Post>(post => commentsPageFactory.CreateAndNavigateToPage(post));
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
            await _publicationPageFactory.CreateAndNavigateToPage(_nameAndPassword, async() => await UpdatePosts());
        }
    }
}