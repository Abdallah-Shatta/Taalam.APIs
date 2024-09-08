using E_Learning.BL.DTO.Cart;
using E_Learning.BL.DTO.User;
using E_Learning.BL.Managers.CartManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CartController : APIBaseController
    {
        private readonly ICartManager _cartManager;

        public CartController(ICartManager cartManager)
        {
            _cartManager = cartManager;
        }

        [HttpGet("GetCartItems")]
        public ActionResult<IEnumerable<CourseDTO>> GetCartItems(int userId)
        {
            var cartItems = _cartManager.GetCartItems(userId);
            return Ok(cartItems);
        }

        [HttpDelete("RemoveCartItem")]
        public ActionResult<IEnumerable<CourseDTO>> RemoveCartItem(int userId, int courseId)
        {
           var cartItems= _cartManager.RemoveItemFromCart(userId, courseId);
            return Ok(cartItems);
        }


        [HttpGet("GetCartTotal")]
        public ActionResult<CartTotalDTO> GetCartTotal(int userId)
        {
            var total = _cartManager.GetCartTotal(userId);
            return Ok(total);
        }


    }
}
