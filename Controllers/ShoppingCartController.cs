using BooksAPI.Models.Domain;
using BooksAPI.Models.DTO.AuthDTO;
using BooksAPI.Models.DTO.AuthorDto;
using BooksAPI.Models.DTO.BlogPostDto;
using BooksAPI.Models.DTO.BookCategoryDto;
using BooksAPI.Models.DTO.BookDTO;
using BooksAPI.Models.DTO.CollectionDTO;
using BooksAPI.Models.DTO.CommentDTO;
using BooksAPI.Models.DTO.ShoppingItemCartDTO;
using BooksAPI.Repositories.Implementation;
using BooksAPI.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : Controller
{
    private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;

    public ShoppingCartController(IShoppingCartItemRepository shoppingCartItemRepository, 
        IShoppingCartRepository shoppingCartRepository, 
        IUserRepository userRepository, IBookRepository bookRepository)
    {
        _shoppingCartItemRepository = shoppingCartItemRepository;
        _shoppingCartRepository = shoppingCartRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }
    
    [Authorize]
    [HttpPost("AddToCart/{userId}")]
    public async Task<IActionResult> AddBookToCard(CreateCartItemDto request, Guid userId)
    {
        var existingUser = await _userRepository.GetById(userId);
        var existingBook = await _bookRepository.GetById(request.BookId);
        if (existingUser != null && existingBook != null)
        {
            var cart = await _shoppingCartRepository.GetCartByUserId(userId);

            if (cart == null)
            {
                cart = await _shoppingCartRepository.CreateCartForUser(userId);
            }

            var existingItem = cart.Items.FirstOrDefault(item => item.Book.Id == request.BookId);
            if (existingItem != null)
            {
                existingItem.Quantity += request.Quantity;
                await _shoppingCartItemRepository.UpdateAsync(existingItem);
            }
            else
            {
                var newItem = new ShoppingCartItem
                {
                    Book = existingBook!,
                    Quantity = request.Quantity,
                    ShoppingCart = cart
                };

                await _shoppingCartItemRepository.CreateAsync(newItem);
                cart.Items.Add(newItem);
            }

            await _shoppingCartRepository.UpdateCart(cart);

            return Ok(cart);
        }
        
        return BadRequest();
    }
    
    [HttpGet("GetUserCart/{userId}")]
    public async Task<IActionResult> GetCartItemsByUserId(Guid userId)
    {
        var cart = await _shoppingCartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            return NotFound("Shopping Cart was not found");
        }

        var cartItems = await _shoppingCartItemRepository.GetItemsByCartId(cart.Id);
        return Ok(cartItems);
    }
    
    [HttpPut("UpdateCart/{userId}/{itemId}/{newQuantity}")]
    public async Task<IActionResult> UpdateCartItemQuantity(Guid userId, Guid itemId, int newQuantity)
    {
        if (newQuantity < 1 || newQuantity > 10)
        {
            return BadRequest("Quantity should be between 1 and 10");
        }
            
        var cart = await _shoppingCartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            return NotFound("Shopping Cart was not found");
        }

        var cartItem = await _shoppingCartItemRepository.GetById(itemId);
        if (cartItem == null || cartItem.ShoppingCart.Id != cart.Id)
        {
            return NotFound("Shopping Cart Items were not found");
        }
        
        cartItem.Quantity = newQuantity;

        await _shoppingCartItemRepository.UpdateAsync(cartItem);

        return Ok(cartItem);
    }
    
    [Authorize]
    [HttpDelete("Delete/{userId}/CartItem/{itemId}")]
    public async Task<IActionResult> RemoveCartItem(Guid userId, Guid itemId)
    {
        var cart = await _shoppingCartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            return NotFound("Shopping Cart was not found");
        }

        var cartItem = cart.Items.FirstOrDefault(item => item.Id == itemId);
        if (cartItem == null)
        {
            return NotFound("Shopping Cart Item was not found");
        }

        cart.Items.Remove(cartItem);
        await _shoppingCartItemRepository.DeleteAsync(itemId);
        await _shoppingCartRepository.UpdateCart(cart);

        return Ok("Елемент корзини видалено");
    }
    
    [HttpDelete("ClearCart{userId}")]
    public async Task<IActionResult> ClearUserCart(Guid userId)
    {
        var cart = await _shoppingCartRepository.GetCartByUserId(userId);
        if (cart == null)
        {
            return NotFound("Корзина не знайдена");
        }

        cart.Items.Clear();

        await _shoppingCartItemRepository.DeleteAllItemsInCart(cart.Id);
        await _shoppingCartRepository.UpdateCart(cart);

        return Ok("Корзина користувача була очищена");
    }
}