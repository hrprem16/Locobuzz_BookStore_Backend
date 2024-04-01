using System;
namespace Common_Layer.Request_Model
{
	public class AddAddressModel
	{
        public string Address { get; set; }
        public string LandMark { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}

