using System.Threading.Tasks;

namespace ViewModels.Factories
{
    public interface IPageFactory<T, TParameter>
    {
        T CreatePage(TParameter parameter);
        Task<T> CreateAndNavigateToPage(TParameter parameter);
    }

    public interface IPageFactory<T, TParameter, TParameter2>
    {
        T CreatePage(TParameter parameter, TParameter2 parameter2);
        Task<T> CreateAndNavigateToPage(TParameter parameter, TParameter2 parameter2);
    }
}