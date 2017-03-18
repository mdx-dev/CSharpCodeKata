using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award {
    public class GovQualityPlusAward : AwardBase {
        public override string Name { get { return "Gov Quality Plus"; } }

        public GovQualityPlusAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime) {
        }
    }
}
