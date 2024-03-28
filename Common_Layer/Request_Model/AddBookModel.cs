using System;
namespace Common_Layer.Request_Model
{
	public class AddBookModel
	{
        public string Book_name { get; set; }
        public string Book_Description { get; set; }
        public string Book_Author { get; set; }
        public string Book_image { get; set; }
        public int Book_Price { get; set; }
        public int Book_Discount_Price { get; set; }
        public int Book_Quantity { get; set; }
    }
}

