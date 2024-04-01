using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Repository_Layer.Entity
{
    public class CartEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        public int Quantity { get; set; }
        public bool IsPurchase { get; set; }
        public DateTime? OrderAt { get; set; }

        [ForeignKey("AddedBy")]
        public int userId { get; set; }

        [ForeignKey("AddedFor")]
        public int Book_id { get; set; }

        [JsonIgnore]
        public virtual UserEntity AddedBy { get; set; }

        [JsonIgnore]
        public virtual BookEntity AddedFor { get; set; }
    }
}
