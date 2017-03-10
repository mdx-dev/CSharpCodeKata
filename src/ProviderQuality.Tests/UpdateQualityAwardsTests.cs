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
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Distinction Plus", SellIn = 0, Quality = 80}
                }
            };

            Assert.IsTrue(app.Awards.Count == 1);
            Assert.IsTrue(app.Awards[0].Name == "Blue Distinction Plus");
            Assert.IsTrue(app.Awards[0].Quality == 80);

            app.UpdateQuality();

            Assert.IsTrue(app.Awards[0].Quality == 80);
        }

        // +++To Do - 1/10/2013: Discuss with team about adding more tests.  Seems like a lot of work for something
        //                       that probably won't change.  I watched it all in the debugger and know everything works
        //                       plus QA has already signed off and no one has complained.

        // Start New Code ---------------------------------------------------


        //Blue Star  ---------------------------------------------------   
        [TestMethod]
        public void TestBlueStarPostExpirationDepreciation()
        {
            // Case: Blue Star depreciates 2x its normal Blue Star depreciation rate after expiration (4x)
            int expected = 26;


            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Star", SellIn = -1,Quality = 30}
                }

            };

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method


        [TestMethod]
        public void TestBlueStarPreExpirationDepreciation()
        {
            // Case: Blue Star depreciates 2x the normal depreciation rate of 1

            int expected = 28;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Star", SellIn = 10,Quality = 30}
                }

            };//Initialize and awards

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method


        //Blue First  ------------------------------------------------ 
        [TestMethod]
        public void TestBlueFirstPreExpirationAppreciation()
        {
            //Case: Blue First appreciates by 1 prior to expiration
            int expected = 31;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue First", SellIn = 10,Quality = 30}
                }

            };

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method


        [TestMethod]
        public void TestBlueFirstPostExpirationDepreciation()
        {
            //Case: Blue First appreciates by 1 even after expiration
            int expected = 31;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue First", SellIn = -1,Quality = 30}
                }

            };

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method


        //Blue Compare -----------------------------------------------
        [TestMethod]
        public void TestBlueComparePreExpirationAppreciation()
        {
            //Case: Blue Compare appreciates by 1 when > 10 days to expiration
            int expected = 31;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Compare", SellIn = 15, Quality = 30}
                }

            };

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

        [TestMethod]
        public void TestBlueComparePostExpirationZeroQuality()
        {
            //Case: Blue Compare quality sets to 0 at after

            int expected = 0;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Compare", SellIn = -1, Quality = 30}
                }

            };

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

        [TestMethod]
        public void TestBlueCompareAppreciationTenDaysToExpiration()
        {
            //Case: Blue Compare appreciates by 2 when 5 < expiration date <= 10

            int expected = 32;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Compare", SellIn = 10, Quality = 30}
                }

            };

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

        [TestMethod]
        public void TestBlueCompareAppreciationFiveDaysToExpiration()
        {
            //Case: Blue Compare appreciates by 3 when expiration date <= 5

            int expected = 33;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Compare", SellIn = 5, Quality = 30}
                }

            };//Initialize and awards

            app.UpdateQuality();

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

        [TestMethod]
        public void TestNegativeQualityIsSetToZero()
        {
            //Case: Quality is never negative value

            int expected = 0;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                    new Award {Name = "Blue Compare", SellIn = 10, Quality =-5}
                }

            };

            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

        [TestMethod]
        public void TestNonBlueAwardTypePreExpirationDepreciation()
        {
            //Case: Quality depreciates by 1 prior expiration

            int expected = 29;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                       new Award {Name = "Top Connected Providres", SellIn = 10, Quality = 30}
                }

            };

            app.UpdateQuality();
            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

        [TestMethod]
        public void TestNonBlueAwardTypePostExpirationDepreciation()
        {
            //Case: Quality depreciates by 1 prior expiration

            int expected = 28;

            var app = new Program()
            {
                Awards = new List<Award>
                {
                       new Award {Name = "Top Connected Providres", SellIn = -5, Quality = 30}
                }

            };

            app.UpdateQuality();
            Assert.AreEqual(expected, app.Awards[0].Quality);

        }//end method

    }
}
