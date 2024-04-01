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
                        var bookinCart = await context.CartTable.FirstOrDefaultAsync(a => a.userId == userId && a.Book_id == bookId);
                        if (bookinCart==null)
                        {
                            CartEntity cart = new CartEntity();
                            cart.AddedBy = user;
                            cart.AddedFor = book;
                            cart.userId = user.userId;
                            cart.Book_id = book.Book_id;
                            cart.Quantity = 1;
                            cart.IsPurchase = false;
                            cart.OrderAt = null;
                            context.CartTable.Add(cart);
                            await context.SaveChangesAsync();
                            return cart;
                        }
                        else
                        {
                            bookinCart.Quantity++;
                            await context.SaveChangesAsync();
                            return bookinCart;
                        }
                    }
                    throw new Exception($"book having book id { bookId} it not available!");
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

        public async Task<bool> Increase_Decrease(int userId,int book_Id,bool increase)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user == null)
            {
                throw new Exception($"User with {userId} doesn't exists!");
            }
            var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == book_Id);
            if (book == null)
            {
                throw new Exception($"Book with {book_Id} doesn't exists!");
            }
            var bookInCart = await context.CartTable.FirstOrDefaultAsync(a => a.Book_id == book_Id);
            if (bookInCart == null)
            {
                throw new Exception("Book doesn't exists in cart");
            }
                if (increase)
                {
                    bookInCart.Quantity++;
                }
                else
                {
                    if (bookInCart.Quantity == 1)
                    {
                        throw new Exception("Use the remove options now as quantity is 1");
                    }
                    bookInCart.Quantity--;
                }
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetTotalPriceofItems(int userId)
        {
            try
            {
                
                var list = await GetAllCartItems(userId);
                int sum = 0;
                foreach (var item in list)
                {
                    var book = await context.BookTable.FirstOrDefaultAsync(a =>a.Book_id==item.Book_id);
                    sum = sum + item.Quantity * book.Book_Discount_Price;
                }
                return sum;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> PurchaseItem(int userId, bool paymentDone)
        {
            if (!paymentDone)
            {
                throw new Exception("Payment not Received!");
            }
            var list = await GetAllCartItems(userId);
            foreach (var item in list)
            {
                item.IsPurchase = true;
            }
            return true;
        }
        
	} 
}

