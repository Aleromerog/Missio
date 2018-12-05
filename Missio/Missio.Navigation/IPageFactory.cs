namespace Missio.Navigation
{
    public interface IPageFactory
    {
        T MakePage<T>();
    }
}