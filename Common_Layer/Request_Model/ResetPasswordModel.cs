using System;
namespace Common_Layer.Request_Model
{
	public class ResetPasswordModel
	{
		public string NewPassword { get; set; }
		public string ConfirmPassword { get; set; }
	}
}

