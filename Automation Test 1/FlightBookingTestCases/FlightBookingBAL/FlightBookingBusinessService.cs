using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FlightBookingDAL;
using System.Net;

namespace FlightBookingBAL
{
    public class FlightBookingBusinessService
    {
        //Get Departure City from Web
        public string GetDepartureCity()
        {
            CityData departure = new CityData(1, "Paris");
            return departure.CityName;
        }

        //Get Destination City from Web
        public string GetDestinationCity()
        {
            CityData destination = new CityData(2, "Rome");
            return destination.CityName;
        }

        //Check City against the Master City Data 
        public int CheckCity(string cityName)
        {
            int flag = 0;
        
            DataTable cityDataTable = new DataTable();
            FlightBookingDataService objCityData = new FlightBookingDataService();

            try
            {
                //Get City Master Data by calling DAL
                cityDataTable = objCityData.GetCityData();

                //Check if the input City is present in the Master City List
                if (cityDataTable.Rows.Count > 0)
                {
                    DataColumn col = cityDataTable.Columns["CityName"];
                    foreach (DataRow row in cityDataTable.Rows)
                    {
                        if (row[col].ToString() == cityName.ToString())
                            flag = 1;
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return flag;

        }

        //Check if the input URL exists
        public int CheckURLExists(string url)
        {
            bool status = CheckUrlStatus(url);//Here I am taking the value from Textbox where user enters the website address.
            if (status == true)
                return 1;
            else
                return 0;
        }

        //Check Website Status
        protected bool CheckUrlStatus(string website)
        {
            try
            {
                var request = WebRequest.Create(website) as HttpWebRequest;
                request.Method = "HEAD";
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }

        //Check Flight#, Airline, Price, Departs, Arrives are NOT Blank
        public int CheckFlightReservationDetailsNotBlank()
        {
            int flag = 0;
            DataTable flightReservationDetailsDataTable = new DataTable();

            try
            {
                //Get Flight Reservation Details from Web
                flightReservationDetailsDataTable = GetUserSelectedFlightDetails();

                //Check Flight#, Airline, Price, Departs, Arrives are NOT Blank
                if (flightReservationDetailsDataTable.Rows.Count > 0)
                {
                    DataColumn col1 = flightReservationDetailsDataTable.Columns["FlightId"];
                    DataColumn col2 = flightReservationDetailsDataTable.Columns["Airline"];
                    DataColumn col3 = flightReservationDetailsDataTable.Columns["Departs"];
                    DataColumn col4 = flightReservationDetailsDataTable.Columns["Arrives"];
                    DataColumn col5 = flightReservationDetailsDataTable.Columns["Price"];
                    foreach (DataRow row in flightReservationDetailsDataTable.Rows)
                    {
                        if (string.IsNullOrEmpty(row[col1].ToString()) || string.IsNullOrEmpty(row[col2].ToString()) || string.IsNullOrEmpty(row[col3].ToString()) || string.IsNullOrEmpty(row[col4].ToString()) || string.IsNullOrEmpty(row[col5].ToString()))
                            flag = 1;
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return flag;
            
        }

        //Flight# should be existing in the Selected Airline Master list
        public int CheckFlightAirlinesIntegrity()
        {
            int flag = 0;
            DataTable flightReservationDetailsDataTable = new DataTable();
            FlightBookingDataService objflightAirlinesData = new FlightBookingDataService();

            try
            {
                //Get Flight Master Data by calling DAL
                IList<FlightData> flightAirlinesDataDetails = new List<FlightData>();
                flightAirlinesDataDetails = objflightAirlinesData.GetFlightMasterData();

                //Get Flight Reservation Details
                flightReservationDetailsDataTable = GetUserSelectedFlightDetails();

                //Flight# should be existing in the Selected Airline Master list
                if (flightReservationDetailsDataTable.Rows.Count > 0)
                {
                    DataColumn col1 = flightReservationDetailsDataTable.Columns["FlightId"];
                    DataColumn col2 = flightReservationDetailsDataTable.Columns["Airline"];
                    foreach (DataRow row in flightReservationDetailsDataTable.Rows)
                    {
                        //LINQ Query to find the Flight from Flight Master List for the selected Flight details
                        var flightId = from fA in flightAirlinesDataDetails
                                       where fA.AirLine.Equals(row[col2].ToString())  && fA.FlightId.Equals(row[col1].ToString())
                                       select fA;

                        foreach (var i in flightId)  
                        {  
                            if (i.FlightId.ToString().Equals(row[col1].ToString()))
                                flag = 1;
                        }  
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return flag;

        }


        //Check Price should be Decimal
        public int PriceShouldBeDecimal()
        {
            int flag = 0;
            DataTable flightReservationDetailsDataTable = new DataTable();

            try
            {
                //Get Flight Reservation Details from Web
                flightReservationDetailsDataTable = GetUserSelectedFlightDetails();

                //Check Price should be Decimal
                if (flightReservationDetailsDataTable.Rows.Count > 0)
                {
                    DataColumn col1 = flightReservationDetailsDataTable.Columns["Price"];
                    foreach (DataRow row in flightReservationDetailsDataTable.Rows)
                    {
                        double n;
                        bool isNumeric = Double.TryParse(row[col1].ToString(), out n);
                        if (isNumeric)
                            flag = 1;
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return flag;

        }

        //Total Cost should be Decimal
        public double GetTotalCost()
        {
            return 914.76;
        }


        //Check Status after Successful purchase
        public int CheckPurchaseStatus()
        {
            int flag = 0;
            DataTable confirmationIDStatusDataTable = new DataTable();

            try
            {
                //Get the Confirmation ID and Status from Web Post Successful Purchase
                confirmationIDStatusDataTable = GetConfirmationIDStatus();

                //Check Price should be Decimal
                if (confirmationIDStatusDataTable.Rows.Count > 0)
                {
                    DataColumn col1 = confirmationIDStatusDataTable.Columns["Status"];
                    foreach (DataRow row in confirmationIDStatusDataTable.Rows)
                    {
                        if (row[col1].ToString().Equals("PendingCapture"))
                            flag = 1;
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

            return flag;

        }

        //Get the User Selected Flight Details from the Reserve Web Page
        public DataTable GetUserSelectedFlightDetails()
        {
            DataSet ds = new DataSet("FlightReserveDataSet");
            DataTable dataTable = new DataTable("FlightReserveDataTable");

            DataColumn col1 = new DataColumn("FlightId");
            DataColumn col2 = new DataColumn("Airline");
            DataColumn col3 = new DataColumn("Departs");
            DataColumn col4 = new DataColumn("Arrives");
            DataColumn col5 = new DataColumn("Price");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            dataTable.Columns.Add(col3);
            dataTable.Columns.Add(col4);
            dataTable.Columns.Add(col5);
            ds.Tables.Add(dataTable);

            //Add Data to the table
            DataRow newRow;

            newRow = dataTable.NewRow();
            newRow["FlightId"] = "VA43";
            newRow["Airline"] = "Virgin America";
            newRow["Departs"] = "1:43 AM";
            newRow["Arrives"] = "9:45 PM";
            newRow["Price"] = "472.56";
            dataTable.Rows.Add(newRow);

            ds.AcceptChanges();

            return dataTable;
        }

        //Get the Confirmation ID and Status from Web Post Successful Purchase
        public DataTable GetConfirmationIDStatus()
        {
            DataSet ds = new DataSet("ConfirmationStatusDataSet");
            DataTable dataTable = new DataTable("ConfirmationStatusDataTable");

            DataColumn col1 = new DataColumn("ConfirmationId");
            DataColumn col2 = new DataColumn("Status");

            dataTable.Columns.Add(col1);
            dataTable.Columns.Add(col2);
            ds.Tables.Add(dataTable);

            //Add Data to the table
            DataRow newRow;

            newRow = dataTable.NewRow();
            newRow["ConfirmationId"] = "1615259878139";
            newRow["Status"] = "PendingCapture";
            dataTable.Rows.Add(newRow);

            ds.AcceptChanges();

            return dataTable;
        }
                

    }
}
