using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProviderQuality.Console.Award.Algorithm;

namespace ProviderQuality.Console.Award
{
    public interface IAward
    {
        string Name { get; }
        int Quality { get; }

        bool IsExpired          { get; }
        bool IsNotExpired       { get; }
        int DaysUntilExpiration { get; }

        IAwardAlgorithm UpdateAlgorithm { get; }

        int ChangeQuality(int step);
        int IncrementDay();
    }
}
