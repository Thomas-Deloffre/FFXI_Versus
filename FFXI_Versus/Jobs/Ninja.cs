using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus.Jobs
{
    class Ninja : IJob
    {
        public const int JobId = 1;

        public string JobName { get; set; } = "Ninja";

        public string JobDescription { get; set; } = "Masters of stealth and deception";

        public string[] Passives { get; set; } = { "Dual wield", "Subtle blow I" };

        public string Ultimate { get; set; } = "Death Blow";

        public double Ultimate_value { get; set; } = 0;

        public double JHpCoef { get; set; } = 0.95;

        public double JMpCoef { get; set; } = 0.70;

        public double JStrCoef { get; set; } = 1.15;

        public double JIntCoef { get; set; } = 0.90;

        public double JDexCoef { get; set; } = 1.15;

        public double JAgiCoef { get; set; } = 1.15;

        public double JChrCoef { get; set; } = 1.05;

        public double JVitCoef { get; set; } = 0.95;

        public double JMndCoef { get; set; } = 0.85;

        public double WCap { get; set; } = 3.25;

    }
}
