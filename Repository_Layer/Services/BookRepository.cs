﻿using System;
using Common_Layer.Request_Model;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class BookRepository:IBookRepository
    {
		private readonly BookStoreContext context;

		public BookRepository(BookStoreContext context)
		{
			this.context = context;
		}
		public async Task<BookEntity> AddBook(int userId, AddBookModel addBookModel)
		{
			var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
			if (user != null)
			{
				if (user.UserRole == "admin")
				{
					BookEntity book = new BookEntity();
					book.AddedBy = user;
					book.userId = user.userId;
					book.Book_name = addBookModel.Book_name;
					book.Book_Description = addBookModel.Book_Description;
					book.Book_Author = addBookModel.Book_Author;
					book.Book_image = addBookModel.Book_image;
					book.Book_Price = addBookModel.Book_Price;
					book.Book_Discount_Price = addBookModel.Book_Discount_Price;
					book.Book_Quantity = addBookModel.Book_Quantity;
					book.CreatedAt = DateTime.Now;
					book.UpdatedAt = DateTime.Now;
					context.BookTable.Add(book);
                    await context.SaveChangesAsync();
                    return book;
				}
				throw new Exception("User is not Admin!");
			}
			else
			{
				throw new Exception("User doesn't exist!");
			}
		}
		public async Task<bool> UpdateBookDetails (int userId,int bookId, UpdateBookDetailsModel model)
        {
            var user= await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user != null)
            {

				if (user.UserRole == "admin")
				{
					var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
					if (book!=null)
					{
						book.Book_name = model.Book_name;
						book.Book_Description = model.Book_Description;
						book.Book_Author = model.Book_Author;
						book.UpdatedAt = DateTime.Now;
						context.BookTable.Update(book);
						await context.SaveChangesAsync();
						return true;	
					}
                    throw new Exception($"Book with {bookId} doesn't exist! ");
				}
				throw new Exception("User is not Admin!");
            }
            else
            {
                throw new Exception("User doesn't exist!");
            }
        }

    }
}

