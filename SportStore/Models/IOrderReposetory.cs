using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public interface IOrderReposetory
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
