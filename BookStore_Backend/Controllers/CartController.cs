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
				return Ok(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
			}
		}

	}
}

