using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Net.Http.Formatting;
using System.Web.Providers;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Bikes.Classes;
using Bikes.Models;

namespace Bikes.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
		SqlConnection connect = new SqlConnection(@"Data Source=NICK-PC\NICK;Initial Catalog=BIKES;Integrated Security=True");

        public ActionResult Index()
        {
			SqlConnection connect = new SqlConnection(@"Data Source=NICK-PC\NICK;Initial Catalog=BIKES;Integrated Security=True");
			
			return View(ReadSqlServer.ReadMainInfoBikes(connect));
        }

	    public ActionResult Bikes()
	    {
		    return View();
		}

		public ActionResult About()
		{
			ViewBag.Title = "About";
			return View();
		}

		[HttpGet]
	    public ActionResult Detail(int id)
	    {
		    return View(ReadSqlServer.ReadProductId(connect, id));
	    }

	    public ActionResult Basket()
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    string listid = "";
			    int userId = ReadSqlServer.GetIdItem(connect,
				    "select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

			    listid = ReadSqlServer.GetIdBaketUser(connect, userId);
			    if (listid == "")
			    {
				    return View(new List<BasketProduct>());
			    }
			    return View(ReadSqlServer.ReadBasketProducts(connect, listid));
		    }
		    else
		    {
			    return RedirectToAction("Index");
		    }
	    }

	    [HttpGet]
	    public ActionResult AddBasket(int id)
	    {
		    if (User.Identity.IsAuthenticated)
		    {
			    DateTime time_creation = new DateTime();
			    time_creation = DateTime.Now;
			    string create = String.Format("{0}/{1}/{2} {3}:{4}:{5}",
				    time_creation.Day, time_creation.Month, time_creation.Year, time_creation.Hour, time_creation.Minute,
				    time_creation.Second);

			    DateTime time_out = new DateTime();
			    time_out = time_creation.AddDays(20);
			    string oute = String.Format("{0}/{1}/{2} {3}:{4}:{5}", time_out.Day, time_out.Month, time_out.Year, time_out.Hour,
				    time_out.Minute, time_out.Second);
			    string sql = "select id_user from Users where Nick_name = '" + User.Identity.Name + "'";

			    int userId = ReadSqlServer.GetIdItem(connect, sql);

			    sql = "Select COUNT(*)  from Basket where User_id = " + userId + " And Product_id = " + id + "";

			    if (ReadSqlServer.GetCountItem(connect, sql) > 0)
			    {
				    return RedirectToAction("Detail", "Home", new RouteValueDictionary(new {id = id}));
			    }
			    else
			    {
				    sql =
					    "Insert into Basket(User_id, Product_id, time_create, time_out) values('" + userId + "','" + id +
					    "',Convert(datetime,'" + create + "',103),Convert(datetime,'" + oute + "',103))";

				    WriteSqlServer.UpdateDBonQuery(connect, sql);

				    return RedirectToAction("Bikes", "Home");
			    }
		    }
		    else
		    {
				return RedirectToAction("Detail", "Home", new RouteValueDictionary(new { id = id }));
		    }
	    }

		[HttpGet]
		public ActionResult DeleteBasket(int id)
		{
			if (User.Identity.IsAuthenticated)
			{
				string sql = "select id_user from Users where Nick_name = '" + User.Identity.Name + "'";

				int userId = ReadSqlServer.GetIdItem(connect, sql);

				sql = "delete from Basket where User_id = " + userId + " And Product_id = " + id + "";

				WriteSqlServer.UpdateDBonQuery(connect, sql);

				return RedirectToAction("Basket");
			}
			else
			{
				return RedirectToAction("Basket");
			}
		}

		[HttpGet]
		public ActionResult DeleteAllBasket()
		{
			if (User.Identity.IsAuthenticated)
			{
				string sql = "select id_user from Users where Nick_name = '" + User.Identity.Name + "'";

				int userId = ReadSqlServer.GetIdItem(connect, sql);

				sql = "delete from Basket where User_id = " + userId + "";

				WriteSqlServer.UpdateDBonQuery(connect, sql);

				return RedirectToAction("Basket");
			}
			else
			{
				return RedirectToAction("Basket");
			}
		}

	    /*Json object for Angular*/

		/*Index.cshtml*/
	    public JsonResult GetMakers()
	    {
		    return Json(ReadSqlServer.ReadMakers(connect), JsonRequestBehavior.AllowGet);
	    }

	    public int GetCountItemBasket()
	    {
			if (User.Identity.IsAuthenticated)
		    {
				int userId = ReadSqlServer.GetIdItem(connect,
					"select id_user from Users where Nick_name = '" + User.Identity.Name + "'");

			    int count = ReadSqlServer.GetCountItemBasket(connect, userId);

			    return count;
		    }
		    return 0;
	    }
		/*----------------------*/

		/*Bikes.cshtml*/
	    public JsonResult GetProductDate(string sql)
	    {
			IEnumerable<ProductInfo> product = ReadSqlServer.ReadProductInfos(connect,sql);

		    return Json(product, JsonRequestBehavior.AllowGet);
	    }

		public JsonResult GetProductDateNamber(string sql,int number)
		{
			IEnumerable<ProductInfo> product = ReadSqlServer.ReadProductInfos(connect, sql, number);

			return Json(product, JsonRequestBehavior.AllowGet);
		}

	    public JsonResult GetCountProduct(string sql)
	    {
			List<Pages> page = new List<Pages>();
		    int count = ReadSqlServer.ReadCountProduct(connect, sql);
		    for (int i = 0; i < Math.Ceiling((double)count/6); i++)
		    {
			    if (i == 0)
			    {
				    page.Add(new Pages(i + 1, "active"));
			    }
			    else
			    {
				    page.Add(new Pages(i+1, ""));
			    }
		    }

		    return Json(page, JsonRequestBehavior.AllowGet);
	    }

	    public JsonResult BikesModelItem()
	    {
			ItemGroup item = new ItemGroup();

			item.GetData(connect);
		    return Json(item, JsonRequestBehavior.AllowGet);
	    }

		public JsonResult _MakersInBikes(string maker)
		{
			List<SelectListItem> model = ReadSqlServer.ReadModels(connect, maker);

			return Json(model, JsonRequestBehavior.AllowGet);
		}
		/*-----------------------------------*/
		/*----------------------------------------------*/
    }
}
