using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
                _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
                "select * from coupon where productname = @ProductName", new { ProductName = productName });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Description = "No discount description", Amount = 0 };


            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
                _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var created = await connection.ExecuteAsync(
                "Insert into coupon(productname, description, amount) values(@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (created == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(
               _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var deleted = await connection.ExecuteAsync("delete from coupon  where ProductName = @ProductName",
                new { ProductName = productName });

            if (deleted == 0)
                return false;

            return true;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(
               _configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var updated = await connection.ExecuteAsync(
                "Update coupon set ProductName= @ProductName, Description= @Description, Amount= @Amount where id = @id",
                 new { Id = coupon.ID, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (updated == 0)
                return false;

            return true;
        }
    }
}
