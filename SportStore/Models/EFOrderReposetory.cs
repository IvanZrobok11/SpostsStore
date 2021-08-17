using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFOrderReposetory : IOrderReposetory
    {
        private readonly ApplicationDbContext context;

        public EFOrderReposetory(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> Orders
        {
            get => context.Orders.Include(o => o.Lines)
                .ThenInclude(l => l.Product);
        }

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
        
    }

}
