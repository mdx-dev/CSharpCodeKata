using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProviderQuality.Console;
using ProviderQuality.Console.Award;
using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Tests
{
    [TestClass]
    public class UpdateQualityAwardsTests
    {
        [TestMethod]
        public void Algorithm_ConstantQuality()
        {
            var algo = new ConstantQualityAlgorithm();
            IAward award = new BlueDistinctionPlusAward(TimeSpan.FromDays(0));

            var originalQuality = award.Quality;
            for(var i = 0; i < 50; i++)
            {
                award = algo.Update(award);
                Assert.AreEqual(originalQuality, award.Quality, 
                    $"Quality changed from '{originalQuality}' to '{award.Quality}' on iteration '{i}'");
            }
        }
    }
}
