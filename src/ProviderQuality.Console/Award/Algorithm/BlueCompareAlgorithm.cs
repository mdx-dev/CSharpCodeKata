using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award.Algorithm
{
    public class BlueCompareAlgorithm : IAwardAlgorithm
    {
        public IAward Run(IAward award)
        {
            if(award.IsExpired)
            {
                award.ChangeQuality(-1 * award.Quality);
                return award;
            }
            var qualityStep = 1;

                 if (award.DaysUntilExpiration <= 5 ) qualityStep = 3;
            else if (award.DaysUntilExpiration <= 10) qualityStep = 2;

            award.ChangeQuality(qualityStep);
            return award;
        }
    }
}
