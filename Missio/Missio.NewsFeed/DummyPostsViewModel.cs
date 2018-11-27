using System;
using System.Collections.Generic;
using Missio.Posts;
using Missio.Users;

namespace Missio.NewsFeed
{
    public class DummyPostsViewModel
    {
        public List<IPost> Posts { get; set; } = new List<IPost>
        {
            new Post(new User("Jorge", new byte[0], "Som@mail.com"), "El mensaje cambiooos inspirador", new DateTime(2011, 2, 3, 4, 4, 5))
        };
    }
}