using System;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class WishlistManager:IWishlistManager
    {
		private readonly IWishlistRepository wishlistRepository;
		public WishlistManager(IWishlistRepository wishlistRepository)
		{
			this.wishlistRepository = wishlistRepository;
		}
		public async Task<WishlistEntity> AddToWishList(int userId, int bookId)
		{
			return await wishlistRepository.AddToWishList(userId, bookId);
		}
        public async Task<WishlistEntity> RemoveBookFromWishlist(int userId, int wishlistId)
		{
			return await wishlistRepository.RemoveBookFromWishlist(userId, wishlistId);
		}

    }
}

