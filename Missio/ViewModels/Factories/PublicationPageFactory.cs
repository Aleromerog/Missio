using System;
using Domain;
using Missio.Navigation;
using Ninject.Parameters;
using Ninject.Syntax;
using ViewModels.Views;

namespace ViewModels.Factories
{
    public class PublicationPageFactory : PageFactory<PublicationPage, NameAndPassword, Action>, IPublicationPageFactory
    {
        public PublicationPageFactory(INavigation navigation, IResolutionRoot resolutionRoot) : base(navigation, resolutionRoot)
        {
        }

        /// <inheritdoc />
        public override IParameter[] GetArguments(NameAndPassword parameter, Action parameter2)
        {
            return new IParameter[]
            {
                new ConstructorArgument("nameAndPassword", parameter, true),
                new ConstructorArgument("postPublishedCallBack", parameter2, true)
            };
        }
    }
}