using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Console.Award
{
    abstract public class AwardBase : IAward
    {
        abstract public string Name { get; }

        public int Quality             { get; protected set; }
        public int DaysUntilExpiration { get; private set; }
        public bool IsExpired          { get { return DaysUntilExpiration < 0; } }
        public bool IsNotExpired       { get { return !this.IsExpired; } }

        public IAwardAlgorithm UpdateAlgorithm { get; protected set; }

        public AwardBase(int initialQuality, TimeSpan expirationTime)
        {
            this.Quality = initialQuality;
            this.DaysUntilExpiration = expirationTime.Days;

            if (this.Quality < 0) this.Quality = 0;
            if (this.DaysUntilExpiration < -1) this.DaysUntilExpiration = -1;
        }

        public int ChangeQuality(int step)
        {
            this.Quality += step;
            if (this.Quality > 50) this.Quality = 50;
            if (this.Quality < 0 ) this.Quality = 0;
            return this.Quality;
        }

        public int IncrementDay()
        {
            if (this.DaysUntilExpiration > -1)
                this.DaysUntilExpiration--;
            return this.DaysUntilExpiration;
        }
    }
}
