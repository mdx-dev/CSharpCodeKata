using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award
{
    public class BlueStarAward : AwardBase
    {
        public override string Name { get { return "Blue Star"; } }

        public BlueStarAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
        }
    }
}
