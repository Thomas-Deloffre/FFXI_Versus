using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus.Races
{
    public class Hume : IRace
    {
        public const int RaceId = 1;

        public string RaceName { get; set; } = "Hume";

        public string RaceIntro { get; set; } = "We cannot let this world be engulfed into darkness.. ";

        public double HpCoef { get; set; } = 1.10;

        public double MpCoef { get; set; } = 0.95;

        public double StrCoef { get; set; } = 1.10;

        public double IntCoef { get; set; } = 1.05;

        public double DexCoef { get; set; } = 1.00;

        public double AgiCoef { get; set; } = 1.05;

        public double ChaCoef { get; set; } = 0.95;

        public double EndCoef { get; set; } = 0.95;

        public double MndCoef { get; set; } = 1.05;
    }

}
