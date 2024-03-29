using System;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface ICartRepository
	{
        public Task<CartEntity> AddToCart(int userId, int bookId);

    }
}

