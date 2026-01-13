using fin_backend.Models.FinModels;

namespace fin_backend.Repos
{
    public interface IFinNewsRepo
    {
        Task AddNewsItemRange(List<NewsItem> newsItems);
    }
}
