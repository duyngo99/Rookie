using System.Collections.Generic;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services.Interface
{
    public interface IProductApiClient
    {
         Task<IList<ProductVm>> GetProducts ();

         Task<ProductVm> GetProductById(int id);

         Task<IEnumerable<ProductVm>> GetProductByCate(int id);

         Task<IEnumerable<ProductVm>> GetProductByName(string name);

         Task<IEnumerable<ProductVm>> GetCateByProduct(int id);

         Task PuttRatingProduct(int ProId,ProductFormVm model);

    }
}