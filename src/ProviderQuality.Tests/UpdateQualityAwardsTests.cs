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
    }
}
