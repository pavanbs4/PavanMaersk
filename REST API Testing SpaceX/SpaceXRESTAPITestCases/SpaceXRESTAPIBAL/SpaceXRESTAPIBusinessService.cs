using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;

namespace SpaceXRESTAPIBAL
{
    public class SpaceXRESTAPIBusinessService
    {
        //Invoke the REST API using REST SHarp and get the Json Object Response
        public dynamic GetSpaceXDataFromWebAPI()
        {
            try
            {
                var restClient = new RestClient("https://api.spacexdata.com");
                var restRequest = new RestRequest("v4/launches/latest", Method.GET);
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                var restResponse = restClient.Execute(restRequest);

                dynamic jsonResponse = JsonConvert.DeserializeObject(restResponse.Content);

                return jsonResponse;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Consume the REST API using Rest Sharp and check if the output response is in JSON format
        public int APIResponseDataFormatCheck()
        {
            try
            {
                //Get Response JSON from SpaceX API
                dynamic jsonResponse = GetSpaceXDataFromWebAPI();
                string json = JsonConvert.SerializeObject(jsonResponse);

                //Check for JSON format
                if (IsValidJson(json))
                    return 1;
                else
                    return 0;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Duplicates Check "ships", "capsules", "payloads", "cores", "crew" data should not repeat - Tested "ships" for example
        public int DuplicateShipsDataCheck()
        {
            try
            {
                //Get Response JSON from SpaceX API
                dynamic jsonResponse = GetSpaceXDataFromWebAPI();

                //Get the ships key value data
                dynamic jsonObject = jsonResponse.ships;
                string[] ships = jsonObject.ToObject<string[]>();

                //Check for duplicates in the list
                if (HasDuplicates(ships))
                    return 0;
                else
                    return 1;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        //URLs Check for each of the URL exists in the "links" section - Tested "small" and "large" URLs for example
        public int CheckSmallLargeURLsExists()
        {
            try
            {
                //Get Response JSON from SpaceX API
                dynamic jsonResponse = GetSpaceXDataFromWebAPI();

                //Get small and large URL key value pairs
                dynamic jsonObject = jsonResponse.links.patch;
                string smalURL = (string)jsonObject.small;
                string largeURL = (string)jsonObject.large;

                //Check if the URLs exist
                if (!CheckUrlStatus(smalURL) || !CheckUrlStatus(largeURL))
                    return 0;
                else
                    return 1;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Check if the input string is in proper JSON format
        private static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // Check if the List Collection is having Duplicates 
        public static bool HasDuplicates<T>(IList<T> items)
        {
            Dictionary<T, bool> map = new Dictionary<T, bool>();
            for (int i = 0; i < items.Count; i++)
            {
                if (map.ContainsKey(items[i]))
                {
                    return true; // has duplicates
                }
                map.Add(items[i], true);
            }
            return false; // no duplicates
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

        




    }
}
