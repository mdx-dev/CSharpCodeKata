using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award;

namespace ProviderQuality.Console
{
    public class AwardSet
    {
        public List<IAward> Awards { get; private set; }

        public AwardSet(IEnumerable<IAward> awards)
        {
            this.Awards = awards.ToList();
        }

        public void UpdateAwards()
        {
            foreach(var award in Awards)
            {
                award.IncrementDay();
                award.UpdateAlgorithm.Run(award);
            }
        }

        public List<IAward> GetByCutoffQuality(int cutoffQuality)
        {
            return this.Awards.Where(award => award.Quality >= cutoffQuality).ToList();
        }

        public List<IAward> GetExpiring(int daysUntilExpiration)
        {
            return this.Awards.Where(award => award.DaysUntilExpiration >= daysUntilExpiration).ToList();
        }

        public List<IAward> GetExpired()
        {
            return this.Awards.Where(award => award.IsExpired).ToList();
        }

        public List<IAward> GetByType(Type type)
        {
            return this.Awards.Where(award => award.GetType() == type).ToList();
        }
    }
}
