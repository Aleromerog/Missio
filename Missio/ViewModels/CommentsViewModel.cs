using Domain;
using JetBrains.Annotations;

namespace ViewModels
{
    public interface ICommentsViewModel
    {
        Post Post { get; set; }
    }

    public class CommentsViewModel : ViewModel, ICommentsViewModel
    {
        private Post _post;

        [UsedImplicitly]
        public Post Post
        {
            get => _post;
            set => SetField(ref _post, value);
        }
    }
}