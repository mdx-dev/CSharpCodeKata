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
        public void Algorithm_BlueCompare_Expired()
        {
            var algo = new BlueCompareAlgorithm();
            IAward award = new BlueCompareAward(50,TimeSpan.FromDays(0));

            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);
            Assert.AreEqual(50,award.Quality);
            algo.Update(award);
            Assert.AreEqual(0, award.Quality);
            
            for (var i = 0; i < 25; i++)
            {
                award = algo.Update(award);
                Assert.AreEqual(0, award.Quality,
                    $"Quality is non-zero on iteration '{i}'");
            }
        }

        [TestMethod]
        public void Algorithm_BlueCompare_AcrossExpiration()
        {
            var algo = new BlueCompareAlgorithm();
            IAward award = new BlueCompareAward(50, TimeSpan.FromDays(0));

            Assert.IsFalse(award.IsExpired);
            award = algo.Update(award);
            Assert.IsTrue(award.Quality > 0);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.IsTrue(award.IsExpired);
            Assert.AreEqual(0, award.Quality);
        }

        [TestMethod]
        public void Algorithm_BlueCompare()
        {
            var algo = new BlueCompareAlgorithm();
            IAward award = new BlueCompareAward(0, TimeSpan.FromDays(15));
            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);

            runner.RunAlgorithmWithDayIncrement(16);

            var expected = new[] { 0, 1, 2, 3, 4, 6, 8, 10, 12, 14, 17, 20, 23, 26, 29, 32, 0 };

            CollectionAssert.AreEqual(expected, runner.AwardInfos[0].history);
        }

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

        [TestMethod]
        public void Algorithm_QualityStep_PreExpiration_IncrementingBy1()
        {
            var step = 1;
            var algo = new QualityStepAlgorithm(step,step);
            IAward award = new AcmePartnerFacilityAward(0,TimeSpan.FromDays(60));

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);
            runner.RunAlgorithm(50);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
            {
                Assert.AreEqual(i, history[i],$"Algorithm history mismatch on iteration '{i}'");
            }
        }

        [TestMethod]
        public void Algorithm_QualityStep_PreExpiration_IncrementingBy2()
        {
            var step = 2;
            var algo = new QualityStepAlgorithm(step, step);
            IAward award = new AcmePartnerFacilityAward(0, TimeSpan.FromDays(60));

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);

            // NOTE: we do 25 iterations to avoid the 50 quality cap which will mess with our 
            //       our nice simple loop to check qualities
            //
            runner.RunAlgorithm(25); 
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
                Assert.AreEqual(i*2, history[i], $"Algorithm history mismatch on iteration '{i}'");
        }

        [TestMethod]
        public void Algorithm_QualityStep_PreExpiration_DecrementingBy1()
        {
            var step = -1;
            var algo = new QualityStepAlgorithm(step, step);
            IAward award = new AcmePartnerFacilityAward(50, TimeSpan.FromDays(60));

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);
            runner.RunAlgorithm(50);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
                Assert.AreEqual(50-i, history[i], $"Algorithm history mismatch on iteration '{i}'");
        }

        [TestMethod]
        public void Algorithm_QualityStep_PreExpiration_DecrementingBy2()
        {
            var step = -2;
            var algo = new QualityStepAlgorithm(step, step);
            IAward award = new AcmePartnerFacilityAward(50, TimeSpan.FromDays(60));

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);
            
            runner.RunAlgorithm(25);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
                Assert.AreEqual(50-(i * 2), history[i], $"Algorithm history mismatch on iteration '{i}'");
        }







        [TestMethod]
        public void Algorithm_QualityStep_PostExpiration_IncrementingBy1()
        {
            var pre = 0;
            var post = 1;
            var algo = new QualityStepAlgorithm(pre, post);
            IAward award = new AcmePartnerFacilityAward(0, TimeSpan.FromDays(0));
            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);
            runner.RunAlgorithm(50);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
            {
                Assert.AreEqual(i, history[i], $"Algorithm history mismatch on iteration '{i}'");
            }
        }

        [TestMethod]
        public void Algorithm_QualityStep_PostExpiration_IncrementingBy2()
        {
            var pre = 0;
            var post = 2;
            var algo = new QualityStepAlgorithm(pre, post);
            IAward award = new AcmePartnerFacilityAward(0, TimeSpan.FromDays(0));

            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);

            // NOTE: we do 25 iterations to avoid the 50 quality cap which will mess with our 
            //       our nice simple loop to check qualities
            //
            runner.RunAlgorithm(25);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
                Assert.AreEqual(i * 2, history[i], $"Algorithm history mismatch on iteration '{i}'");
        }

        [TestMethod]
        public void Algorithm_QualityStep_PostExpiration_DecrementingBy1()
        {
            var pre = 0;
            var post = -1;
            var algo = new QualityStepAlgorithm(pre, post);
            IAward award = new AcmePartnerFacilityAward(50, TimeSpan.FromDays(0));

            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);
            runner.RunAlgorithm(50);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
                Assert.AreEqual(50 - i, history[i], $"Algorithm history mismatch on iteration '{i}'");
        }

        [TestMethod]
        public void Algorithm_QualityStep_PostExpiration_DecrementingBy2()
        {
            var pre = 0;
            var post = -2;
            var algo = new QualityStepAlgorithm(pre, post);
            IAward award = new AcmePartnerFacilityAward(50, TimeSpan.FromDays(0));

            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);

            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);

            runner.RunAlgorithm(25);
            var history = runner.AwardInfos[0].history;
            for (var i = 0; i < history.Count; i++)
                Assert.AreEqual(50 - (i * 2), history[i], $"Algorithm history mismatch on iteration '{i}'");
        }

        [TestMethod]
        public void Algorithm_QualityStep_AcrossExpiration_Incrementing()
        {
            var pre = 1;
            var post = 2;
            var algo = new QualityStepAlgorithm(pre, post);
            IAward award = new AcmePartnerFacilityAward(0, TimeSpan.FromDays(1));

            award.IncrementDay();
            Assert.IsFalse(award.IsExpired);
            award = algo.Update(award);

            Assert.AreEqual(1, award.Quality);

            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);
            award = algo.Update(award);
            Assert.AreEqual(3, award.Quality);
        }

        [TestMethod]
        public void Algorithm_QualityStep_AcrossExpiration_Decrementing()
        {
            var pre = -1;
            var post = -2;
            var algo = new QualityStepAlgorithm(pre, post);
            IAward award = new AcmePartnerFacilityAward(10, TimeSpan.FromDays(1));

            award.IncrementDay();
            Assert.IsFalse(award.IsExpired);
            award = algo.Update(award);

            Assert.AreEqual(9, award.Quality);

            award.IncrementDay();
            Assert.IsTrue(award.IsExpired);
            award = algo.Update(award);
            Assert.AreEqual(7, award.Quality);
        }

        [TestMethod]
        public void Award_AcmePartnerFacility()
        {
            IAward award = new AcmePartnerFacilityAward(10, TimeSpan.FromDays(1));
            var algo = award.UpdateAlgorithm;

            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);
            Assert.AreEqual(10, award.Quality);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(9, award.Quality);
            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(7, award.Quality);
            Assert.IsTrue(award.IsExpired);
            Assert.IsFalse(award.IsNotExpired);
        }

        [TestMethod]
        public void Award_BlueCompare()
        {
            IAward award = new BlueCompareAward(0, TimeSpan.FromDays(15));
            var algo = award.UpdateAlgorithm;
            
            var runner = new AwardAlgorithmRunner(algo);
            runner.Add(award);

            runner.RunAlgorithmWithDayIncrement(16);

            var expected = new[] { 0, 1, 2, 3, 4, 6, 8, 10, 12, 14, 17, 20, 23, 26, 29, 32, 0 };

            CollectionAssert.AreEqual(expected, runner.AwardInfos[0].history);
        }

        [TestMethod]
        public void Award_BlueDistinctionPlus()
        {
            IAward award = new BlueDistinctionPlusAward(TimeSpan.FromDays(1));
            var algo = award.UpdateAlgorithm;

            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);
            Assert.AreEqual(80, award.Quality);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(80, award.Quality);
            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(80, award.Quality);
            Assert.IsTrue(award.IsExpired);
            Assert.IsFalse(award.IsNotExpired);
        }

        [TestMethod]
        public void Award_BlueFirstAward()
        {
            IAward award = new BlueFirstAward(10, TimeSpan.FromDays(1));
            var algo = award.UpdateAlgorithm;

            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);
            Assert.AreEqual(10, award.Quality);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(11, award.Quality);
            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(12, award.Quality);
            Assert.IsTrue(award.IsExpired);
            Assert.IsFalse(award.IsNotExpired);
        }

        [TestMethod]
        public void Award_BlueStar()
        {
            IAward award = new BlueStarAward(10, TimeSpan.FromDays(1));
            var algo = award.UpdateAlgorithm;

            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);
            Assert.AreEqual(10, award.Quality);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(8, award.Quality);
            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(4, award.Quality);
            Assert.IsTrue(award.IsExpired);
            Assert.IsFalse(award.IsNotExpired);
        }

        [TestMethod]
        public void Award_GovQualityPlusAward()
        {
            IAward award = new GovQualityPlusAward(10, TimeSpan.FromDays(1));
            var algo = award.UpdateAlgorithm;

            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);
            Assert.AreEqual(10, award.Quality);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(9, award.Quality);
            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(7, award.Quality);
            Assert.IsTrue(award.IsExpired);
            Assert.IsFalse(award.IsNotExpired);
        }

        [TestMethod]
        public void Award_TopConnectedProviders()
        {
            IAward award = new TopConnectedProvidersAward(10, TimeSpan.FromDays(1));
            var algo = award.UpdateAlgorithm;

            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);
            Assert.AreEqual(10, award.Quality);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(9, award.Quality);
            Assert.IsFalse(award.IsExpired);
            Assert.IsTrue(award.IsNotExpired);

            award.IncrementDay();
            award = algo.Update(award);
            Assert.AreEqual(7, award.Quality);
            Assert.IsTrue(award.IsExpired);
            Assert.IsFalse(award.IsNotExpired);
        }
    }
}
