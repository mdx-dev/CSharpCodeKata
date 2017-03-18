using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Console.Award
{
    public class BlueCompareAward : AwardBase
    {
        public override string Name { get { return "Blue Compare"; } }

        public BlueCompareAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
            this.UpdateAlgorithm = new BlueCompareAlgorithm();
        }
    }
}
