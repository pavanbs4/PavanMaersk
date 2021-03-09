using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;
using SpaceXRESTAPIBAL;

namespace SpaceXRESTAPITestCases
{
    [TestClass]
    public class APIResponseDataCheck
    {
        //Consume the REST API using Rest Sharp and check if the output response is in JSON format
        [TestMethod]
        public void APIResponseDataFormatCheck()
        {
            SpaceXRESTAPIBusinessService objAPIJSON = new SpaceXRESTAPIBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objAPIJSON.APIResponseDataFormatCheck());
        }
    }
}
