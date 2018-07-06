using System.Collections.Generic;
using Missio.NewsFeed;

namespace Missio.NewsFeedTests
{
    public class ExtraNewsFeedPosts
    {
        public static readonly object[] ExtraPosts =
        {
            new List<IPost> {new TextOnlyPost("Francisco Greco", "Hello there")},
            new List<IPost> {new TextOnlyPost("Jorge Romero", "<3")},
        };
    }
}