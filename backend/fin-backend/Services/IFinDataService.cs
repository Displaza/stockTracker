using fin_backend.Models.FinModels;

namespace fin_backend.Services
{
    public interface IFinDataService
    {
        Task<List<Symbol>> SearchForSymbol(string symbol);
        //Task<string> SearchForSymbol(string symbol);

        Task<List<NewsItem>> GetNews(string newsType, int? minId);
    }
}
