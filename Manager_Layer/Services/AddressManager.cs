using System;
using Common_Layer.Request_Model;
using Manager_Layer.Interfaces;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Manager_Layer.Services
{
	public class AddressManager: IAddressManager
    {
		private readonly IAddressRepository addressRepository;
		public AddressManager(IAddressRepository addressRepository)
		{
			this.addressRepository = addressRepository;
		}

        public async Task<AddressEntity> AddAddress(int userId, AddAddressModel model)
        {
            return await addressRepository.AddAddress(userId, model);
        }
        public async Task<bool> UpdateAddress(int userId, int addressId, AddAddressModel model)
        {
            return await addressRepository.UpdateAddress(userId, addressId, model);
        }
        public async Task<AddressEntity> RemoveAddress(int userId, int addressId)
        {
            return await addressRepository.RemoveAddress(userId, addressId);
        }
        public async Task<List<AddressEntity>> GetAllAddress(int userId)
        {
            return await addressRepository.GetAllAddress(userId);
        }
    }
}

