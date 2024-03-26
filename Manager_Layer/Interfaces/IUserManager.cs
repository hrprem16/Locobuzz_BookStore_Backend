using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface IUserManager
	{
        public Task<UserEntity> UserRegistration(RegisterModel model);
    }
}

