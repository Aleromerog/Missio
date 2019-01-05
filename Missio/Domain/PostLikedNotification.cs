using System;
using JetBrains.Annotations;

namespace Domain
{
    public class PostLikedNotification
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public User UserToBeNotified { get; private set; }

        [UsedImplicitly]
        public User UserWhichLikedThePost { get; private set; }

        [UsedImplicitly]
        public Post PostThatWasLiked { get; private set; }

        [UsedImplicitly]
        public DateTime TimeLiked { get; set; }

        private PostLikedNotification()
        {
        }

        public PostLikedNotification([NotNull] User userWhichLikedThePost, [NotNull] User userToBeNotified, [NotNull] Post postThatWasLiked)
        {
            UserWhichLikedThePost = userWhichLikedThePost ?? throw new ArgumentNullException(nameof(userWhichLikedThePost));
            UserToBeNotified = userToBeNotified ?? throw new ArgumentNullException(nameof(userToBeNotified));
            PostThatWasLiked = postThatWasLiked ?? throw new ArgumentNullException(nameof(postThatWasLiked));
        }
    }
}