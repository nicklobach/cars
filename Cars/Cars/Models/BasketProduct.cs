using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bikes.Models
{
	[Serializable]
	public class BasketProduct
	{
		public int id { get; set; }
		public string image { get; set; }
		public string mainInfo { get; set; }
		public string moreInfo { get; set; }
		public int price { get; set; }
	}
}