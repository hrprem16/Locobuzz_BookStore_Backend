using System;
namespace Common_Layer.Request_Model
{
	public class RegisterModel
	{
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
    }
}

