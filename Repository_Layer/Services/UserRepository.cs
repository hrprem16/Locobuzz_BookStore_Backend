using System;
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
        private string GenerateToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Email",user.EmailId),
                new Claim("Role",user.UserRole),
                new Claim("UserId",user.userId.ToString())
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> UserLogin(LoginModel model)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.EmailId == model.EmailId);
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password)){
                    var token = GenerateToken(user);
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
        public async Task<string> ForgetPassword(ForgetPasswordModel model)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.EmailId==model.EmailId);
            {
                if (user != null)
                {
                    var token=GenerateToken(user);
                    return token;
                }
                else
                {
                    throw new Exception($"User with email id {model.EmailId} doesn't exists!");
                }
            }

        }

        public async Task<bool> ResetPassword(int userId,ResetPasswordModel model)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user != null)
            {
                if (model.NewPassword== model.ConfirmPassword)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                    user.UpdatedAt = DateTime.Now;
                    context.SaveChanges();
                    return true;
                }
                else{
                    throw new Exception("Password not matched!");
                }
            }
            else
            {
                throw new Exception("User doesn't exist!");
            }
        }

    }
}

            
