using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Components
{
    public class NavigationMenu : ViewComponent
    {
        private IProductReposetory reposetory;
        public NavigationMenu(IProductReposetory reposetory)
        {
            this.reposetory = reposetory;
        }
        public IViewComponentResult Invoke(string category)
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            return View(reposetory.Products.Select(x=>x.Category).
                Distinct().//вертає всі категорії без повторення
                OrderBy(x=>x));
        }
    }
}
