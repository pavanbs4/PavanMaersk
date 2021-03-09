using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;
using SpaceXRESTAPIBAL;

namespace SpaceXRESTAPITestCases
{
    [TestClass]
    public class URLsExistsCheck
    {
        //URLs Check for each of the URL exists in the "links" section - Tested "small" and "large" URLs for example
        [TestMethod]
        public void CheckSmallLargeURLsExists()
        {
            SpaceXRESTAPIBusinessService objURL = new SpaceXRESTAPIBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objURL.CheckSmallLargeURLsExists());
        }
    }
}
