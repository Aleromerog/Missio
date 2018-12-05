using Missio.Navigation;
using Ninject.Syntax;

namespace ViewModels.Factories
{
    public abstract class PageFactoryBase
    {
        protected readonly INavigation Navigation;
        protected readonly IResolutionRoot ResolutionRoot;

        protected PageFactoryBase(INavigation navigation, IResolutionRoot resolutionRoot)
        {
            Navigation = navigation;
            ResolutionRoot = resolutionRoot;
        }
    }
}