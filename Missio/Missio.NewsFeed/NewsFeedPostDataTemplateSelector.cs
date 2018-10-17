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
            switch (item)
            {
                case Post _:
                    return TextAndImagePost;
                case StickyPost _:
                    return StickyPostTemplate;
                default:
                    throw new ArgumentException(nameof(item));
            }
        }
    }
}