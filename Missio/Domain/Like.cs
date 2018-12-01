using JetBrains.Annotations;

namespace Domain
{
    public class Like
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public User Author { get; private set; }

        private Like()
        {
        }

        public Like(User author)
        {
            Author = author;
        }
    }
}