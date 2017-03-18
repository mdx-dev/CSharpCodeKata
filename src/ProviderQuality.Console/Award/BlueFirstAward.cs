using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award
{
    public class BlueFirstAward : AwardBase
    {
        public override string Name { get { return "Blue First"; } }

        public BlueFirstAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
        }
    }
}
