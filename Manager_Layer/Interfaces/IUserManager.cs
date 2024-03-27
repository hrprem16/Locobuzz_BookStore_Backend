using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface IUserManager
	{
        public Task<UserEntity> UserRegistration(RegisterModel model);
        public Task<string> UserLogin(LoginModel model);
        public Task<string> ForgetPassword(ForgetPasswordModel model);
        public Task<bool> ResetPassword(int userId, ResetPasswordModel model);
    }
}

