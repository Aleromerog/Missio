﻿using System.Collections.Generic;
using Missio.Posts;
using Missio.Users;

namespace Missio.LocalDatabase
{
    public interface IPostRepository
    {
        void PublishPost(IPost post);
        List<IPost> GetMostRecentPostsInOrder(User user);
    }
}