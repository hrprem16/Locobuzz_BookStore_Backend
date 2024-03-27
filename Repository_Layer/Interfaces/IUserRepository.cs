using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface IUserRepository
	{
        public Task<UserEntity> UserRegistration(RegisterModel model);
        public Task<string> UserLogin(LoginModel model);
        public Task<string> ForgetPassword(ForgetPasswordModel model);
        public Task<bool> ResetPassword(int userId, ResetPasswordModel model);

    }
}

