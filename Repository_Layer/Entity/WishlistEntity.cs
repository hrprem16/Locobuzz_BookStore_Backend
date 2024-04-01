using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
	public class WishlistEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Wishlist_Id { get; set; }

		[ForeignKey("WishlistBy")]
		public int userId { get; set;}
		[JsonIgnore]
		public virtual UserEntity WishlistBy { get; set; }

        [ForeignKey("WishlistFor")]
		public int Book_id { get; set; }
		[JsonIgnore]
        public virtual BookEntity WishlistFor { get; set; }

    }
}

