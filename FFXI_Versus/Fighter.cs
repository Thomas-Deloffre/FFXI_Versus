using System.Text.Json.Serialization;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Threading;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization;
using ZstdSharp;
using System.Numerics;
using FFXI_Versus.Jobs;
using FFXI_Versus.Races;

namespace FFXI_Versus
{
    [BsonDiscriminator(Required = true)]   
    public class Fighter : Character, IRace, IJob
    {
        //Fighter params and Base stats
        [BsonId]
        public ObjectId _id { get; set; }

        public int FighterId { get; set; }
        public int BaseHp { get; set; }
        public int BaseMp { get; set; }
        public int BaseStr { get; set; }
        public int BaseInt { get; set; }
        public int BaseDex { get; set; }
        public int BaseAgi { get; set; }
        public int BaseChr { get; set; }
        public int BaseVit { get; set; }
        public int BaseMnd { get; set; }
        public string[] ActiveSpellList { get; set; }
        public double TpJauge { get; set; }
        public bool IsPlayer { get; set; } = true;

        [BsonIgnore]
        public IRace Race { get; set; }

        [BsonIgnore]
        public IJob Job { get; set; }

        //IRace params and coefs
        public int RaceId { get; set; }
        public string RaceName { get; set; }
        public double HpCoef { get; set; }
        public double MpCoef { get; set; }
        public double StrCoef { get; set; }
        public double IntCoef { get; set; }
        public double DexCoef { get; set; }
        public double AgiCoef { get; set; }
        public double ChaCoef { get; set; }
        public double EndCoef { get; set; }
        public double MndCoef { get; set; }

        //IJob params and coefs
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string JobDescription { get; set; }
        public string[] Passives { get; set; }
        public string Ultimate { get; set; }
        public double Ultimate_value { get; set; }
        public double JHpCoef { get; set; }
        public double JMpCoef { get; set; }
        public double JStrCoef { get; set; }
        public double JIntCoef { get; set; }
        public double JDexCoef { get; set; }
        public double JAgiCoef { get; set; }
        public double JChrCoef { get; set; }
        public double JVitCoef { get; set; }
        public double JMndCoef { get; set; }
        public double WCap {  get; set; }


        //Character stats with coefs
        [BsonElement("Hp")]
        public override int Hp => (int)(BaseHp * JHpCoef * HpCoef);
        [BsonElement("Mp")]
        public override int Mp => (int)(BaseMp * JMpCoef * MpCoef);
        [BsonElement("Str")]
        public override int Str => (int)(BaseStr * JStrCoef * StrCoef);
        [BsonElement("Int")]
        public override int Int => (int)(BaseInt * JIntCoef * IntCoef);
        [BsonElement("Dex")]
        public override int Dex => (int)(BaseDex * JDexCoef * DexCoef);
        [BsonElement("Agi")]
        public override int Agi => (int)(BaseAgi * JAgiCoef * AgiCoef);
        [BsonElement("Chr")]
        public override int Chr => (int)(BaseChr * JChrCoef * ChaCoef);
        [BsonElement("Vit")]
        public override int Vit => (int)(BaseVit * JVitCoef * EndCoef);
        [BsonElement("Mnd")]
        public override int Mnd => (int)(BaseMnd * JMndCoef * MndCoef);
        [BsonElement("Atk")]
        public int Atk => (int)Math.Ceiling(8 + 460 + (Str / 2.0));
        [BsonElement("Def")]
        public int Def => (int)Math.Ceiling(Vit * 1.5) + 131 + 18 + (int)Math.Ceiling((131-89.1)/2);
        [BsonElement("WeapBaseDmg")]
        public int WeapBaseDmg {  get; set; }
        public int Spd => (int)Math.Ceiling((Str + Agi + Dex + Int + Chr) / 5.1);
        [BsonElement("WsList")]
        public int[] WsIdList { get; set; }

        public Fighter() { }

