using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services
{
    public class ShoppingApiClient : IShoppingApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ShoppingApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        

     
        
    }
}