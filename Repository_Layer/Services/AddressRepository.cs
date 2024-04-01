using System;
using Common_Layer.Request_Model;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Context;
using Repository_Layer.Entity;
using Repository_Layer.Interfaces;

namespace Repository_Layer.Services
{
	public class AddressRepository:IAddressRepository
    {
		private readonly BookStoreContext context;
		public AddressRepository(BookStoreContext context)
		{
			this.context = context;
		}

		public async Task<AddressEntity> AddAddress(int userId, AddAddressModel model)
		{
			var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
			if (user == null)
			{
				throw new Exception("User doesn't exist!");
			}
			if (user.UserRole != "admin")
			{
				AddressEntity address = new AddressEntity();
				address.AddressBy = user;
				address.Address = model.Address;
                address.LandMark =model.LandMark;
                address.City = model.City;
                address.State = model.State;
                address.PostalCode = model.PostalCode;
                address.Country = model.Country;
				context.AddressTable.Add(address);
				await context.SaveChangesAsync();
				return address;

            }
			throw new Exception("Admin can't add Address");
		}

		public async Task<bool> UpdateAddress(int userId,int addressId, AddAddressModel model)
		{
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist!");
            }
            if (user.UserRole != "admin")
            {
                var addressentity = await context.AddressTable.FirstOrDefaultAsync(a => a.Address_Id == addressId);
                if (addressentity != null)
                {
                    addressentity.Address = model.Address;
                    addressentity.LandMark = model.LandMark;
                    addressentity.City = model.City;
                    addressentity.State = model.State;
                    addressentity.PostalCode = model.PostalCode;
                    addressentity.Country = model.Country;

                    await context.SaveChangesAsync();
                    return true;
                }

                throw new Exception("Address doesn't exist!");

            }
            throw new Exception("Admin can't update Address");

        }
        public async Task<AddressEntity> RemoveAddress(int userId, int addressId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist!");
            }
            if (user.UserRole != "admin")
            {
                var addressentity = await context.AddressTable.FirstOrDefaultAsync(a => a.Address_Id == addressId);
                if (addressentity != null)
                {
                    context.AddressTable.Remove(addressentity);
                    await context.SaveChangesAsync();
                    return addressentity;
                }

                throw new Exception("Address doesn't exist!");

            }
            throw new Exception("Admin can't remove Address");

        }

        public async Task<List<AddressEntity>> GetAllAddress(int userId)
        {
            var user = await context.UserTable.FirstOrDefaultAsync(a => a.userId == userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist!");
            }
            if (user.UserRole != "admin")
            {
                var entity = await context.AddressTable.Where(a => a.userId == userId).ToListAsync();
                if (entity != null)
                {
                    return entity;
                }

                throw new Exception("Address list is empty!");

            }
            throw new Exception("Admin can't see Address!");

        }
    }
}
