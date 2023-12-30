using System.Net;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController: ControllerBase
{
    private readonly IBasketRepository _repository;
    private readonly DiscountGrpcService _discountGrpcService;
    public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
    }

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await _repository.GetBasket(userName);
        // Si no hay un carrito guardado, se crea uno nuevo
        return Ok(basket ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        // Vamos a llamar al Discount.Grpc para obtener descuentos. OJITO!
        // TODO: Communicate with Discount.Grpc and Calculate latest prices of product into shopping cart
        // 1 - Communicate
        //  1.1 Necesitamos instalar:
        //   - dotnet tool install -g dotnet-grpc
        //   - dotnet-grpc add-file path_to_file (que es el proto del otro proyecto)
        
        // 2 - Calculate latest prices
        //  2.1 Tenemos que crear una clase que se encargue de consumir el Grpc para el decoplamiento
        //  2.2 Obtenemos los descuentos de CADA item en el carrito
        foreach (var item in basket.Items)
        {   
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            // Recalculamos el precio
            item.Price -= coupon.Amount;
        }
        return Ok(await _repository.UpdateBasket(basket));
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]  
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _repository.DeleteBasket(userName);
        return Ok();
    }

}
