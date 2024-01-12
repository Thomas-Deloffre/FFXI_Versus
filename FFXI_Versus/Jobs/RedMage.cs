using MongoDB.Bson.Serialization.Attributes;
using System.Threading;

namespace FFXI_Versus.Jobs
{
    public class RedMage : IJob
    {
        public const int JobId = 3;

        public string JobName { get; set; } = "RedMage";

        public string JobDescription { get; set; } = "Versatile war mages wielding both black and white magic";

        public string[] Passives { get; set; } = { "Magic Boost I", "Magic Defense I" };

        public string Ultimate { get; set; } = "Last Resort";

        public double Ultimate_value { get; set; } = 0;

        public double JHpCoef { get; set; } = 0.95;

        public double JMpCoef { get; set; } = 1.25;

        public double JStrCoef { get; set; } = 1.05;

        public double JIntCoef { get; set; } = 1.15;

        public double JDexCoef { get; set; } = 0.95;

        public double JAgiCoef { get; set; } = 0.90;

        public double JChrCoef { get; set; } = 1.05;

        public double JVitCoef { get; set; } = 0.95;

        public double JMndCoef { get; set; } = 1.10;

        public double WCap { get; set; } = 3.25;

    }
}