using Amado.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Amado.Views.Shared.Components.SearchResult
{
    public class SearchResultViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(List<Product> model)
        {
            return View(model);
        }
    }
}
