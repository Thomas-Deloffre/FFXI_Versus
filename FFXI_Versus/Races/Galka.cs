using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FFXI_Versus.Races
{
    public class Galka : IRace
    {
        public const int RaceId = 5;

        public string RaceName { get; set; } = "Galka";

        public string RaceIntro { get; set; } = "May my strenght be useful to you in some way.. ";

        public double HpCoef { get; set; } = 1.15;

        public double MpCoef { get; set; } = 0.85;

        public double StrCoef { get; set; } = 1.25;

        public double IntCoef { get; set; } = 0.90;

        public double DexCoef { get; set; } = 1.05;

        public double AgiCoef { get; set; } = 0.95;

        public double ChaCoef { get; set; } = 1;

        public double EndCoef { get; set; } = 1.10;

        public double MndCoef { get; set; } = 0.95;
    }
}
