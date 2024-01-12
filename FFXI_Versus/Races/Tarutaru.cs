using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FFXI_Versus.Races
{
    public class Tarutaru : IRace
    {
        public const int RaceId = 2;

        public string RaceName { get; set; } = "Tarutaru";

        public string RaceIntro { get; set; } = "Let my Magic powers help you on this fight.. ";

        public double HpCoef { get; set; } = 0.90;

        public double MpCoef { get; set; } = 1.25;

        public double StrCoef { get; set; } = 0.85;

        public double IntCoef { get; set; } = 1.20;

        public double DexCoef { get; set; } = 1.05;

        public double AgiCoef { get; set; } = 1.00;

        public double ChaCoef { get; set; } = 1.00;

        public double EndCoef { get; set; } = 0.90;

        public double MndCoef { get; set; } = 1.10;
    }
}
