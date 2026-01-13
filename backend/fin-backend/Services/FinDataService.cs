using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using fin_backend.Models.FinModels;
using fin_backend.Models.DTOs;

namespace fin_backend.Services
{
    public class FinDataService : IFinDataService
    {
        private readonly HttpClient httpClient;
        private readonly string apikey;
        private const string baseUrl = "https://finnhub.io/api/v1";
        private readonly IConfiguration _config;

        // This class will handle all the business logic for the financial data
        // It will interact with the repositories to get and save data
        // It will also handle any calculations or transformations needed

        public FinDataService(IConfiguration config)   
        {
            _config = config;
            apikey = _config["FinHubKey"];
            httpClient = new HttpClient();
        }

        public async Task<List<Symbol>> SearchForSymbol(string symbol)
        {
            var url = $"{baseUrl}/search?q={symbol}&token={apikey}";
            var response = await httpClient.GetFromJsonAsync<SymbolResponse>(url);
            List<Symbol> results = response.result.ToList();
            return results;
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

            var raw = await httpClient.GetStringAsync(url);
            Console.WriteLine(raw.Substring(0, 200));


            var response = await httpClient.GetFromJsonAsync<List<NewsItemDTO>>(url);

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
    }
    




}
