using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Bikes.Classes
{
	public static class WriteSqlServer
	{
		public static bool UpdateDBonQuery(SqlConnection _connect, string _sql)
		{
			int x = 0;
			_connect.Open();
			SqlCommand sql = new SqlCommand(_sql, _connect);

			x = sql.ExecuteNonQuery();

			_connect.Close();

			if (x > 0) return true;
			else return false;

		}
	}
}