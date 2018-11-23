using Missio.Users;
using Ninject;
using Ninject.Syntax;
using Ninject.Parameters;

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

        /// <inheritdoc />
        public T MakePage<T>(NameAndPassword nameAndPassword)
        {
            var constructorArgument = new ConstructorArgument(nameof(nameAndPassword), nameAndPassword, true);
            return _resolutionRoot.Get<T>(constructorArgument);
        }
    }
}