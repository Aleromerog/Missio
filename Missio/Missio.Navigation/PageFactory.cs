using Ninject;
using Ninject.Syntax;

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
        public T MakePage<T>()
        {
            return _resolutionRoot.Get<T>();    
        }
    }
}