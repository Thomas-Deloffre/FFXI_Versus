namespace FFXI_Versus.Jobs
{
    public class Dragoon
    {
        public const int JobId = 7;

        public string JobName { get; set; } = "Dragoon";

        public string JobDescription { get; set; } = "Adventurers who make their living by their agility and cunning";

        public string[] Passives { get; set; } = { "Attack Boost I", "Defense Boost I" };

        public string Ultimate { get; set; } = "High Sky";

        public double Ultimate_value { get; set; } = 0;

        public double JHpCoef { get; set; } = 1.10;

        public double JMpCoef { get; set; } = 0.80;

        public double JStrCoef { get; set; } = 1.25;

        public double JIntCoef { get; set; } = 0.85;

        public double JDexCoef { get; set; } = 1.05;

        public double JAgiCoef { get; set; } = 1.05;

        public double JChrCoef { get; set; } = 1.00;

        public double JVitCoef { get; set; } = 1.10;

        public double JMndCoef { get; set; } = 1.10;

        public double WCap { get; set; } = 3.75;

    }
}