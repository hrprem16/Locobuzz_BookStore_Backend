using System;
using Common_Layer.Response_Model;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace BookStore_Backend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CartController:ControllerBase
	{
		private readonly ICartManager cartManager;
		public CartController(ICartManager cartManager)
		{
			this.cartManager = cartManager;
		}
		[Authorize]
		[HttpPost]
		[Route("AddToCart")]
		public async Task<IActionResult> AddToCart(int bookId)
		{
			try
			{
				int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = await cartManager.AddToCart(userId, bookId);
				if (response != null)
				{
					return Ok(new ResModel<CartEntity> { Success = true, Message = $"Book added to cart Successfully!",Data=response });

				}
				else
				{
                    return BadRequest(new ResModel<CartEntity> { Success = false, Message = $"Book Adding in cart Failed!!", Data = null });
                }
			}
			catch(Exception ex)
			{
				return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
			}
		}
        [Authorize]
        [HttpPut]
        [Route("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity(int bookId, int quantity)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await cartManager.UpdateQuantity(userId, bookId, quantity);
                if (response != null)
                {
                    return Ok(new ResModel<int> { Success = true, Message = $"Item quantity is changed!", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<int> { Success = false, Message = $"Item qunatity isn't changed!", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await cartManager.GetAllCartItems(userId);
                if (response != null)
                {
                    return Ok(new ResModel<List<CartEntity>> { Success = true, Message = $"Display items from cart are Successfully!", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<List<CartEntity>> { Success = false, Message = $"Display items from cart is Failed!!", Data = response });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("RemoveBookFromCart")]
        public async Task<IActionResult> RemoveBookFromCart(int cartId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await cartManager.RemoveBookfromCart(userId, cartId);
                if (response)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = $"Book remove from cart is Successfully!",Data=response });

                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = $"Book remove is  Failed!!", Data =false });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
          

    }
}

