using System;
using Common_Layer.Request_Model;
using Common_Layer.Response_Model;
using Common_Layer.Utility;
using Manager_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.Entity;

namespace BookStore_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController:ControllerBase
	{
        private readonly IUserManager userManager;
        private readonly IBus bus;

        public UserController(IUserManager userManager,IBus bus)
        {
            this.userManager = userManager;
            this.bus = bus;
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
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                var response = await userManager.ForgetPassword(model);
                if (response != null)
                {
                    Send send = new Send();
                    string str = send.SendMail(model.EmailId, response);
                    Uri uri = new Uri("rabbitmq://localhost/BookStoreEmailQueue");
                    var endpoint = await bus.GetSendEndpoint(uri);
                    return Ok(new ResModel<bool> { Success = true, Message = "Forget Password Successfull", Data =true });
                }
                else
                {
                    throw new Exception("Failed to send token");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = false, Message = ex.Message, Data =false });
            }

        }
        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                var response = await userManager.ResetPassword(userId, model);
                if (response != null)
                {
                    return Ok(new ResModel<bool> { Success = true, Message = "Reset Password Successfull", Data = response });
                }
                else
                {
                    return BadRequest(new ResModel<bool> { Success = true,Message="Reset Password Failed!", Data=response });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new ResModel<bool> { Success = true, Message = ex.Message, Data = false });
            }
        }
    }
}

               

           
      
