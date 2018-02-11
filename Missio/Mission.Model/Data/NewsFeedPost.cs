namespace Mission.Model.Data
{
    public class NewsFeedPost
    {
        public readonly PostAuthor PostAuthor;
        public readonly string Content;
        public readonly int AmountOfLikes;

        public NewsFeedPost(PostAuthor postAuthor, string content, int amountOfLikes)
        {
            PostAuthor = postAuthor;
            Content = content;
            AmountOfLikes = amountOfLikes;
        }
    }

    public class PostAuthor
    {
        public string Name;

        public PostAuthor(string name)
        {
            Name = name;
        }
    }
}