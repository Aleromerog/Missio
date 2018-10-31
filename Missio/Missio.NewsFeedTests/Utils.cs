using Missio.Posts;
using Missio.Users;

namespace Missio.NewsFeedTests
{
    public static class Utils
    {
        public static Post MakeDummyPost()
        {
            return new Post(new User("Francisco Greco", "HashedElPass", null, "email@gmail.com"), "A message");
        }
    }
}