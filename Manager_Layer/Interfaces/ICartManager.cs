using System;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface ICartManager
	{
        public Task<CartEntity> AddToCart(int userId, int bookId);

    }
}

