using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ProductVm>> GetProducts(){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
    }
}