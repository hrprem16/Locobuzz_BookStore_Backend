using System;
using Common_Layer.Request_Model;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
        private readonly BookStoreContext context;

        public UserRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public async Task<UserEntity> UserRegistration(RegisterModel model)
        {
            if (await context.UserTable.FirstOrDefaultAsync(a => a.EmailId == model.EmailId) == null)
            {
                UserEntity entity = new UserEntity();
                entity.FullName = model.FullName;
                entity.EmailId = model.EmailId;
                entity.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                entity.MobileNumber = model.MobileNumber;
                entity.CreatedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                entity.UserRole = "user";
                context.UserTable.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
            else
            {
                throw new Exception("User Already Exists ,Enter another id for Registration");
            }
           
        }
    }
}

            
