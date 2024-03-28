﻿using System;
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
        public async Task<bool> UpdatePrice(int userId, int bookId, int price)
		{
			return await bookRepository.UpdatePrice(userId, bookId, price);
		}
        public async Task<bool> UpdateDiscounPrice(int userId, int bookId, int discountPrice)
		{
			return await bookRepository.UpdateDiscounPrice(userId, bookId, discountPrice);
		}
        public async Task<bool> UpdateImage(int userId, int bookId, string imageFilePath)
		{
			return await bookRepository.UpdateImage(userId, bookId, imageFilePath);
		}
    }
}