        public Fighter(ObjectId id, int fighterId, string charname, int raceId, int jobId, string background, string exclamation, int age, string[] relatives, int baseHp, int baseMp, int baseStr, int baseDex, int baseVit, int baseAgi, int baseInt, int baseMnd, int baseChr, int weapBaseDmg, int[] wsIdList)
        {
            _id = _id;
            FighterId = fighterId;
            CharName = charname;
            RaceId = raceId;
            //Dynamic charge of the races and jobs
            Race = LoadRaceFromRaceId(raceId);
            JobId = jobId;
            //Dynamic charge of the races and jobs
            Job = LoadJobFromJobId(jobId);          
            Background = background;
            Exclamation = exclamation;
            Age = age;
            Relatives = relatives;
            BaseHp = baseHp;
            BaseMp = baseMp;
            BaseStr = baseStr;
            BaseDex = baseDex;
            BaseVit = baseVit;
            BaseAgi = baseAgi;
            BaseInt = baseInt;
            BaseMnd = baseMnd;
            BaseChr = baseChr;           
            WeapBaseDmg = weapBaseDmg;
            WsIdList = wsIdList;

           
            //To Not include in Database, coefs are for final stats calculation purpose only.
            HpCoef = Race.HpCoef;
            MpCoef = Race.MpCoef;
            StrCoef = Race.StrCoef;
            IntCoef = Race.IntCoef;
            DexCoef = Race.DexCoef;
            AgiCoef = Race.AgiCoef;
            ChaCoef = Race.ChaCoef;
            EndCoef = Race.EndCoef;
            MndCoef = Race.MndCoef;

            JHpCoef = Job.JHpCoef;
            JMpCoef = Job.JMpCoef;
            JStrCoef = Job.JStrCoef;
            JIntCoef = Job.JIntCoef;
            JDexCoef = Job.JDexCoef;
            JAgiCoef = Job.JAgiCoef;
            JChrCoef = Job.JChrCoef;
            JVitCoef = Job.JVitCoef;
            JMndCoef = Job.JMndCoef;

        }

        private IJob LoadJobFromJobId(int jobId)
        {           
            switch (jobId)
            {
                case BlackMage.JobId:
                    return new BlackMage();
                case RedMage.JobId:
                    return new RedMage();
                case Ninja.JobId:
                    return new Ninja();
                default:
                    return null; // ou lancez une exception si nécessaire
            }
        }

        private IRace LoadRaceFromRaceId(int raceId)
        {
            switch (raceId)
            {
                case Hume.RaceId:
                    return new Hume();
                case Tarutaru.RaceId:
                    return new Tarutaru();
                default:
                    return null;  // ou lancez une exception si nécessaire
            }
        }
            
        public override void DisplayCharacter()
        {
            Generics.SpaceWriteLine(CharName + $"\n race : {RaceName} \n Story : {Background} \n {CharName}'stats -> \n Job : {JobName}," +
                $" \n Hp : {Hp}, Mp : {Mp}, Str : {Str}, Int : {Int}, " +
                $"Dex : {Dex}, Agi : {Agi}, Cha : {Chr}, Vit : {Vit}, Mnd : {Mnd} " + "\n");

        }

        public override void DisplayStats()
        {
            Generics.SpaceWriteLine(CharName + $"'stats -> \n Job : {JobName}, \n Hp : {Hp}, Mp : {Mp}, Str : {Str}, Int : {Int}, " +
                $"Dex : {Dex}, Agi : {Agi}, Chr : {Chr}, Vit : {Vit}, Mnd : {Mnd} " + "\n");
        }

        public override void JoinFight()
        {
            Generics.CharSpeaks($"{CharName}: {RaceIntro} {Exclamation}");
        }

        public Fighter ActivateFighterPassivesAtBattleStart(Fighter fighter)
        {
            //ToDo

            Generics.SpaceWriteLine($"{fighter.CharName}'s passives have been triggered !");

            return fighter;
        }       
    }
}