using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FFXI_Versus.Races
{
    public class Mithra : IRace
    {
        public const int RaceId = 4;

        public string RaceName { get; set; } = "Mithra";

        public string RaceIntro { get; set; } = "Let's rrroll into action ! ";

        public double HpCoef { get; set; } = 0.90;

        public double MpCoef { get; set; } = 0.90;

        public double StrCoef { get; set; } = 1.05;

        public double IntCoef { get; set; } = 0.95;

        public double DexCoef { get; set; } = 1.15;

        public double AgiCoef { get; set; } = 1.20;

        public double ChaCoef { get; set; } = 1.10;

        public double EndCoef { get; set; } = 0.95;

        public double MndCoef { get; set; } = 0.95;
    }
}
