using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award;
using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Tests
{
    internal class AwardAlgorithmRunner
    {
        public List<AwardInfo> AwardInfos = new List<AwardInfo>();
        private IAwardAlgorithm Algorithm;

        public AwardAlgorithmRunner(IAwardAlgorithm algorithm)
        {
            this.Algorithm = algorithm;
        }

        public void Add(IAward award)
        {
            var info = new AwardInfo { award = award };
            info.history.Add(award.Quality);
            this.AwardInfos.Add(info);
        }
        public void AddRange(IEnumerable<IAward> awards)
        {
            foreach(var award in awards)
            {
                var info = new AwardInfo { award = award };
                info.history.Add(award.Quality);
                this.AwardInfos.Add(info);
            }
        }

        public void RunAlgorithmWithDayIncrement(int howManyTimes)
        {
            this.RunAlgorithm(howManyTimes, true);
        }
        public void RunAlgorithm(int howManyTimes, bool incrementDay = false)
        {
            foreach (var info in this.AwardInfos)
            {
                for (var i = 0; i < howManyTimes; i++)
                {
                    if(incrementDay)
                        info.award.IncrementDay();
                    info.award = this.Algorithm.Run(info.award);
                    info.history.Add(info.award.Quality);
                }
            }
        }
        public class AwardInfo
        {
            public IAward award;
            public List<int> history = new List<int>();
        }
    }
}
