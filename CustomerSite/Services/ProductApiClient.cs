using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services
{
    public class ProductApiClient :IProductApiClient
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
        public async Task<ProductVm> GetProductById(int id){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/products/"+id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }

         public async Task<IEnumerable<ProductVm>> GetCateByProduct(int id){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/products");
            var pro = await client.GetAsync("https://localhost:5001/api/products/"+id);
            response.EnsureSuccessStatusCode();
            pro.EnsureSuccessStatusCode();
            IList<ProductVm> products = await response.Content.ReadAsAsync<IList<ProductVm>>();
            ProductVm product= await pro.Content.ReadAsAsync<ProductVm>();
            return products.Where(x => x.CategoryID == product.CategoryID && x.ProductID!=product.ProductID);
            
        }
        public async Task<IEnumerable<ProductVm>> GetProductByCate(int id){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/products");
            response.EnsureSuccessStatusCode();
            IList<ProductVm> productByCate = await response.Content.ReadAsAsync<IList<ProductVm>>();
            return productByCate.Where(x => x.CategoryID == id);
            
        }

        public async Task<IEnumerable<ProductVm>> GetProductByName(string name){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/products");
            response.EnsureSuccessStatusCode();
            IList<ProductVm> productByName = await response.Content.ReadAsAsync<IList<ProductVm>>();
            return productByName.Where(x => x.ProductName.StartsWith(name, StringComparison.OrdinalIgnoreCase));
            
        }

        

    }
}