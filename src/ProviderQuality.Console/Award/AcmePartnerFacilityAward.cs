using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award
{
    public class AcmePartnerFacilityAward : AwardBase
    {
        public override string Name { get { return "ACME Partner Facility"; } }

        public AcmePartnerFacilityAward(int initialQuality, TimeSpan expirationTime) : base(initialQuality, expirationTime)
        {
        }
    }
}
