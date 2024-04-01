using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Manager_Layer.Interfaces
{
	public interface IAddressManager
	{
        public Task<AddressEntity> AddAddress(int userId, AddAddressModel model);
        public Task<bool> UpdateAddress(int userId, int addressId, AddAddressModel model);
        public Task<AddressEntity> RemoveAddress(int userId, int addressId);
        public Task<List<AddressEntity>> GetAllAddress(int userId);
    }
}

