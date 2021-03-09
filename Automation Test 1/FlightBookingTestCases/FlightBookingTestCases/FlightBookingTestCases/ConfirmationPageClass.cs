using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightBookingBAL;
using NUnit.Framework;


namespace FlightBookingTestCases
{
    [TestClass]
    public class ConfirmationPageClass
    {
        // Test Case#20: Status should be "PendingCapture" on confirmation after Successful Purchase
        [TestMethod]
        public void CheckPurchaseStatus()
        {
            FlightBookingBusinessService objStatus = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objStatus.CheckPurchaseStatus());
        }



    }
}
