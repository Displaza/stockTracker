using fin_backend.Models.FinModels;
using fin_backend.Services;
using fin_backend.Repos;

namespace fin_backend.BackgroundWorkers
{
    public class NewsUpdateWorker : BackgroundService
    {
        private readonly ILogger<NewsUpdateWorker> _logger;
        //private IFinDataService _finDataService;
        //private IFinNewsRepo _finNewsRepo;
        private readonly IServiceScopeFactory _scopeFactory;

        public NewsUpdateWorker(ILogger<NewsUpdateWorker> logger,
                                IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            //_finDataService = finDataService;
            //_finNewsRepo = finNewsRepo;
            _scopeFactory = scopeFactory;   
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("News Update Worker running at: {time}", DateTimeOffset.Now);
                try
                {

                    //now we will create a scope that can use scoped services INSIDE this singleton
                    using IServiceScope scope = _scopeFactory.CreateScope();
                    IFinDataService injectedFinDataService = scope.ServiceProvider.GetRequiredService<IFinDataService>();
                    IFinNewsRepo injectedFinNewsRepo = scope.ServiceProvider.GetRequiredService<IFinNewsRepo>();

                    //should think about the stopping tokens in this async task
                    List<NewsItem> newsItems = await injectedFinDataService.InputNews("general", null);
                    await injectedFinNewsRepo.AddNewsItemRange(newsItems);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating news.");
                }
                // Wait for 1 hour before next update       
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
