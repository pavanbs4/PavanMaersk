using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightBookingBAL;
using NUnit.Framework;


namespace FlightBookingTestCases
{
    [TestClass]
    public class ReservePageClass
    {
        // Test Case#7: Flight#, Airline, Price, Departs, Arrives should not be NOT Blank
        [TestMethod]
        public void FlightReservationDetailsNotBlank()
        {
            FlightBookingBusinessService objFlight = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(0, objFlight.CheckFlightReservationDetailsNotBlank());
        }

        // Test Case#9: Flight# should be existing in the Selected Airline Master list
        [TestMethod]
        public void CheckFlightAirlinesIntegrity()
        {
            FlightBookingBusinessService objFlightAirlines = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objFlightAirlines.CheckFlightAirlinesIntegrity());
        }

        // Test Case#10: Price should be Decimal
        [TestMethod]
        public void PriceShouldBeNumeric()
        {
            FlightBookingBusinessService objPriceNumeric = new FlightBookingBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objPriceNumeric.PriceShouldBeDecimal());
        }



    }
}
