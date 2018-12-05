using System;
using Domain;
using ViewModels.Views;

namespace ViewModels.Factories
{
    public interface IPublicationPageFactory : IPageFactory<PublicationPage, NameAndPassword, Action>
    {
    }
}