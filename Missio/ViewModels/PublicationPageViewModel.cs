using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.DataTransferObjects;
using Domain.Repositories;
using JetBrains.Annotations;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModels
{
    public interface IPublicationPageViewModel
    {
        event EventHandler PostPublishedEvent;
    }

    public class PublicationPageViewModel : IPublicationPageViewModel
    {
        private readonly INameAndPasswordService _nameAndPasswordService;
        private readonly IPostRepository _postRepository;
        private readonly INavigation _navigation;
        private string _postText;
        public event EventHandler PostPublishedEvent;
        
        [UsedImplicitly]
        public ICommand PublishPostCommand { get; }

        [UsedImplicitly]
        public string PostText
        {
            get => _postText ?? "";
            set => _postText = value;
        }

        public PublicationPageViewModel([NotNull] IPostRepository postRepository, [NotNull] INavigation navigation, INameAndPasswordService nameAndPasswordService)
        {
            _nameAndPasswordService = nameAndPasswordService;
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            PublishPostCommand = new Command(async() => await PublishPost());
        }

        public async Task PublishPost()
        {
            await _postRepository.PublishPost(new CreatePostDTO(_nameAndPasswordService.NameAndPassword, PostText, null));
            PostPublishedEvent?.Invoke(this, EventArgs.Empty);
            await _navigation.ReturnToPreviousPage();
        }
    }
}