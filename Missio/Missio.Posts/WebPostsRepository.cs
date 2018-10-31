using System;
using System.Collections.Generic;

namespace Missio.Posts
{
    public class WebPostsRepository : IPostRepository
    {
        /// <inheritdoc />
        public void PublishPost(CreatePostDTO post)
        {
        }

        /// <inheritdoc />
        public List<IPost> GetMostRecentPostsInOrder(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}