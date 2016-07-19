using System;
using Bikes.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bikes.Tests
{
	[TestClass]
	public class TestModels
	{
		[TestMethod]
		public void Test_Equal_True_MainInfoBikes()
		{
			//arrange
			MainInfoBikes info1 = new MainInfoBikes(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "");
			MainInfoBikes info2 = new MainInfoBikes(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(true, equal);
		}

		[TestMethod]
		public void Test_Equal_False_MainInfoBikes()
		{
			//arrange
			MainInfoBikes info1 = new MainInfoBikes(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "");
			MainInfoBikes info2 = new MainInfoBikes(2, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}

		[TestMethod]
		public void Test_Equal_False_On_NUll_MainInfoBikes()
		{
			//arrange
			MainInfoBikes info1 = new MainInfoBikes(1, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "");
			MainInfoBikes info2 = null;// = new MainInfoBikes(2, "Volvo", "FH16", 2002, 12345, 21, "March", "", "", 12, "", "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}

		[TestMethod]
		public void Test_Equal_True_ProductInfos()
		{
			//arrange
			ProductInfo info1 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", 12, "",
				"", 12, 12, "", "");
			
			ProductInfo info2 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", 12, "",
				"", 12, 12, "", "");

			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(true, equal);
		}

		[TestMethod]
		public void Test_Equal_False_ProductInfos()
		{
			//arrange
			ProductInfo info1 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", 12, "",
				"", 12, 12, "", "");

			ProductInfo info2 = new ProductInfo(2, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", 12, "",
				"", 12, 12, "", "");
			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}

		[TestMethod]
		public void Test_Equal_False_On_NUll_ProductInfos()
		{
			//arrange
			ProductInfo info1 = new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", 12, "",
				"", 12, 12, "", "");

			ProductInfo info2 = null;//new ProductInfo(1, "Volvo", "FH16", 2002, 12345, 21, "March", 2015, "", "", 12, "", "", 12, "",
				//"", 12, 12, "", "");
			//act
			bool equal = info1.Equals(info2);

			//Assert
			Assert.AreEqual(false, equal);
		}
	}
}
