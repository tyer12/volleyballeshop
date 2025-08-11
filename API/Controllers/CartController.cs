using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CartController(ICartservice cartservice) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
    {
        var cart = await cartservice.GetCartAsync(id);

        return Ok(cart ?? new ShoppingCart { Id = id });
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
    {
        var updateCart = await cartservice.SetCartAsync(cart);

        if (updateCart == null) return BadRequest("Problem with cart");

        return updateCart;
    }

    [HttpDelete]
    public async Task<ActionResult<ShoppingCart>> DeleteCart(string id)
    {
        var result = await cartservice.DeleteCartAsync(id);

        if (!result) return BadRequest("Problem deleting cart");

        return Ok();
    } 
}
