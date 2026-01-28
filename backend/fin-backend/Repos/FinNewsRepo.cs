using fin_backend.Data;
using fin_backend.Models.FinModels;

namespace fin_backend.Repos
{
    
    public class FinNewsRepo : IFinNewsRepo
    {
        private readonly FinDbContext _context;
        public FinNewsRepo(FinDbContext context) 
        {
            _context = context;
        }

        public async Task AddNewsItemRange(List<NewsItem> newsItems)
        {
            try {
                //wipe existing table
                _context.NewsItems.RemoveRange(_context.NewsItems);
                await _context.SaveChangesAsync();

                //add new data
                await _context.NewsItems.AddRangeAsync(newsItems);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding news items: {ex.Message}");
                throw;
            }

        }
    }
}
