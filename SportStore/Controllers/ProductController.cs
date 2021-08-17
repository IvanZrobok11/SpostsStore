using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.Models;
using SportStore.Models.ViewModels;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        private IProductReposetory reposetory;
        public ProductController(IProductReposetory reposetory)
        {
            this.reposetory = reposetory;
        }
        public int PageSize = 4;
        public ViewResult List(string category,int productPage = 1)
        {
            return View(new ProductsListViewModel
            {
                PagingInfo = new PagingInfo
                {        
                    CurrentPage = productPage,
                    ItemPerPage = PageSize,
                    TotalItem = category == null ? reposetory.Products.Count() :
                    reposetory.Products.Where(x=>x.Category == category).Count()
                },
                Products = reposetory.Products
                .Where(p=>category == null || p.Category == category)
                .OrderBy(x => x.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                CurrentCategory = category
            });

        }
    }
}

