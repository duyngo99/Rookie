using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CustomerSite.Services.Interface;
using Microsoft.Extensions.Configuration;
using Share;
using Newtonsoft.Json;
using System.Text;

namespace CustomerSite.Services.API
{
    public class OrderApiClient : IOrderApiClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderApiClient(IConfiguration configuration,IHttpClientFactory httpClientFactory  )
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<OrderVm>> GetOrders()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/orders");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderVm>>();
        }

        public async Task PostOrders(OrderFormVm orderFormVm)
        {

            var client = new HttpClient();
            OrderFormVm jsonInString = new OrderFormVm { Amount = orderFormVm.Amount , Status = orderFormVm.Status , Username = orderFormVm.Username, Total = orderFormVm.Total  };
            await client.PostAsync(_configuration["local:BackEndUrl"] + "api/orders", new StringContent(JsonConvert.SerializeObject(jsonInString), Encoding.UTF8, "application/json"));

        }
    }
}