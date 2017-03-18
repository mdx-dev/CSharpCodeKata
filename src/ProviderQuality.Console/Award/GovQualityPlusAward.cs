using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Console.Award
{
    public class GovQualityPlusAward : AwardBase
    {
        public override string Name { get { return "Gov Quality Plus"; } }

        public GovQualityPlusAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
            var preExpirationStep = -1;
            var postExpirationStep = -2;
            this.UpdateAlgorithm = new QualityStepAlgorithm(preExpirationStep, postExpirationStep);
        }
    }
}
