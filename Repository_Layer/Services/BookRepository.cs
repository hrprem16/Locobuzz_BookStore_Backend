using System;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
                    if (addBookModel.Book_Price < 0)
                    {
                        throw new ArithmeticException("Price can't be less than zero!");
                    }
                    book.Book_Price = addBookModel.Book_Price;
					book.Book_Discount_Price = addBookModel.Book_Discount_Price;
					book.Book_Quantity = addBookModel.Book_Quantity;
					book.CreatedAt = DateTime.Now;
					book.UpdatedAt = DateTime.Now;
					context.BookTable.Add(book);
                    await context.SaveChangesAsync();
                    return book;
				}
				throw new Exception("User is not an Admin!");
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
				throw new Exception("User is not an Admin!");
            }
            else
            {
                throw new Exception("User doesn't exist!");
            }
        }

		public async Task<bool> UpdatePrice(int userId,int bookId,int price)
		{
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
			if (user != null)
			{
                if (user.UserRole == "admin")
                {
                    var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
                    if (book != null)
                    {
						if (price < 0)
						{
							throw new ArithmeticException("Price can't be less than zero!");
						}
						book.Book_Price = price;
                        book.UpdatedAt = DateTime.Now;
                        context.BookTable.Update(book);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    throw new Exception($"Book with {bookId} doesn't exist! ");
                }
                throw new Exception("User is not an Admin!");
            }
            throw new Exception("User doesn't exist!");
        }

        public async Task<bool> UpdateDiscounPrice(int userId, int bookId, int discountPrice)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user != null)
            {
                if (user.UserRole == "admin")
                {
                    var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
                    if (book != null)
                    {
						if (discountPrice <= book.Book_Price)
						{
                            if (discountPrice < 0)
                            {
                                throw new ArithmeticException("Discount Price can't be less than zero!");
                            }
                            book.Book_Discount_Price = discountPrice;
                            book.UpdatedAt = DateTime.Now;
                            context.BookTable.Update(book);
                            await context.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            throw new InvalidOperationException("Discount Price can't greater than original price");
                        }
                    }
                    throw new Exception($"Book with {bookId} doesn't exist! ");
                }
                throw new Exception("User is not an Admin!");
            }
            throw new Exception("User doesn't exist!");
        }

        public async Task<bool> UpdateImage(int userId,int bookId,string imageFilePath)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user != null)
            {
                if (user.UserRole == "admin")
                {
                    var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
                    if (book != null)
                    {
                        Account account = new Account("dz2emvokk", "734452883777881", "RRlJONvtfnLJZgviiyDH3Lf-ufQ");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(imageFilePath),
                            PublicId = book.Book_name
                        };
                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                        book.UpdatedAt = DateTime.Now;
                        book.Book_image = uploadResult.Url.ToString();
                        await context.SaveChangesAsync();
                        return true;
                    }
                    throw new Exception($"Book with {bookId} doesn't exist! ");
                }
                throw new Exception("User is not an Admin!");
            }
            else
            {
                throw new Exception("User doesn't exist!");
            }
        }
        public async Task<bool> UpdateQuantity(int userId,int bookId,int bookQuantity)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user != null)
            {
                if (user.UserRole == "admin")
                {
                    var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
                    if (book != null)
                    {
                        if (bookQuantity < 0)
                        {
                            throw new ArithmeticException("Quantity can't be less than zero!");
                        }
                        book.Book_Quantity = bookQuantity;
                        book.UpdatedAt = DateTime.Now;
                        await context.SaveChangesAsync();
                        return true;
                    }
                    throw new Exception($"Book with {bookId} doesn't exist! ");
                }
                throw new Exception("User is not as Admin!");
            }
            throw new Exception("User doesn't exist!");
        }
        public async Task<bool> DeleteBook(int userId, int bookId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user != null)
            {
                if (user.UserRole == "admin")
                {
                    var book = await context.BookTable.FirstOrDefaultAsync(a => a.Book_id == bookId);
                    if (book != null)
                    {
                        context.BookTable.Remove(book);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    throw new Exception($"Book with {bookId} doesn't exist! ");
                }
                throw new Exception("User is not as Admin!");
            }
            throw new Exception("User doesn't exist!");
        }

        public async Task<List<BookEntity>> GetAllBooks(int userId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user.UserRole == "admin")
            {
                var books = await context.BookTable.Where(a => a.userId == userId).ToListAsync();
                if (books != null)
                {
                    return books;
                }
                throw new Exception("Books Unavailable!");
            }
            else
            {
                var booklist = await context.BookTable.ToListAsync();
                if (booklist != null)
                {
                    return booklist;
                }
                throw new Exception("Books Unavailable!");
            }
        }

        public async Task<List<BookEntity>> GetAllBooks()
        {   
            var booklist = await context.BookTable.ToListAsync();
            if (booklist != null)
            {
                return booklist;
            }
            throw new Exception("Books Unavailable!");
        }


    }
}

