using System.Collections.Generic;
using Mission.Model.Data;

namespace Missio.Tests
{
    public class ExtraNewsFeedPosts
    {
        public static readonly object[] extraPosts =
        {
            new List<NewsFeedPost> {new TextOnlyPost("Francisco Greco", "Hello there")},
            new List<NewsFeedPost> {new TextOnlyPost("Jorge Romero", "<3")},
        };
    }
}