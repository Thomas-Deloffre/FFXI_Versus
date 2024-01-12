using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using FFXI_Versus.Jobs;
using FFXI_Versus.Races;

namespace FFXI_Versus
{
    public abstract class Character : IFighter
    {
        [BsonIgnore]
        public int FighterId { get; set; }

        public string CharName { get; set; }

        public IJob Job { get; }

        public IRace Race{ get; }

        public string Background { get; set; }

        public string RaceIntro { get; set; }

        public string Exclamation { get; set; }

        public int Age { get; set; }

        public string[] Relatives { get; set; }

        public abstract int Hp { get; }

        public abstract int Mp { get; }

        public abstract int Str { get; }

        public abstract int Dex { get; }

        public abstract int Vit { get; }

        public abstract int Agi { get; }

        public abstract int Int { get; }

        public abstract int Mnd { get; }

        public abstract int Chr { get; }      
        
        
        public void ShowRelatives(string[] relatives)
        {    
            foreach (var r in relatives)
            {
                Console.WriteLine(CharName + $"'s relatives are {r}");
            }
            
        }

        public abstract void DisplayStats();
        
        public abstract void DisplayCharacter();

        public abstract void JoinFight();
       
        
    }
}
