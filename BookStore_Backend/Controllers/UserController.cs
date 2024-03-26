using System;
using Common_Layer.Request_Model;
using Common_Layer.Response_Model;
using Manager_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace BookStore_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
	{
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost]
        [Route("Reg")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                var response = await userManager.UserRegistration(model);
                if (response != null)
                {
                    return Ok(new ResModel<UserEntity> { Success = true, Message = "User Registration Successfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<UserEntity> { Success = false, Message = "User Registration Failed!", Data = response });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<UserEntity> { Success = false, Message = ex.Message, Data = null });
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {
                var response = await userManager.UserLogin(model);
                if (response != null)
                {
                    return Ok(new ResModel<string> { Success = true, Message = "Login Successfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<string> { Success = false, Message = "Login Failed!", Data = response });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<string> { Success = false, Message = ex.Message, Data = null });
            }
            
        }
	}
}

