using JetBrains.Annotations;

namespace Domain
{
    public class Comment
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public string Text { get; private set; }
        
        [UsedImplicitly]
        public User Author { get; private set; }

        private Comment()
        {
        }

        public Comment(string text, User author)
        {
            Text = text;
            Author = author;
        }
    }
}