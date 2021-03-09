using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightBookingBAL;
using NUnit.Framework;


namespace FlightBookingTestCases
{
    [TestClass]
    public class PurchasePageClass
    {
        // Test Case#15: Total Cost should be Decimal
        [TestMethod]
        public void TotalCostShouldBeDecimal()
        {
            double n, totalCost;
            FlightBookingBusinessService objTotalCostDecimal = new FlightBookingBusinessService();
            totalCost = objTotalCostDecimal.GetTotalCost();
            bool isNumeric = Double.TryParse(totalCost.ToString(), out n);
            NUnit.Framework.Assert.AreEqual(true, isNumeric);
        }



    }
}
