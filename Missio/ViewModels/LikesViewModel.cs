using Domain;
using JetBrains.Annotations;

namespace ViewModels
{
    public interface ILikesViewModel
    {
        Post Post { get; set; }
    }

    public class LikesViewModel : ILikesViewModel
    {
        [UsedImplicitly]
        public Post Post { get; set; } 
    }
}