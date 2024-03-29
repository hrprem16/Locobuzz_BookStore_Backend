using System;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class WishlistRepository:IWishlistRepository
    {
		private readonly BookStoreContext context;

		public WishlistRepository(BookStoreContext context)
		{
			this.context = context;
		}
		public async Task<WishlistEntity> AddToWishList(int userId,int bookId)
		{
			var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            //var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
            if (user.UserRole != "admin")
			{
				var entity = await context.WishlistTable.FirstOrDefaultAsync(a => a.Book_id == bookId && userId == userId);
				if (entity == null)
				{
					WishlistEntity wishlist = new WishlistEntity();
					wishlist.WishlistBy = user;
					//wishlist.WishlistFor = book;
					wishlist.userId = userId;
					wishlist.Book_id = bookId;
					context.WishlistTable.Add(wishlist);
					await context.SaveChangesAsync();
					return wishlist;

				}
				throw new Exception("Book already exists in wishlist!");
			}
			throw new Exception("Admin can't add items to wishlist!");
		}

        public async Task<WishlistEntity> RemoveBookFromWishlist(int userId,int wishlistId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user.UserRole != "admin")
            {
                var entity = await context.WishlistTable.FirstOrDefaultAsync(a=>a.Wishlist_Id==wishlistId);
                if (entity!=null)
				{ 
                    context.WishlistTable.Remove(entity);
                    await context.SaveChangesAsync();
					return entity;
                }
                throw new Exception("Book not exists in wishlist!");
            }
            throw new Exception("Admin can't remove items from wishlist!");
        }
        public async Task<List<WishlistEntity>> GetAllBookFromWishlist(int userId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user.UserRole != "admin")
            {
                var entity = await context.WishlistTable.Where(a=>a.userId==userId).ToListAsync();
                if (entity != null)
                {
                    return entity;
                }
                throw new Exception("Wishlist is empty!");
            }
            throw new Exception("Admin can't see wishlist!");
        }

    }
}

