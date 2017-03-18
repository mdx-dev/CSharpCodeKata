using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Console.Award
{
    public class BlueFirstAward : AwardBase
    {
        public override string Name { get { return "Blue First"; } }

        public BlueFirstAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
            var preExpirationStep = 1;
            var postExpirationStep = 1;
            this.UpdateAlgorithm = new QualityStepAlgorithm(preExpirationStep, postExpirationStep);
        }
    }
}
