using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Share;
using Microsoft.AspNetCore.Http.Extensions; 

namespace CustomerSite.Services
{
    public class RatingApiClient : IRatingApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RatingApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<RatingVm>> GetRating(){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/ratings");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<RatingVm>>();

        }

        public async Task PostRatingByID(int ProId,string UserName,int RatingText){
            var client = new HttpClient();
            RatingVm jsonInString= new RatingVm{ProductID=ProId,UserName=UserName,RatingText=RatingText};
            await client.PostAsync("https://localhost:5001/api/ratings", new StringContent(JsonConvert.SerializeObject(jsonInString), Encoding.UTF8, "application/json"));
            
        }
        public async Task<RatingVm> SearchRating(int ProId,string UserName){
             var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/ratings");
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            return ratingVms.FirstOrDefault(x => x.ProductID == ProId && x.UserName == UserName);
        }


        public async Task RemoveRating(int ProId,string UserName){
            RatingVm ratingVms= await SearchRating(ProId,UserName);
            var client = new HttpClient();
            await client.DeleteAsync("https://localhost:5001/api/ratings/"+ratingVms.Id);
             
             
        }







    }
}