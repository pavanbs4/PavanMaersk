using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FlightBookingDAL
{
    public class FlightBookingDataService
    {

        //Load and Return Flight Master Data Collection
        public IList<FlightData> GetFlightMasterData()
        {
            //Flight Master Data Collection
            IList<FlightData> flightAirlinesDetails = new List<FlightData>() { 
                        new FlightData() { FlightId = "VA43", AirLine = "Virgin America" } ,
                        new FlightData() { FlightId = "UA234", AirLine = "United Airlines" } ,
                        new FlightData() { FlightId = "VA12", AirLine = "Virgin America" } ,
                        new FlightData() { FlightId = "LF4346", AirLine = "Lufthansa" } 
                    };

            return flightAirlinesDetails;
        }

        //Load and Return City Master Data
        public DataTable GetCityData()
        {
            DataSet ds = new DataSet("CityDataSet");
            DataTable dataTable = new DataTable("CityDataTable");

            DataColumn col1 = new DataColumn("CityId");
            DataColumn col2 = new DataColumn("CityName");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            ds.Tables.Add(dataTable);

            //Add City Data to the table
            DataRow newRow;

            newRow = dataTable.NewRow();
            newRow["CityId"] = "1";
            newRow["CityName"] = "Paris";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["CityId"] = "2";
            newRow["CityName"] = "Philadelphia";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["CityId"] = "3";
            newRow["CityName"] = "Rome";
            dataTable.Rows.Add(newRow);

            newRow = dataTable.NewRow();
            newRow["CityId"] = "4";
            newRow["CityName"] = "London";
            dataTable.Rows.Add(newRow);

            ds.AcceptChanges();

            return dataTable;

        }


    }
}
