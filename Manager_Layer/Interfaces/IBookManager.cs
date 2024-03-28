using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface IBookManager
	{
        public Task<BookEntity> AddBook(int userId, AddBookModel addBookModel);
        public Task<bool> UpdateBookDetails(int userId, int bookId, UpdateBookDetailsModel model);



    }
}

