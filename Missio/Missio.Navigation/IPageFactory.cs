using Domain;

namespace Missio.Navigation
{
    public interface IPageFactory
    {
        T MakePage<T>();
        T MakePage<T>(NameAndPassword nameAndPassword);
    }
}