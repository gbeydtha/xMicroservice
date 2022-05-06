using Basket.API.Entities;
using Basket.API.GrpcService;
using Basket.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _respository;
        private readonly DisocuntGrpcService _disocuntGrpcService;

        public BasketController(IBasketRepository respository, DisocuntGrpcService disocuntGrpcService)
        {
            _respository = respository ?? throw new ArgumentNullException(nameof(respository));
            _disocuntGrpcService = disocuntGrpcService ?? throw new ArgumentNullException(nameof(disocuntGrpcService));
        }

        [HttpGet("{username}", Name = "GetBasket")]
        [ProducesResponseType(typeof(IEnumerable<ShoppingCart>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>>  GetBasket(string username)
        {
            var basket = await _respository.GetBasket(username);
            return Ok(basket ?? new ShoppingCart(username)); 
        }

        [HttpPost]
       // [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //TODO : Communicate Discount.Grpc
            // Calculate the new total price 
            // consume Grpc
            foreach(var item in basket.Items)
            {
                var coupon = await _disocuntGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount; 
            }

             return Ok(await _respository.UpdateBasket(basket));
        }

       
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _respository.DeleteBasket(userName); 
            return Ok();
        }
    }
}
