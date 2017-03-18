using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award
{
    public class TopConnectedProvidersAward : AwardBase
    {
        public override string Name { get { return "Top Connected Providers"; } }

        public TopConnectedProvidersAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
        }
    }
}
