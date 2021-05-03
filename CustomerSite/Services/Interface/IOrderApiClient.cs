using System.Collections.Generic;
using System.Threading.Tasks;
using Share;

namespace CustomerSite.Services.Interface
{
    public interface IOrderApiClient
    {
         Task<IList<OrderVm>> GetOrders();
         Task PostOrders(OrderFormVm orderFormVm);
    }
}