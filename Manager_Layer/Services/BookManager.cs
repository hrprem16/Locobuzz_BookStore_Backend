using System;
using Common_Layer.Request_Model;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class BookManager: IBookManager
    {
		private readonly IBookRepository bookRepository;

		public BookManager(IBookRepository bookRepository)
		{
			this.bookRepository = bookRepository;
		}
		public async Task<BookEntity> AddBook(int userId, AddBookModel addBookModel)
		{
			return await bookRepository.AddBook(userId, addBookModel);
		}
        public async Task<bool> UpdateBookDetails(int userId, int bookId, UpdateBookDetailsModel model)
        {
			return await bookRepository.UpdateBookDetails(userId, bookId, model);
		}
    }
}

