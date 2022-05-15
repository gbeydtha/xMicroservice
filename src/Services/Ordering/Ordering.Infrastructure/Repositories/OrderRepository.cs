using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistance;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async  Task<IEnumerable<Order>> GetOrderByUserName(string username)
        {
            var orderList = await _dbContext.Orders
                .Where(u => u.UserName == username)
                .ToListAsync();

            return orderList; 
        }
    }
}
