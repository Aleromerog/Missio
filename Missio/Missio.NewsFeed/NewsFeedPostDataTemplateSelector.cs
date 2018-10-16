using System;
using Missio.Posts;
using Xamarin.Forms;

namespace Missio.NewsFeed
{
    public class NewsFeedPostDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StickyPostTemplate { get; set; }
        public DataTemplate TextAndImagePost { get; set; }

        /// <inheritdoc />
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is Post)
                return TextAndImagePost;
            if (item is StickyPost)
                return StickyPostTemplate;
            throw new ArgumentException(nameof(item));
        }
    }
}