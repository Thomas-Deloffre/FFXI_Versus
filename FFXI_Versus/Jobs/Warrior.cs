namespace FFXI_Versus.Jobs
{
    public class Warrior
    {
        public const int JobId = 6;

        public string JobName { get; set; } = "Warrior";

        public string JobDescription { get; set; } = "Adventurers who make their living by their agility and cunning";

        public string[] Passives { get; set; } = { "Attack Boost I", "Attack Boost II" };

        public string Ultimate { get; set; } = "Mighty Strikes";

        public double Ultimate_value { get; set; } = 0;

        public double JHpCoef { get; set; } = 1.20;

        public double JMpCoef { get; set; } = 0.75;

        public double JStrCoef { get; set; } = 1.20;

        public double JIntCoef { get; set; } = 0.80;

        public double JDexCoef { get; set; } = 1.05;

        public double JAgiCoef { get; set; } = 1.05;

        public double JChrCoef { get; set; } = 1.05;

        public double JVitCoef { get; set; } = 1.20;

        public double JMndCoef { get; set; } = 0.95;

        public double WCap { get; set; } = 3.75;

    }
}