namespace Mission.Model.Data
{
    public class NewsFeedPost
    {
    }

    public class TextOnlyPost : NewsFeedPost
    {
        public string Author { get; }
        public string Text { get; }

        public TextOnlyPost(string author, string text)
        {
            Text = text;
            Author = author;
        }
    }

    public class StickyPost : NewsFeedPost
    {
        public string Title { get; }
        public string Message { get; }

        public StickyPost(string title, string message)
        {
            Message = message;
            Title = title;
        }
    }
}