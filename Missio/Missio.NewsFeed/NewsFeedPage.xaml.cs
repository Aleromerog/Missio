using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Missio.NewsFeed;
using Missio.Posts;
using Missio.Users;
using Xamarin.Forms.Xaml;

namespace Missio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	// ReSharper disable once MismatchedFileName
	public partial class NewsFeedPage
	{
        public ObservableCollection<IPost> Posts { get; } = new ObservableCollection<IPost>();

        public NewsFeedPage()
	    {
            Posts.Add(new TextAndImagePost(new User("Ana Gaxiola", "AmoAJorge", "https://scontent.fntr6-1.fna.fbcdn.net/v/t1.0-9/36355412_2372799529413493_5210179261469556736_n.jpg?_nc_cat=0&oh=43acb1611fbedb33963ced1540d79c94&oe=5BFC87A5"),
                                            "Hola amigos", "https://scontent.fntr6-1.fna.fbcdn.net/v/t1.0-9/39442197_2476652019028243_8630219233457864704_n.jpg?_nc_cat=0&oh=49664b70ae34ad2d95b88533209d9a19&oe=5C0280F4")
                     );

            InitializeComponent();
	    }

        [UsedImplicitly]
	    public NewsFeedPage (NewsFeedViewModel newsFeedViewModel)
		{
            BindingContext = newsFeedViewModel;
			InitializeComponent ();
		}
	}
}