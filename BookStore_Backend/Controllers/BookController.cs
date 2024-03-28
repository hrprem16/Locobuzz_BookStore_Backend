﻿using System;
using Common_Layer.Request_Model;
using Common_Layer.Response_Model;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace BookStore_Backend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookController:ControllerBase
	{
		private readonly IBookManager bookManager;

		public BookController(IBookManager bookManager)
		{
			this.bookManager = bookManager;
		}
		[Authorize]
		[HttpPost]
		[Route("AddBook")]
		public async Task<IActionResult> AddBook(AddBookModel model)
		{
			try
			{
				int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = await bookManager.AddBook(userId, model);
				if (response != null)
				{
					return Ok(new ResModel<BookEntity> { Success = true, Message = "Book Added Successfull", Data = response });
				}
				else{
                    return BadRequest(new ResModel<BookEntity> { Success = false, Message = "Book Added UnSuccessfull", Data = response });
                }
			}
			catch(Exception ex)
			{
                return BadRequest(new ResModel<BookEntity> { Success = false, Message =ex.Message, Data = null });
            }
		}
		[Authorize]
		[HttpPut]
		[Route("UpdateBook")]
		public async Task<IActionResult> UpdateBookDetails(int bookId,UpdateBookDetailsModel model)
		{
			try
			{
				int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = await bookManager.UpdateBookDetails(userId, bookId, model);
				if (response)
				{
					return Ok(new ResModel<bool> { Success = true, Message = "Update Book Details Successfull", Data = response });
				}
				else
				{
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Update Book Details UnSuccessfull", Data = response });
                }
			}
			catch (Exception ex)
			{
                return BadRequest(new ResModel<bool> { Success = false, Message =ex.Message, Data = false });
            }
		}
		[Authorize]
		[HttpPut]
		[Route("updatePrice")]
		public async Task<IActionResult> UpdateBookPrice(int bookId,int price)
		{
			try
			{
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
				var response = await bookManager.UpdatePrice(userId, bookId, price);
				if (response)
				{
					return Ok(new ResModel<bool> { Success = true, Message = "Original Price Updated", Data = response });

                }
				else
				{
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Original Price not Updated", Data = response });
                }
				
			}
			catch(Exception ex)
			{
                return BadRequest(new ResModel<bool> { Success = false, Message =ex.Message, Data = false});
            }
		}
        [Authorize]
        [HttpPut]
        [Route("updateDiscountPrice")]
        public async Task<IActionResult> UpdateDiscountPrice(int bookId, int discountPrice)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await bookManager.UpdateDiscounPrice(userId, bookId, discountPrice);
                if (response)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Discount Price Updated", Data = response });

                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = false, Message = "Discount Price not Updated", Data = response });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }


    }
}

