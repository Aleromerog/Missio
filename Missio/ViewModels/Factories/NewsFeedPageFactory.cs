using Domain;
using Missio.Navigation;
using Ninject.Parameters;
using Ninject.Syntax;
using ViewModels.Views;

namespace ViewModels.Factories
{
    public class NewsFeedPageFactory : PageFactory<NewsFeedPage, NameAndPassword>, INewsFeedPageFactory
    {
        /// <inheritdoc />
        public NewsFeedPageFactory(INavigation navigation, IResolutionRoot resolutionRoot) : base(navigation, resolutionRoot)
        {
        }

        /// <inheritdoc />
        protected override IParameter[] GetArguments(NameAndPassword parameter)
        {
            return new IParameter[] {new ConstructorArgument("nameAndPassword", parameter, true)};
        }
    }
}