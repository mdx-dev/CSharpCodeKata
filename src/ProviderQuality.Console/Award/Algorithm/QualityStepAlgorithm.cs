using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award.Algorithm {
    public class QualityStepAlgorithm : IAwardAlgorithm {
        public int PreExpiration_QualityStep { get; private set; }
        public int PostExpiration_QualityStep { get; private set; }

        public QualityStepAlgorithm(int preExpiration_QualityStep, int postExpiration_QualityStep) {
            this.PreExpiration_QualityStep = preExpiration_QualityStep;
            this.PostExpiration_QualityStep = postExpiration_QualityStep;
        }

        public IAward Update(IAward award) {
            var step = this.PreExpiration_QualityStep;

            if (award.IsExpired)
                step = this.PostExpiration_QualityStep;

            award.ChangeQuality(step);
            return award;
        }
    }
}
