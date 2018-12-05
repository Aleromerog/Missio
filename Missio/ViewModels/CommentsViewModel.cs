using Domain;
using JetBrains.Annotations;

namespace ViewModels
{
    public class CommentsViewModel
    {
        [UsedImplicitly]
        public Post Post { get; }

        public CommentsViewModel(Post post)
        {
            Post = post;
        }
    }
}