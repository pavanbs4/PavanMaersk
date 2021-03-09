using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBookingDAL
{
    public class CityData
    {
       public int CityId { get; set; }
       public string CityName { get; set; }

       public CityData(int cityId, string cityName)
            {
                CityId = cityId;
                CityName = cityName;
            }
    }
}
