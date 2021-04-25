using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Share;
using CustomerSite.Services.Interface;

namespace CustomerSite.Services.API
{
    public class ProductApiClient : IProductApiClient

    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRatingApiClient _ratingApiClient;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IRatingApiClient ratingApiClient, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _ratingApiClient = ratingApiClient;
            _configuration = configuration;
        }

        public async Task<IList<ProductVm>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/products");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
        public async Task<ProductVm> GetProductById(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/products/" + id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IEnumerable<ProductVm>> GetCateByProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/products");
            var pro = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/products/" + id);
            response.EnsureSuccessStatusCode();
            pro.EnsureSuccessStatusCode();
            IList<ProductVm> products = await response.Content.ReadAsAsync<IList<ProductVm>>();
            ProductVm product = await pro.Content.ReadAsAsync<ProductVm>();
            return products.Where(x => x.CategoryID == product.CategoryID && x.ProductID != product.ProductID);

        }
        public async Task<IEnumerable<ProductVm>> GetProductByCate(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/products");
            response.EnsureSuccessStatusCode();
            IList<ProductVm> productByCate = await response.Content.ReadAsAsync<IList<ProductVm>>();
            return productByCate.Where(x => x.CategoryID == id);

        }

        public async Task<IEnumerable<ProductVm>> GetProductByName(string name)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/products");
            response.EnsureSuccessStatusCode();
            IList<ProductVm> productByName = await response.Content.ReadAsAsync<IList<ProductVm>>();
            return productByName.Where(x => x.ProductName.StartsWith(name, StringComparison.OrdinalIgnoreCase));

        }


        public async Task PuttRatingProduct(int ProId, ProductFormVm model)
        {
            var client = new HttpClient();
            double avr = await _ratingApiClient.FindRatingByProduct(ProId);
            var product = await GetProductById(ProId);
            model = new ProductFormVm()
            {
                Name = product.ProductName,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                RatingAVG = avr,
                CategoryID = product.CategoryID
            };
            await client.PutAsync(_configuration["local:BackEndUrl"] + "api/products/" + ProId, new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

        }





    }
}