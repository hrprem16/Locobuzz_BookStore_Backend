using System;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface IWishlistRepository
	{
        public Task<WishlistEntity> AddToWishList(int userId, int bookId);
        public Task<WishlistEntity> RemoveBookFromWishlist(int userId, int wishlistId);

    }
}

