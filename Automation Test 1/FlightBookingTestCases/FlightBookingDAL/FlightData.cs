using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBookingDAL
{
    public class FlightData
    {       
        public string FlightId { get; set; }
        public string AirLine { get; set; }


        public FlightData()
            {
                FlightId = this.FlightId;
                AirLine = this.AirLine;
            }
    }
}
