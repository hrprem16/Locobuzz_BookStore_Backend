using System;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class CartManager:ICartManager
	{
		private readonly ICartRepository cartRepository;

		public CartManager(ICartRepository cartRepository)
		{
			this.cartRepository = cartRepository;
		}
        public async Task<CartEntity> AddToCart(int userId, int bookId)
        {
			return await cartRepository.AddToCart(userId, bookId);
		}

    }
}

