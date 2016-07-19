using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using Bikes.Models;

namespace Bikes.Classes
{
	public static class ReadSqlServer
	{
		public static List<MainInfoBikes> ReadMainInfoBikes(SqlConnection _connect) 
		{
			List<MainInfoBikes> bikes = new List<MainInfoBikes>();

			string _sqlQuery =
				"select TOP 5 id, Maker, Model, Year_creation, Price, DAY(Date_add), DATENAME(MONTH, Date_add), Image, Engine, V_engine, Chassis from Product order by Date_add desc";
			
			_connect.Open();
			SqlCommand sql = new SqlCommand(_sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					bikes.Add(new MainInfoBikes(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
						reader.GetInt16(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6),
						reader.GetString(7), reader.GetString(8), reader.GetInt16(9), reader.GetString(10)));
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();
			
			return bikes;
		}

		public static List<ProductInfo> ReadProductInfos(SqlConnection _connect,string _sql, int _count)
		{
			List<ProductInfo> bikes = new List<ProductInfo>();

			int i  = 0;
			string sqlQuery =
			"select id, Maker, Model, Year_creation, Price, DAY(Date_add), DATENAME(MONTH,Date_add),YEAR(Date_add), Image, Engine, V_engine, Chassis, Horse_power, Color,Type_bike, Power, Max_KM, Max_power_KM, Max_KM_revolution from Product";

			sqlQuery += " " + _sql;
			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					if (i > _count*6-1 && i < _count*6 + 6)
					{
						bikes.Add(new ProductInfo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
							reader.GetInt16(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6),
							reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetInt16(10),
							reader.GetString(11), reader.GetInt16(12), reader.GetString(13),
							reader.GetString(14), reader.GetInt16(15), reader.GetInt16(16), reader.GetString(17),
							reader.GetString(18)));
					}
					i++;
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return bikes;
		}

		public static List<ProductInfo> ReadProductInfos(SqlConnection _connect, string _sql)
		{
			List<ProductInfo> bikes = new List<ProductInfo>();

			string sqlQuery =
			"select TOP 6 id, Maker, Model, Year_creation, Price, DAY(Date_add), DATENAME(MONTH,Date_add),YEAR(Date_add), Image, Engine, V_engine, Chassis, Horse_power, Color,Type_bike, Power, Max_KM, Max_power_KM, Max_KM_revolution from Product";

			sqlQuery += " " + _sql;

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
                    bikes.Add(new ProductInfo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetInt16(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6),
                            reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetInt16(10),
                            reader.GetString(11), reader.GetInt16(12), reader.GetString(13),
                            reader.GetString(14), reader.GetInt16(15), reader.GetInt16(16), reader.GetString(17),
                            reader.GetString(18)));
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return bikes;
		}

		public static int ReadCountProduct(SqlConnection _connect, string _sql)
		{
			string sqlQuery = "Select COUNT(*) from Product";

			sqlQuery += " " + _sql;

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			int result = 0;
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result = reader.GetInt32(0);
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();
			return result;
		}

		public static List<SelectListItem> ReadModels(SqlConnection _connect, string _maker)
		{
			string sqlQuery = "Select distinct Model from Product where Maker = '" + _maker + "'";

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<SelectListItem> result = new List<SelectListItem>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(new SelectListItem{Value = reader.GetString(0), Text = reader.GetString(0)});
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();
			return result;
		}

		public static List<string> ReadMakers(SqlConnection _connect)
		{
			string sqlQuery = "Select distinct Maker from Product";

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			List<string> result = new List<string>();
			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result.Add(reader.GetString(0));
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();
			return result;
		}

