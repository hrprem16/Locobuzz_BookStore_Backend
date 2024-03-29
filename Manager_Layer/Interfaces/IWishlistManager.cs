using System;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface IWishlistManager
	{
        public Task<WishlistEntity> AddToWishList(int userId, int bookId);
        public Task<WishlistEntity> RemoveBookFromWishlist(int userId, int wishlistId);

    }
}

