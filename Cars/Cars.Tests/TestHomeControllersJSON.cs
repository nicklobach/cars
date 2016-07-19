using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Bikes.Controllers;
using Bikes.Models;
using System.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bikes.Tests
{
	[TestClass]
	public class TestHomeControllersJSON
	{
		[TestMethod]
		public void Test_GetMakers()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			JsonResult result = controller.GetMakers() as JsonResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetProductDate()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "where Model = 'FH16'";

			// Act
			JsonResult result = controller.GetProductDate(sql) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetProductDateNumber()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "where Model = 'FH16'";

			// Act
			JsonResult result = controller.GetProductDateNamber(sql, 1) as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetCountProduct()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "where Model = 'FH16'";

			// Act
			JsonResult result = controller.GetCountProduct(sql) as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_GetCountProduct_One()
		{
			// Arrange
			HomeController controller = new HomeController();

			var sql = "";

			// Act
			JsonResult result = controller.GetCountProduct(sql) as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_BikesModelItem()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			JsonResult result = controller.BikesModelItem() as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void Test_MakerInCar()
		{
			// Arrange
			HomeController controller = new HomeController();

			// Act
			JsonResult result = controller._MakersInBikes("Volvo") as JsonResult;

			// Assert 
			Assert.IsNotNull(result);
		}
	}
}
