using Ninject;
using Ninject.Syntax;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class PageFactory : IPageFactory
    {
        private readonly IResolutionRoot _resolutionRoot;

        public PageFactory(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        /// <inheritdoc />
        public T MakePage<T>() where T : Page
        {
            return _resolutionRoot.Get<T>();    
        }
    }
}