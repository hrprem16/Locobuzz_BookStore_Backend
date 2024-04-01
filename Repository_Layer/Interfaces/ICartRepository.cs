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
        public Task<bool> Increase_Decrease(int userId, int book_Id, bool increase);
        public Task<int> GetTotalPriceofItems(int userId);
        public Task<bool> PurchaseItem(int userId, bool paymentDone);

    }
}

