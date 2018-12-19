﻿using Domain;
using JetBrains.Annotations;

namespace ViewModels
{
    public interface ICommentsViewModel
    {
        Post Post { get; set; }
    }

    public class CommentsViewModel : ICommentsViewModel
    {
        [UsedImplicitly]
        public Post Post { get; set; }
    }
}