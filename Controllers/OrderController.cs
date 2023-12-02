using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO;
using BooksAPI.Models.DTO.OrderDTO;
using BooksAPI.Models.DTO.ShoppingItemCartDTO;
using BooksAPI.Repositories.Implementation;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public OrderController(IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository)
    {
        _orderRepository = orderRepository;
        _shoppingCartRepository = shoppingCartRepository;
    }

    [HttpPost("CreateOrderFromCart/{userId}")]
    public async Task<IActionResult> CreateOrderFromCart(Guid userId)
    {
        var cart = await _shoppingCartRepository.GetCartByUserId(userId);
        if (cart == null || cart.Items.Count == 0)
        {
            return BadRequest("Shopping Cart is empty or not found");
        }

        var order = await _orderRepository.CreateOrderFromCartAsync(userId, cart);
        if (order == null)
        {
            return BadRequest("Failed to create order");
        }
        
        await _shoppingCartRepository.DeleteCartByUserId(userId);

        return Ok(order);
    }
    
    [HttpGet("AllOrders")]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpGet("OrderById/{user}")]
    public async Task<IActionResult> GetById(Guid user)
    {
        try
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(user);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}