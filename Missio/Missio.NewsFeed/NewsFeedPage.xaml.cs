using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using JetBrains.Annotations;
using Missio.Posts;
using Missio.Users;
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

            public List<IPost> Posts { get; set; } = new List<IPost>();

            public ListModel()
            {
                byte[] userImage;
                using (var webClient = new WebClient())
                {
                    userImage = webClient.DownloadData("TuURL");
                }

                Posts.Add(new Post(new User("Jorge", userImage, "Som@mail.com"), "El mensaje inspirador", time, null));
            }
        }
	}
}