using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Share;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using CustomerSite.Services.Interface;
namespace CustomerSite.Services.API
{
    public class RatingApiClient : IRatingApiClient
    {
        private readonly IConfiguration _configuration;

        private readonly IHttpClientFactory _httpClientFactory;

        public RatingApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IList<RatingVm>> GetRating()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/ratings");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<RatingVm>>();

        }

        public async Task PostRatingByID(int ProId, string UserName, double RatingText)
        {

            var client = new HttpClient();
            RatingVm jsonInString = new RatingVm { ProductID = ProId, UserName = UserName, RatingText = RatingText };
            await client.PostAsync(_configuration["local:BackEndUrl"] + "api/ratings", new StringContent(JsonConvert.SerializeObject(jsonInString), Encoding.UTF8, "application/json"));

        }
        public async Task<RatingVm> SearchRating(int ProId, string UserName)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/ratings");
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            return ratingVms.FirstOrDefault(x => x.ProductID == ProId && x.UserName == UserName);
        }


        public async Task RemoveRating(int ProId, string UserName)
        {
            RatingVm ratingVms = await SearchRating(ProId, UserName);
            var client = new HttpClient();
            await client.DeleteAsync(_configuration["local:BackEndUrl"] + "api/ratings/" + ratingVms.Id);


        }

        public async Task<double> FindRatingByProduct(int ProId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["local:BackEndUrl"] + "api/ratings");
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            var ratings = ratingVms.Where(x => x.ProductID == ProId);
            double average = 0;
            double tong = 0;
            int count = 0;
            foreach (var item in ratings)
            {
                count++;
                tong += item.RatingText;
            }
            average = tong / count;
            return average;
        }




    }
}