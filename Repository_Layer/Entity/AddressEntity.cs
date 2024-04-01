using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository_Layer.Entity
{
	public class AddressEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Address_Id { get; set; }

		public string Address { get; set; }
		public string LandMark { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }

		[ForeignKey("AddressBy")]
		public int userId { get; set; }

		public virtual UserEntity AddressBy { get; set; }

    }
}

