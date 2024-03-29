using System;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface ICartRepository
	{
        public Task<CartEntity> AddToCart(int userId, int bookId);
        public Task<bool> RemoveBookfromCart(int userId, int cartId);
        public Task<List<CartEntity>> GetAllCartItems(int userId);
        public Task<int> UpdateQuantity(int userId, int bookId, int quantity);

    }
}

