using System;
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
                        if(await context.CartTable.FirstOrDefaultAsync(a => a.userId == userId && a.Book_id == bookId) == null)
                        {
                            CartEntity cart = new CartEntity();
                            cart.AddedBy = user;
                            cart.AddedFor = book;
                            cart.userId = user.userId;
                            cart.Book_id = book.Book_id;
                            cart.Quantity = 1;

                            context.CartTable.Add(cart);
                            await context.SaveChangesAsync();
                            return cart;
                        }
                        throw new Exception("Book Already added in the cart");
                    }
                    throw new Exception($"book having book id{bookId} it not available!");
                }
                throw new Exception("Admin can't add item to cart");
			}
            throw new Exception("user not found!");
        }

        public async Task<bool> RemoveBookfromCart(int userId,int cartId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user.UserRole == "user")
            {
                var cartItem = await context.CartTable.FirstOrDefaultAsync(a => a.CartId == cartId && a.userId==userId);
                if (cartItem != null)
                {
                    context.CartTable.Remove(cartItem);
                    await context.SaveChangesAsync();
                    return true;
                }
                throw new Exception("Cart item doesn't exists!");
            }
            throw new Exception("User can't remove items from cart!");
            
        }

        public async Task<List<CartEntity>> GetAllCartItems(int userId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user.UserRole != "admin")
            {
                var cartItems = await context.CartTable.Where(a=>a.userId==userId).ToListAsync();
                return cartItems;
            }
            throw new Exception("Admin can't see the items inside carts");
        }
        public async Task<int> UpdateQuantity(int userId,int bookId,int quantity)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user.UserRole != "admin")
            {
                var cartItem = await context.CartTable.FirstOrDefaultAsync(a => a.userId == userId && a.Book_id == bookId);
                if (cartItem != null)
                {
                    cartItem.Quantity = quantity;
                    await context.SaveChangesAsync();
                    return cartItem.Quantity;
                  
                }
                throw new Exception("cart items not valid!");
                
            }
            throw new Exception("Admin can't make changes!");
        }
	} 
}

