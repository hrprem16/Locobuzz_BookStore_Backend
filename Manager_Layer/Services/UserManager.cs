﻿using System;
using Common_Layer.Request_Model;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class UserManager:IUserManager
	{
        private readonly IUserRepository repository;

        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<UserEntity> UserRegistration(RegisterModel model)
        {
            return await repository.UserRegistration(model);
        }
    }
}

