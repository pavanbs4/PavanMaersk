using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using NUnit.Framework;
using SpaceXRESTAPIBAL;

namespace SpaceXRESTAPITestCases
{
    [TestClass]
    public class DuplicateKeyValuePairsCheck
    {
        //Duplicates Check "ships", "capsules", "payloads", "cores", "crew" data should not repeat - Tested "ships" for example
        [TestMethod]
        public void DuplicateShipsDataCheck()
        {
            SpaceXRESTAPIBusinessService objShips = new SpaceXRESTAPIBusinessService();
            NUnit.Framework.Assert.AreEqual(1, objShips.DuplicateShipsDataCheck());
        }
    }
}
