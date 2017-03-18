using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProviderQuality.Console.Award {
    public class BlueDistinctionPlusAward : AwardBase {
        public override string Name { get { return "Blue Distinction Plus"; } }

        private static readonly int InitialQuality = 80;

        public BlueDistinctionPlusAward(TimeSpan expirationTime) : base(InitialQuality, expirationTime) {
        }

        // TODO: consider throwing here
        //
        new public int ChangeQuality(int step) {
            return this.Quality;
        }
    }
}
