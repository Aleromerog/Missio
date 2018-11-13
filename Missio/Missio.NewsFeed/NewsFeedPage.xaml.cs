using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Missio.Posts;
using Xamarin.Forms.Xaml;

namespace Missio.NewsFeed
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once MismatchedFileName
	public partial class NewsFeedPage
	{
        public NewsFeedPage()
	    {
            BindingContext = new ListModel();

            InitializeComponent();
	    }

        [UsedImplicitly]
	    public NewsFeedPage (NewsFeedViewModel newsFeedViewModel)
		{
            BindingContext = newsFeedViewModel;
			InitializeComponent ();
		}

        public class ListModel{
            static System.DateTime time = System.DateTime.Now;

            public List<IPost> Posts { get; set; } = new List<IPost>{
                new Post(new Users.User("Jorge", null, "Som@mail.com"), "El mensaje inspirador", time, null)
            };
        }
	}
}