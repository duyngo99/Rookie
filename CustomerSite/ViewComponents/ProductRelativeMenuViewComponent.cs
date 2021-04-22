using System.Threading.Tasks;
using CustomerSite.Services;
using CustomerSite.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class ProductRelativeMenuViewComponent : ViewComponent
    {
        private readonly IProductApiClient _productApiClient;
        public ProductRelativeMenuViewComponent(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id){
            var product = await _productApiClient.GetCateByProduct(id);
            return View(product);
        }
    }
}