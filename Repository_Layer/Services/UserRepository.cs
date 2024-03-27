﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common_Layer.Request_Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class UserRepository:IUserRepository
	{
        private readonly BookStoreContext context;
        private readonly IConfiguration config;
        public UserRepository(BookStoreContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
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
        private string GenerateToken(string Email, int UserId)
        {
            //Defining a Security Key 
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("UserId", UserId.ToString())
            };
            var token = new JwtSecurityToken(
                config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2), // Token expiration time
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }

        public async Task<string> UserLogin(LoginModel model)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.EmailId == model.EmailId);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password)){
                    var token = GenerateToken(user.EmailId, user.userId);
                    return token;
                }
                else
                {
                    throw new Exception("Invalid Password");
                }
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

    }
}

            
