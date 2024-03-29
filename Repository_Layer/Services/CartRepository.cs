﻿using System;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class CartRepository:ICartRepository
	{
		private readonly BookStoreContext context;

		public CartRepository(BookStoreContext context)
		{
			this.context = context;
		}
		public async Task<CartEntity> AddToCart(int userId, int bookId)
		{
			var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
			if (user != null)
			{
				if (user.UserRole != "admin")
				{
                    var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
                    if (book != null)
                    {
                        CartEntity cart = new CartEntity();
                        cart.AddedBy = user;
                        cart.AddedFor = book;
                        cart.userId = user.userId;
                        cart.Book_id = book.Book_id;
                        cart.Quantity =1;

                        context.CartTable.Add(cart);
                        await context.SaveChangesAsync();
                        return cart;
                    }
                    throw new Exception($"book having book id{bookId} it not available!");
                }
                throw new Exception("Admin can't add item to cart");
			}
            throw new Exception("user not found!");
        }
	}
}
