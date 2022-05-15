using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfigureOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with Context {DbContextName}", typeof(OrderContext).Name);
            }
        }
        private static IEnumerable<Order> GetPreconfigureOrders()
        {
            return new List<Order>
            {
                new Order()
                {
                    UserName= "swn", FirstName = "Abul", LastName = "Hasan",
                    EmailAddress="abul.hasan.de@gmail.com", AddressLine="Nuremberg",
                    Country="Germany", TotalPrice=300
                }
            };
        }
    }
}
