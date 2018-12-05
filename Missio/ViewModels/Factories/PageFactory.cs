using System.Threading.Tasks;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using Xamarin.Forms;
using INavigation = Missio.Navigation.INavigation;

namespace ViewModels.Factories
{
    public abstract class PageFactory<T, TParameter> : PageFactoryBase, IPageFactory<T, TParameter> where T : Page
    {
        /// <inheritdoc />
        protected PageFactory(INavigation navigation, IResolutionRoot resolutionRoot) : base(navigation, resolutionRoot)
        {
        }

        protected abstract IParameter[] GetArguments(TParameter parameter);

        public T CreatePage(TParameter parameter)
        {
            var arguments = GetArguments(parameter);
            return ResolutionRoot.Get<T>(arguments);
        }

        public async Task<T> CreateAndNavigateToPage(TParameter parameter)
        {
            var page = CreatePage(parameter);
            await Navigation.GoToPage(page);
            return page;
        }
    }

    public abstract class PageFactory<T, TParameter, TParameter2> : PageFactoryBase, IPageFactory<T, TParameter, TParameter2> where T : Page
    {
        /// <inheritdoc />
        protected PageFactory(INavigation navigation, IResolutionRoot resolutionRoot) : base(navigation, resolutionRoot)
        {
        }

        public abstract IParameter[] GetArguments(TParameter parameter, TParameter2 parameter2);

        public T CreatePage(TParameter parameter, TParameter2 parameter2)
        {
            var arguments = GetArguments(parameter, parameter2);
            return ResolutionRoot.Get<T>(arguments);
        }

        public async Task<T> CreateAndNavigateToPage(TParameter parameter, TParameter2 parameter2)
        {
            var page = CreatePage(parameter, parameter2);
            await Navigation.GoToPage(page);
            return page;
        }
    }
}