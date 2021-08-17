using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class FakeProductReposetory : IProductReposetory
    {
        public IQueryable<Product> Products => new List<Product> 
        { 
            new Product{Name = "bool",Price = 23.4m},
            new Product{Name = "top",Price = 1.9m},
            new Product{Name = "netbook",Price = 453.6m},
            new Product{Name = "pen",Price = 3.4m},
            new Product{Name = "beg",Price = 2m},

        }.AsQueryable<Product>();
    }
}
