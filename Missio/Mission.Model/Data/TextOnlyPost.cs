using System;
using JetBrains.Annotations;

namespace Mission.Model.Data
{
    public class TextOnlyPost : NewsFeedPost
    {
        public string Author { [UsedImplicitly] get; }
        public string Text { [UsedImplicitly] get; }

        public TextOnlyPost([NotNull] string author, [NotNull] string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }
    }
}