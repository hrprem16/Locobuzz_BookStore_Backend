using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface IBookRepository
	{

        public Task<BookEntity> AddBook(int userId, AddBookModel addBookModel);
        public Task<bool> UpdateBook(int bookId, AddBookModel addBookModel);
    }
}

