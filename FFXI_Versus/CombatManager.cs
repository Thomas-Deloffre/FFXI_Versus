using FFXI_Versus.Mechanics;
using MongoDB.Driver;
using Spectre.Console;
using System.ComponentModel;
using System.Numerics;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace FFXI_Versus
{
    public class CombatManager
    {
        MongoDB.Driver.IMongoCollection<WeaponSkill> WeaponSkillsBase = WeaponskillManager.InitializeDatabase();

        MongoDB.Driver.IMongoCollection<Magic> MagicSpellBase = MagicManager.InitializeDatabase();

        public CombatManager() { }

        public int NormalAttack(Fighter fighter1, Fighter fighter2)
        {
            var fStr = CalcfStr(fighter1, fighter2);

            var fighterBaseDamage = fighter1.WeapBaseDmg + fStr;

            var pDif = fighter1.Atk / fighter2.Def;

            var fighterTotalDamage = fighterBaseDamage * pDif;

            Console.WriteLine($"{fighter1.CharName} fends off and attacks swiftly, which inflicts {fighterTotalDamage} to {fighter2.CharName} !");

            IncrementTpJauge(fighter1);

            TpGainFromHit(fighter2, fighterTotalDamage);

            return fighterTotalDamage;
        }

        public int WeaponSkill(Fighter player, Fighter opponent)
        {
            var weaponskillListPlayer = WeaponSkillsBase.Find(w => w.JobId == player.JobId).ToList();

            var weaponskillListCPU = WeaponSkillsBase.Find(w => w.JobId == opponent.JobId).ToList();

            if (player.IsPlayer)
            {
                var weaponskillName = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose a Weaponskill")
                        .PageSize(10)
                        .AddChoices(weaponskillListPlayer.Select(w => w.WeaponSkillName)
                        ));

                var weaponskill = WeaponskillManager.GetWeaponSkillByName(weaponskillName);

                var fStr = CalcfStr(player, opponent);

                var fTP = Mechanics.WeaponSkill.CalcfTP(player, weaponskill);

                var modAtt1 = GetModAtt(player, weaponskill.ModAtt1);

                var modAtt2 = GetModAtt(player, weaponskill.ModAtt2);

                var playerBaseDamage = (int)Math.Round((player.WeapBaseDmg + fStr + (modAtt1 * weaponskill.WSC1) + (modAtt2 * weaponskill.WSC2)) * fTP);

                var pDif = (int)Math.Round(Math.Min((player.Atk / opponent.Def), player.WCap));

                var playerTotalDamage = playerBaseDamage * pDif;

                Console.WriteLine($"{player.CharName} : {weaponskill.WeaponSkillName} ! It inflics {playerTotalDamage} to {opponent.CharName} !");

                EmptyTpJauge(player);

                TpGainFromHit(opponent, playerTotalDamage);

                return playerTotalDamage;
            }

            else
            {
                WeaponSkill cpuWeaponskill = null;

                double maxPotentialDamage = 0;

                foreach (var weaponSkill in weaponskillListCPU)
                {
                    double potentialDamage = CalculatePotentialWPDamage(opponent, weaponSkill);

                    if (potentialDamage > maxPotentialDamage)
                    {
                        maxPotentialDamage = potentialDamage;
                        cpuWeaponskill = weaponSkill;
                    }
                }

                var fStr = CalcfStr(opponent, player);

                var fTP = Mechanics.WeaponSkill.CalcfTP(opponent, cpuWeaponskill);

                var modAtt1 = GetModAtt(opponent, cpuWeaponskill.ModAtt1);

                var modAtt2 = GetModAtt(opponent, cpuWeaponskill.ModAtt2);

                var cpuBaseDamage = (int)Math.Round((opponent.WeapBaseDmg + fStr + (modAtt1 * cpuWeaponskill.WSC1) + (modAtt2 * cpuWeaponskill.WSC2)) * fTP);

                var pDif = (int)Math.Round(Math.Min((opponent.Atk / player.Def), opponent.WCap));

                var cpuTotalDamage = cpuBaseDamage * pDif;

                Console.WriteLine($"{opponent.CharName} : {cpuWeaponskill.WeaponSkillName} ! It inflics {cpuTotalDamage} to {player.CharName} !");

                EmptyTpJauge(opponent);

                TpGainFromHit(player, cpuTotalDamage);

                return cpuTotalDamage;
            }
        }

        public double CastSpell(Fighter player, Fighter cpu)
        {
            var magicListPlayer = MagicSpellBase.Find(w => w.JobId == player.JobId).ToList();

            var magicListCPU = MagicSpellBase.Find(w => w.JobId == cpu.JobId).ToList();

            if (player.IsPlayer)
            {
                var selectableMagicNames = magicListPlayer
                    .Where(magic => magic.MpCost <= player.Mp)  
                    .Select(magic => magic.MagicName)
                    .ToList();

                var magicName = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Choose a Spell")
                        .PageSize(10)
                        .AddChoices(magicListPlayer.Select(w => w.MagicName)
                        ));

                var magic = MagicManager.GetMagicByName(magicName);
               
                var baseMagicDamage = Magic.CalcDmagicValue(player, cpu, magic);

                var adjustedMagicCoef = Magic.CalcTMDA(cpu);

                var playerTotalDamage = baseMagicDamage * adjustedMagicCoef;

                Console.WriteLine($"{player.CharName} : {magic.MagicName} ! It inflics {playerTotalDamage} to {cpu.CharName} !");
               
                TpGainFromHit(cpu, playerTotalDamage);

                return playerTotalDamage;
            }

            else
            {
                Magic cpuMagic = null;

                double maxPotentialDamage = 0;

                foreach (var magic in magicListCPU)
                {
                    if (magic.MpCost <= cpu.Mp)
                    {
                        double potentialDamage = CalculateTotalPotentialMagicDamage(cpu, player, magic);

                        if (potentialDamage > maxPotentialDamage)
                        {
                            maxPotentialDamage = potentialDamage;
                            cpuMagic = magic;
                        }
                    }                   
                }

                if (cpuMagic != null)
                {
                    var baseMagicDamage = Magic.CalcDmagicValue(cpu, player, cpuMagic);

                    var adjustedMagicCoef = Magic.CalcTMDA(player);

                    var cpuTotalDamage = baseMagicDamage * adjustedMagicCoef;

                    Console.WriteLine($"{cpu.CharName} : {cpuMagic.MagicName} ! It inflics {cpuTotalDamage} to {player.CharName} !");

                    TpGainFromHit(player, cpuTotalDamage);

                    return cpuTotalDamage;
                }

                else
                {
                    NormalAttack(cpu, player);
                }

                return 0;
            }
        }

        public int Defend(Fighter player, Fighter opponent, int opponentTotalDamage)
        {
            var reducedDamage = opponentTotalDamage / 2;

            //ToDo : Possile Way to Implement Defense : Create an boolean Defense attribute for Fighter, False by default.
            //       When the function Defend() is triggered, it renders the boolean to True and if it's True, halves the dmg received. 

            Generics.SpaceWriteLine($"{player.CharName} is defending and prepared to whistand the next attack !");

            return reducedDamage;
        }

        public Fighter LimitBurst(Fighter player, Fighter opponent)
        {
            throw new NotImplementedException();
        }

        public int CalcfStr(Fighter player, Fighter opponent)
        {
            var fStr = (player.Str - opponent.Vit + 8) / 4;

            return fStr;
        }

        public double IncrementTpJauge(Fighter fighter)
        {
            var random = new Random();

            var incr_tpJauge = random.Next(350, 450);

            Generics.SpaceWriteLine($"{fighter.CharName}'s TP jauge raised by {incr_tpJauge}");

            return fighter.TpJauge + incr_tpJauge;
        }

        public double TpGainFromHit(Fighter fighter, double damage)
        {
            var random = new Random();

            var incr_tpJauge = random.Next(50, 70) + (int)Math.Round(damage * 0.05);

            return fighter.TpJauge + incr_tpJauge;
        }

        public double EmptyTpJauge(Fighter fighter)
        {
            return fighter.TpJauge = 0;
        }

        public int GetModAtt(Fighter fighter, string mod_Att)
        {
            var property = typeof(Fighter).GetProperty(mod_Att, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (property != null)
            {
                return (int)property.GetValue(fighter);
            }

            return 0;
        }

        public double CalculatePotentialWPDamage(Fighter opponent, WeaponSkill weaponSkill)
        {
            var fTP = Mechanics.WeaponSkill.CalcfTP(opponent, weaponSkill);

            var modAtt1 = GetModAtt(opponent, weaponSkill.ModAtt1);

            var modAtt2 = GetModAtt(opponent, weaponSkill.ModAtt2);

            double potentialDamage = (int)Math.Round((opponent.WeapBaseDmg + (modAtt1 * weaponSkill.WSC1) + (modAtt2 * weaponSkill.WSC2)) * fTP);

            return potentialDamage;
        }

        public double CalculateTotalPotentialWPDamage(Fighter player, Fighter opponent, WeaponSkill weaponSkill)
        {
            var fTP = Mechanics.WeaponSkill.CalcfTP(opponent, weaponSkill);

            var modAtt1 = GetModAtt(opponent, weaponSkill.ModAtt1);

            var modAtt2 = GetModAtt(opponent, weaponSkill.ModAtt2);

            var fStr = CalcfStr(player, opponent);

            var playerBaseDamage = (int)Math.Round((player.WeapBaseDmg + fStr + (modAtt1 * weaponSkill.WSC1) + (modAtt2 * weaponSkill.WSC2)) * fTP);
            var pDif = (int)Math.Round(Math.Min((player.Atk / opponent.Def), player.WCap));

            var totalPotentialDamage = playerBaseDamage * pDif;

            return totalPotentialDamage;
        }

        public void OpponentTurn(Fighter cpu, Fighter player)
        {
            var weaponskillListCPU = WeaponSkillsBase.Find(w => w.JobId == cpu.JobId).ToList();

            var magicsListCPU = MagicSpellBase.Find(w => w.JobId == cpu.JobId).ToList();

            if (cpu.TpJauge >= 1000)
            {
                WeaponSkill cpuWeaponSkill = null;

                Magic cpuMagic = null;

                double predicateMaxWSDamage = 0;

                double predicateMaxMagicDamage = 0;

                foreach (var weaponSkill in weaponskillListCPU)
                {
                    double predicateDamage = CalculateTotalPotentialWPDamage(cpu, player, weaponSkill);

                    if (predicateDamage > predicateMaxWSDamage)
                    {
                        predicateMaxWSDamage = predicateDamage;
                        cpuWeaponSkill = weaponSkill;
                    }
                }

                foreach (var magic in magicsListCPU)
                {
                    double predicateDamage = CalculateTotalPotentialMagicDamage(cpu, player, cpuMagic);

                    if (predicateDamage > predicateMaxMagicDamage)
                    {
                        predicateMaxMagicDamage = predicateDamage;
                        cpuMagic = magic;
                    }
                }

                if (predicateMaxWSDamage > predicateMaxMagicDamage)
                {
                    WeaponSkill(cpu, player);
                }
                else
                {
                    CastSpell(cpu, player);
                }
            }
            else
            {
                NormalAttack(cpu, player);
            }
        }

        private double CalculateTotalPotentialMagicDamage(Fighter cpu, Fighter player, Magic magic)
        {
            throw new NotImplementedException();
        }

        private int UpdateFighterMp(Fighter fighter, Magic magic)
        {
            var mpUsed = 0;

            int remainingMp = fighter.Mp - mpUsed;

            return remainingMp;
        }

        private bool CheckMpPool(Fighter fighter)
        {
            var magicsListCPU = MagicSpellBase.Find(w => w.JobId == fighter.JobId).ToList();

            var remainingMp = fighter.Mp;

            foreach (var magic in magicsListCPU)
            {
                if (magic.MpCost <= remainingMp)
                {
                    return true;
                }               
            }

            return false;
        }
    }
}