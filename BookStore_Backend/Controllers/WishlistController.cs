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
	public class WishlistController:ControllerBase
	{
		private readonly IWishlistManager wishlistManager;
		public WishlistController(IWishlistManager wishlistManager)
		{
			this.wishlistManager = wishlistManager;
		}
		[Authorize]
		[HttpPost]
		[Route("AddToWishList")]
		public async Task<IActionResult> AddtoWishList(int bookId)
		{ 
            try
			{
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = await wishlistManager.AddToWishList(userId, bookId);
				if (response != null)
				{
					return Ok(new ResModel<WishlistEntity> { Success = true, Message = "Book Added to Wishlist!", Data = response });
				}
                return BadRequest(new ResModel<WishlistEntity> { Success = false, Message = "Book not Added to Wishlist", Data = null });
            }
			catch(Exception ex){
				return BadRequest(new ResModel<bool> { Success = true, Message = ex.Message, Data = false });
			}
		}

	}
}

