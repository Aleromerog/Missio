using System;
using Xamarin.Forms;

namespace Missio.NewsFeed
{
    public class NewsFeedPostDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TextOnlyPostTemplate { get; set; }
        public DataTemplate StickyPostTemplate { get; set; }

        /// <inheritdoc />
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is TextOnlyPost)
                return TextOnlyPostTemplate;
            if (item is StickyPost)
                return StickyPostTemplate;
            throw new ArgumentException(nameof(item));
        }
    }
}