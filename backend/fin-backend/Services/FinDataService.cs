using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using fin_backend.Models.FinModels;
using fin_backend.Models.DTOs;
using fin_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace fin_backend.Services
{
    public class FinDataService : IFinDataService
    {
        private readonly HttpClient httpClient;
        private readonly string apikey;
        private const string baseUrl = "https://finnhub.io/api/v1";
        private readonly IConfiguration _config;
        private readonly FinDbContext _context;

        // This class will handle all the business logic for the financial data
        // It will interact with the repositories to get and save data
        // It will also handle any calculations or transformations needed

        public FinDataService(IConfiguration config, FinDbContext context)   
        {
            _config = config;
            apikey = _config["FinHubKey"];
            httpClient = new HttpClient();
            _context = context;
        }

        public async Task<List<Symbol>> SearchForSymbol(string symbol)
        {
            try
            {
                var url = $"{baseUrl}/search?q={symbol}&token={apikey}";
                var response = await httpClient.GetFromJsonAsync<SymbolResponse>(url);

                if (response == null || response.result == null)
                {
                    return new List<Symbol>();
                }

                List<Symbol> results = response.result.ToList();
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SearchForSymbol: {ex.Message}");
                throw;
            }
        }

        public async Task<List<NewsItem>> InputNews(string newsType, int? minId)
        {
            if(newsType == null)
            {
                throw new ArgumentNullException(nameof(newsType));
            }

            string url = $"{baseUrl}/news?category={newsType}&token={apikey}";

            if (minId != null)
            {
                url += $"&minId={minId}";
            }

            try
            {
                var response = await httpClient.GetFromJsonAsync<List<NewsItemDTO>>(url);

                if (response == null)
                {
                    return new List<NewsItem>();
                }

                List<NewsItem> cleanEntities = response.Select(dto => new NewsItem
                {
                    category = dto.Category,
                    datetime = dto.Datetime.ToString(),
                    headline = dto.Headline,
                    image = dto.Image,
                    related = dto.Related,
                    source = dto.Source,
                    summary = dto.Summary,
                    url = dto.Url
                }).ToList();

                return cleanEntities;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching news: {ex.Message}");
                throw;
            }


            
        }

        public async Task<IOrderedQueryable<NewsItem>> GetPaginatedNews(int pageNumber, int pageSize)
        {
            //Do I even want to have a data repo for this? Will think on this later
            //TODO: Data repo considerations
            var query = _context.NewsItems.OrderByDescending(n => n.NewsItemId);

            return query;

            //var totalCount = await query.CountAsync();


            //throw new NotImplementedException();
        }

    }
    




}
