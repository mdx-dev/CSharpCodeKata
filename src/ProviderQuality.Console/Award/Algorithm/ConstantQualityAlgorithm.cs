using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award.Algorithm {
    public class ConstantQualityAlgorithm : IAwardAlgorithm {
        public IAward Update(IAward award) {
            return award;
        }
    }
}
