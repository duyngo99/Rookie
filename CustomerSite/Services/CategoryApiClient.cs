using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Share;

namespace CustomerSite.Services
{
    public class CategoryApiClient : ICategoryApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<CategoryVm>> GetCategories()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["BackendUrl:Default"] + "/api/categories");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}