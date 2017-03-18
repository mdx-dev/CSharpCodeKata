using System;
using System.Collections.Generic;

using ProviderQuality.Console.Award;

namespace ProviderQuality.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Updating award metrics...!");

            var set = new AwardSet(new IAward[]
            {
                new GovQualityPlusAward(20,TimeSpan.FromDays(10)),
                new BlueFirstAward(0,TimeSpan.FromDays(2)),
                new AcmePartnerFacilityAward(7,TimeSpan.FromDays(5)),
                new BlueDistinctionPlusAward(TimeSpan.FromDays(0)),
                new BlueCompareAward(20,TimeSpan.FromDays(15)),
                new TopConnectedProvidersAward(3,TimeSpan.FromDays(6)),
                new BlueFirstAward(30,TimeSpan.FromDays(3)),
            });
            set.UpdateAwards();

            System.Console.ReadKey();
        }
    }
}