		public static ProductInfo ReadProductId(SqlConnection _connect, int? _id)
		{
			ProductInfo bikes = new ProductInfo();

			string sqlQuery =
			"select id, Maker, Model, Year_creation, Price, DAY(Date_add), DATENAME(MONTH,Date_add),YEAR(Date_add), Image, Engine, V_engine, Chassis, Horse_power, Color,Type_bike, Power, Max_KM, Max_power_KM, Max_KM_revolution from Product where id = " + _id;

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
                    bikes = new ProductInfo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetInt16(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6),
                            reader.GetInt32(7), reader.GetString(8), reader.GetString(9), reader.GetInt16(10),
                            reader.GetString(11), reader.GetInt16(12), reader.GetString(13),
                            reader.GetString(14), reader.GetInt16(15), reader.GetInt16(16), reader.GetString(17),
                            reader.GetString(18));
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return bikes;
		}

		public static User ReadUser(SqlConnection _connect, string nick)
		{
			User user = new User();

			string sqlQuery =
				"select Nick_name, First_name, Last_name, Phone, Address, Email, Image, id_user from Users where Nick_name = '" + nick + "'";
			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					user.nickName = reader.GetString(0).Replace(" ", "");
					user.firstName = reader.GetString(1).Replace(" ", "");
					user.lastName = reader.GetString(2).Replace(" ", "");
					user.phone = reader.GetString(3).Replace(" ", "");
					user.address = reader.GetString(4).Replace(" ", "");
					user.email = reader.GetString(5).Replace(" ", "");
					user.image = reader.GetString(6).Replace(" ", "");
					user.id = reader.GetInt32(7).ToString();
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return user;
		}

		public static UserViewEdit ReadUserV(SqlConnection _connect, string nick)
		{
			UserViewEdit user = new UserViewEdit();

			string sqlQuery =
				"select Nick_name, First_name, Last_name, Phone, Address, Email, Image, id_user from Users where Nick_name = '" + nick + "'";
			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			if (reader.HasRows)
			{
				while (reader.Read())
				{
					user.nickName = reader.GetString(0).Replace(" ", "");
					user.firstName = reader.GetString(1).Replace(" ", "");
					user.lastName = reader.GetString(2).Replace(" ", "");
					user.phone = reader.GetString(3).Replace(" ", "");
					user.address = reader.GetString(4).Replace(" ", "");
					user.email = reader.GetString(5).Replace(" ", "");
					user.image = reader.GetString(6).Replace(" ", "");
					user.id = reader.GetInt32(7).ToString();
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return user;
		}

		public static int ReadIdUser(SqlConnection _connect, string nick)
		{
			int id = 1;

			string sqlQuery =
				"select id_user from Users where Nick_name = '" + nick + "'";
			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			if (reader.HasRows)
			{
				if (reader.Read())
				{
					id = reader.GetInt32(0);
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return id;
		}

		public static List<BasketProduct> ReadBasketProducts(SqlConnection _connect, string listid)
		{
			List<BasketProduct> bikes = new List<BasketProduct>();

			string sqlQuery =
				"select id, Maker, Model, Year_creation, Price, Image, Engine, V_engine from Product where id  in(" + listid +
				")";

			_connect.Open();
			SqlCommand sql = new SqlCommand(sqlQuery, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					BasketProduct bike = new BasketProduct();
					bike.id = reader.GetInt32(0);
					bike.image = reader.GetString(5);
					bike.mainInfo = reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetInt16(3) + "y.";
					bike.moreInfo = reader.GetString(6) + " " + reader.GetInt16(7) + "cm^3 ";
					bike.price = reader.GetInt32(4);
                    bikes.Add(bike);
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return bikes;
		}
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>
        /// <param name="_connect"></param>
        /// <param name="_sql"></param>
        /// <returns></returns>
		public static int GetIdItem(SqlConnection _connect, string _sql)
		{
			_connect.Open();
			SqlCommand sql = new SqlCommand(_sql, _connect);

			SqlDataReader reader = sql.ExecuteReader();

			int result = 0;
			if (reader.HasRows)
			{
				if (reader.Read())
				{
					result = reader.GetInt32(0);
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();
			return result;
		}

		public static string GetIdBaketUser(SqlConnection _connect, int id)
		{
			string result = "";
			DateTime current = new DateTime();
			current = DateTime.Now;
			DateTime oute = new DateTime();
			string sql = "Select Product_id, time_create, time_out from Basket where User_id = '" + id + "'";

			_connect.Open();
			SqlCommand cmd = new SqlCommand(sql, _connect);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					oute = reader.GetDateTime(2);
					if (current < oute)
					{
						result += Convert.ToString(reader.GetInt32(0));
						result += ",";
					}
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			if (result == "")
			{
				return result;
			}
			return result.Remove(result.Length-1,1);
		}

		public static int GetCountItemBasket(SqlConnection _connect, int id)
		{
			int result = 0;
			string sql = "Select COUNT(*) from Basket where User_id = '" + id + "'";

			_connect.Open();
			SqlCommand cmd = new SqlCommand(sql, _connect);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result = reader.GetInt32(0);
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return result;

		}

		public static int GetCountItem(SqlConnection _connect, string  sql)
		{
			int result = 0;

			_connect.Open();
			SqlCommand cmd = new SqlCommand(sql, _connect);

			SqlDataReader reader = cmd.ExecuteReader();

			while (reader.HasRows)
			{
				while (reader.Read())
				{
					result = reader.GetInt32(0);
				}
				reader.NextResult();
			}
			reader.Close();
			_connect.Close();

			return result;

		}
	}
}