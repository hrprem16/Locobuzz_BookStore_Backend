using System;
using Common_Layer.Request_Model;
using Repository_Layer.Entity;

namespace Repository_Layer.Interfaces
{
	public interface IAddressRepository
	{
        public Task<AddressEntity> AddAddress(int userId, AddAddressModel model);
        public Task<bool> UpdateAddress(int userId, int addressId, AddAddressModel model);
        public Task<AddressEntity> RemoveAddress(int userId, int addressId);
        public Task<List<AddressEntity>> GetAllAddress(int userId);

    }
}

