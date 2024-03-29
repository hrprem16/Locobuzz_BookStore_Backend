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
        public async Task<bool> RemoveBookfromCart(int userId, int cartId)
		{
			return await cartRepository.RemoveBookfromCart(userId, cartId);
		}
        public async Task<List<CartEntity>> GetAllCartItems(int userId)
		{
			return await cartRepository.GetAllCartItems(userId);

        }
        public async Task<int> UpdateQuantity(int userId, int bookId, int quantity)
		{
			return await cartRepository.UpdateQuantity(userId, bookId, quantity);

        }

    }
}

