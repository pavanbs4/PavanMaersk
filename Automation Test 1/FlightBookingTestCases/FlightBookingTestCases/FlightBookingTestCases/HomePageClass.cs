using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightBookingBAL;
using NUnit.Framework;

namespace FlightBookingTestCases
{
    [TestClass]
    public class HomePageClass
    {
        // Test Case#1: Check Departure City is Not Blank
        [TestMethod]
        public void DepartureCityNotBlank()
        {
            FlightBookingBusinessService objDepartureCity = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreNotEqual("", objDepartureCity.GetDepartureCity());
        }

        // Test Case#2: Check Destination City is Not Blank
        [TestMethod]
        public void DestinationCityNotBlank()
        {
            FlightBookingBusinessService objDestinationCity = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreNotEqual("", objDestinationCity.GetDestinationCity());
        }

        // Test Case#3: Check Departure City should be one from the Master City List
        [TestMethod]
        public void DepartureInMasterCityList()
        {
            FlightBookingBusinessService objCity = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objCity.CheckCity(objCity.GetDepartureCity()));
        }

        // Test Case#4: Check Destination City should be one from the Master City List
        [TestMethod]
        public void DestinationInMasterCityList()
        {
            FlightBookingBusinessService objCity = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objCity.CheckCity(objCity.GetDestinationCity()));
        }

        // Test Case#5: Departure and Destination should NOT be same
        [TestMethod]
        public void DepartureDestinationNotSame()
        {
            FlightBookingBusinessService objCity = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreNotEqual(objCity.GetDepartureCity(), objCity.GetDestinationCity());
        }

        // Test Case#6: Check if the URL "destination of the week! The Beach!" exists
        [TestMethod]
        public void DestOfTheWeekURLExists()
        {
            FlightBookingBusinessService objURL = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objURL.CheckURLExists("https://blazedemo.com/vacation.html"));
        }

    }
}
