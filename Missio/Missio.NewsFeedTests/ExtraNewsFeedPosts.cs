using System.Collections.Generic;
using Missio.Posts;
using Missio.UserTests;

namespace Missio.NewsFeedTests
{
    public class ExtraNewsFeedPosts
    {
        public static readonly object[] ExtraPosts =
        {
            new List<IPost> {new TextOnlyPost(UserTestUtils.FranciscoUser.UserName, "Hello there")},
            new List<IPost> {new TextOnlyPost(UserTestUtils.FranciscoUser.Password, "<3")},
        };
    }
}