using Domain;
using Missio.Navigation;
using Ninject.Parameters;
using Ninject.Syntax;
using ViewModels.Views;

namespace ViewModels.Factories
{
    public class CommentsPageFactory : PageFactory<CommentsPage, Post>, ICommentsPageFactory
    {
        /// <inheritdoc />
        public CommentsPageFactory(INavigation navigation, IResolutionRoot resolutionRoot) : base(navigation, resolutionRoot)
        {
        }

        /// <inheritdoc />
        protected override IParameter[] GetArguments(Post parameter)
        {
            return new IParameter[]
            {
                new ConstructorArgument("post", parameter, true)
            };
        }
    }
}