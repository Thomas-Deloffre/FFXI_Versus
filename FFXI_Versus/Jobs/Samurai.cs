namespace FFXI_Versus.Jobs
{
    public class Samurai
    {
        public const int JobId = 4;

        public string JobName { get; set; } = "Samurai";

        public string JobDescription { get; set; } = "Versatile war mages wielding both black and white magic";

        public string[] Passives { get; set; } = { "Magic Boost I", "Magic Defense I" };

        public string Ultimate { get; set; } = "Meikyo Shisui";

        public double Ultimate_value { get; set; } = 0;

        public double JHpCoef { get; set; } = 1.20;

        public double JMpCoef { get; set; } = 0.80;

        public double JStrCoef { get; set; } = 1.35;

        public double JIntCoef { get; set; } = 0.85;

        public double JDexCoef { get; set; } = 1.15;

        public double JAgiCoef { get; set; } = 1.05;

        public double JChrCoef { get; set; } = 1.00;

        public double JVitCoef { get; set; } = 1.15;

        public double JMndCoef { get; set; } = 0.95;

        public double WCap { get; set; } = 3.75;

    }
}