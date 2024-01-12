using MongoDB.Bson.Serialization.Attributes;

namespace FFXI_Versus.Jobs
{
    class BlackMage : IJob
    {
        public const int JobId = 2;

        public string JobName { get; set; } = "BlackMage";

        public string JobDescription { get; set; } = "Powerful war wizards wielding black magic";

        public string[] Passives { get; set; } = { "Magic Boost I", "Magic Boost II" };

        public string Ultimate { get; set; } = "Mana Well";

        public double Ultimate_value { get; set; } = 0;

        public double JHpCoef { get; set; } = 0.75;

        public double JMpCoef { get; set; } = 1.35;

        public double JStrCoef { get; set; } = 0.80;

        public double JIntCoef { get; set; } = 1.25;

        public double JDexCoef { get; set; } = 0.90;

        public double JAgiCoef { get; set; } = 0.95;

        public double JChrCoef { get; set; } = 1.05;

        public double JVitCoef { get; set; } = 0.90;

        public double JMndCoef { get; set; } = 1.10;

        public double WCap { get; set; } = 3.25;
    }

}