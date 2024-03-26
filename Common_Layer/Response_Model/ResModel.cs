using System;
namespace Common_Layer.Response_Model
{
	public class ResModel<T>
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public T Data {get;set;}

	}
}

