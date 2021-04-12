using System.Collections.Generic;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services
{
    public interface IRatingApiClient
    {
        Task<IList<RatingVm>> GetRating();
        Task PostRatingByID(int ProId,string UserName,double RatingText);
        Task<RatingVm> SearchRating(int ProId,string UserName);

        Task RemoveRating(int ProId,string UserName);

         Task<double> FindRatingByProduct(int ProId);
    }
}