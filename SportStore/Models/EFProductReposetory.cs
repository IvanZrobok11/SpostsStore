using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFProductReposetory : IProductReposetory
    {
        private ApplicationDbContext _context;
        public EFProductReposetory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products
        {
            get => _context.Products;
        }
    }
}
