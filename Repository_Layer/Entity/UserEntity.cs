using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository_Layer.Entity
{
	public class UserEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int userId { get; set; }

		public string FullName { get; set; }
		public string EmailId { get; set; }
		public string Password { get; set; }
		public string MobileNumber { get; set;}

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public string UserRole { get; set; }

		
	}
}

