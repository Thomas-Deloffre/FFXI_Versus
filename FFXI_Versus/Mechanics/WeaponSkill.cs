using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FFXI_Versus.Mechanics
{
    [BsonDiscriminator(Required = true)]
    public class WeaponSkill
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public int WeaponSkillId { get; set; }

        public int JobId { get; set; }

        public string WeaponSkillName { get; set; }

        public string Description { get; set; }

        public string ModAtt1 { get; set; }

        public string ModAtt2 { get; set; }

        public double WSC1 { get; set; }

        public double WSC2 { get; set; }

        public double FTPCoef1 { get; set; }

        public double FTPCoef2 { get; set; }

        public double FTPCoef3 { get; set; }


        public WeaponSkill() { }

        public WeaponSkill(ObjectId id, int weapondskillId, int jobId, string weaponskillName, string description, string modAtt1, string modAtt2, double wsc1, double wsc2, double fTPCoef1, double fTPCoef2, double fTPCoef3)
        {
            _id = id;
            WeaponSkillId = weapondskillId;
            JobId = jobId;
            WeaponSkillName = weaponskillName;
            Description = description;
            ModAtt1 = modAtt1;
            ModAtt2 = modAtt2;
            WSC1 = wsc1;
            WSC2 = wsc2;
            FTPCoef1 = fTPCoef1;
            FTPCoef2 = fTPCoef2;
            FTPCoef3 = fTPCoef3;
        }

        public static double CalcfTP(Fighter fighter, WeaponSkill weaponskill)
        {
            double fTP = 0;

            if (fighter.TpJauge < 2000)
            {
                fTP = weaponskill.FTPCoef1;
            }
            if (2000 < fighter.TpJauge && fighter.TpJauge < 3000)
            {
                fTP = weaponskill.FTPCoef2;
            }
            if (fighter.TpJauge > 3000)
            {
                fTP = weaponskill.FTPCoef3;
            }

            return fTP;
        }

        public void DisplayWeaponSkill()
        {
            Generics.SpaceWriteLine("To be implemented !");
        }


    }
}
