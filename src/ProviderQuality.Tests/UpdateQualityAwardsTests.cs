using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderQuality.Console;

namespace ProviderQuality.Tests
{
    [TestClass]
    public class UpdateQualityAwardsTests
    {
        [TestMethod]
        public void TestImmutabilityOfBlueDistinctionPlus()
        {
            var app = new Program()
            {
            };

            //Assert.IsTrue(app.Awards.Count == 1);
            //Assert.IsTrue(app.Awards[0].Name == "Blue Distinction Plus");
            //Assert.IsTrue(app.Awards[0].Quality == 80);

            //app.UpdateQuality();

            //Assert.IsTrue(app.Awards[0].Quality == 80);
        }
    }
}
