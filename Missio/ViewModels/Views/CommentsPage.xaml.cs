using Xamarin.Forms.Xaml;

namespace ViewModels.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CommentsPage
	{
		public CommentsPage ()
		{
			InitializeComponent ();
		}

	    public CommentsPage(CommentsViewModel commentsViewModel)
	    {
	        BindingContext = commentsViewModel;
            InitializeComponent();
	    }
	}
}