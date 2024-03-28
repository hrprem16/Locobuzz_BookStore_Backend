using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface IBookManager
	{
        public Task<BookEntity> AddBook(int userId, AddBookModel addBookModel);
        public Task<bool> UpdateBookDetails(int userId, int bookId, UpdateBookDetailsModel model);
        public Task<bool> UpdatePrice(int userId, int bookId, int price);
        public Task<bool> UpdateDiscounPrice(int userId, int bookId, int discountPrice);
        public Task<bool> UpdateImage(int userId, int bookId, string imageFilePath);

    }
}

