using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Bikes.Models
{
	public class ProductInfo
	{
		public int id { get; set; }
		public string maker { get; set; }
		public string model { get; set; }
		public Int16 yearCreation { get; set; }
		public int price { get; set; }
		public int day { get; set; }
		public string month { get; set; }
		public int year { get; set; }
		public string image { get; set; }
		public string typeEngine { get; set; }
		public Int16 vEngine { get; set; }
		public string chassis { get; set; }
		public Int16 horsePower { get; set; }
		public string color { get; set; }
		public string typeBikes { get; set; }
		public Int16 power { get; set; }
		public Int16 maxKM { get; set; }
		public string maxPowerKM { get; set; }
		public string maxKMRevolution { get; set; }

		public ProductInfo()
		{
			this.id = 0;
			this.maker = "";
			this.model = "";
			this.yearCreation = 0;
			this.price = 0;
			this.day = 0;
			this.month = "";
			this.year = 0;
			this.image = "";
			this.typeEngine = "";
			this.vEngine = 0;
			this.chassis = "";
			this.horsePower = 0;
			this.color = "";
			this.typeBikes = "";
			this.power = 0;
			this.maxKM = 0;
			this.maxPowerKM = "";
			this.maxKMRevolution = "";
		}

		public ProductInfo(int _id, string _maker, string _model, Int16 _yearCreation, int _price, int _day, 
			string _month, int _year, string _image, string _typeEngine, Int16 _vEngine, string _chassis, 
			Int16 _horsePower, string _color, string _typeBikes, Int16 _power, Int16 _maxKM, string _maxPowerKM, string _maxKMRevolution)
		{
			this.id = _id;
			this.maker = _maker;
			this.model = _model;
			this.yearCreation = _yearCreation;
			this.price = _price;
			this.day = _day;

			this.month = _month;
			this.image = "../BD/" + _image;
			this.typeEngine = _typeEngine;
			this.vEngine = _vEngine;
			this.chassis = _chassis;
			this.year = _year;
			this.horsePower = _horsePower;
			this.color = _color;
			this.typeBikes = _typeBikes;
			this.power = _power;
			this.maxKM = _maxKM;
			this.maxPowerKM = _maxPowerKM;
			this.maxKMRevolution = _maxKMRevolution;
		}


		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (this.id == ((ProductInfo) obj).id)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}

	public class ItemGroup
	{
		public List<SelectListItem> maker = new List<SelectListItem>(); 
		public List<SelectListItem> model = new List<SelectListItem>();
		public List<SelectListItem> typeFuel = new List<SelectListItem>();

		public void GetData(SqlConnection _connect)
		{
			string sqlQuery = "Select distinct Maker from Product";

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					this.maker.Add(new SelectListItem{Value = reader.GetString(0), Text = reader.GetString(0)});
				}
				reader.NextResult();
			}
			reader.Close();


			sqlQuery = "Select distinct Type_fuel from Product";

			sql = new SqlCommand(sqlQuery, _connect);
			reader = sql.ExecuteReader();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					this.typeFuel.Add(new SelectListItem { Value = reader.GetString(0), Text = reader.GetString(0) });
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();
		}
	}
}