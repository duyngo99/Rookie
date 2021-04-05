using System.Collections.Generic;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services
{
    public interface IProductApiClient
    {
         Task<IList<ProductVm>> GetProducts ();
    }
}