using Domain;
using Xamarin.Forms;

namespace ViewModels
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
                    return TextAndImagePost; // TODO: Find a smarter way to check when a json object is passed
            }
        }
    }
}