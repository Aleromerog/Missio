using System.Collections.Generic;
using Missio.NewsFeed;

namespace Missio.Tests
{
    public class ExtraNewsFeedPosts
    {
        public static readonly object[] ExtraPosts =
        {
            new List<NewsFeedPost> {new TextOnlyPost("Francisco Greco", "Hello there")},
            new List<NewsFeedPost> {new TextOnlyPost("Jorge Romero", "<3")},
        };
    }
}