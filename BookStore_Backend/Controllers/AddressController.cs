using System;
using Common_Layer.Request_Model;
using Common_Layer.Response_Model;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace BookStore_Backend.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AddressController:ControllerBase
	{
		private readonly IAddressManager addressManager;
		public AddressController(IAddressManager addressManager)
		{
			this.addressManager = addressManager;
		}
		[Authorize]
		[HttpPost]
		[Route("addAddress")]
		public async Task<IActionResult> AddAddress(AddAddressModel model)
		{
			try
			{
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await addressManager.AddAddress(userId, model);
				if (response != null)
				{
					return Ok(new ResModel<AddressEntity> { Success = true, Message = "Address Added Successfully", Data = response });

				}
				return BadRequest(new ResModel<AddressEntity> { Success = false, Message = "Address added failed!", Data = null });

            }
			catch (Exception ex)
			{
                return BadRequest(new ResModel<bool> { Success = false, Message =ex.Message, Data =false});
            }
		}

        [Authorize]
        [HttpPut]
        [Route("updateAddress")]
        public async Task<IActionResult> UpdateAddress(int addressId,AddAddressModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await addressManager.UpdateAddress(userId,addressId, model);
                if (response != null)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Address Update Successfully", Data = response });

                }
                return BadRequest(new ResModel<bool> { Success = false, Message = "Address Update failed!", Data =false });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await addressManager.GetAllAddress(userId);
                if (response != null)
                {
                    return Ok(new ResModel<List<AddressEntity>> { Success = true, Message = "Display all address successfully", Data = response });

                }
                return BadRequest(new ResModel<List<AddressEntity>> { Success = false, Message = "Something went wrong", Data =null });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }
        [Authorize]
        [HttpDelete]
        [Route("DeleteAddress")]
        public async Task<IActionResult> DeleteAddress(int addressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await addressManager.RemoveAddress(userId,addressId);
                if (response != null)
                {
                    return Ok(new ResModel<AddressEntity> { Success = true, Message = "Address Removed successfully", Data = response });

                }
                return BadRequest(new ResModel<AddressEntity> { Success = false, Message = "Something went wrong", Data = null });

            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data = false });
            }
        }




    }

}

