using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
	public class BookEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Book_id { get; set; }

		public string Book_name { get; set;}
		public string Book_Description { get; set; }
        public string Book_Author{ get; set; }
        public string Book_image { get; set; }
        public int Book_Price{ get; set; }
        public int Book_Discount_Price { get; set; }
        public int Book_Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("AddedBy")]
        public int userId { get; set; }

        [JsonIgnore]
        public virtual UserEntity AddedBy { get; set; }
    }
}

