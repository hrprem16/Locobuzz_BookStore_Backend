using System;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface ICartManager
	{
        public Task<CartEntity> AddToCart(int userId, int bookId);
        public Task<bool> RemoveBookfromCart(int userId, int cartId);
        public Task<List<CartEntity>> GetAllCartItems(int userId);
        public Task<int> UpdateQuantity(int userId, int bookId, int quantity);

    }
}

