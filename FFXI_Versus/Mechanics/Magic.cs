using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FFXI_Versus.Mechanics
{
    [BsonDiscriminator(Required = true)]
    public class Magic
    {
        [BsonId]
        public ObjectId _id { get; set; }

        public int MagicId { get; set; }

        public int JobId { get; set; }

        public string MagicName { get; set; }

        public string Description { get; set; }

        public string MagicType { get; set; }

        public double MpCost { get; set; }

        public int RecastTime { get; set; }

        public int CastTime { get; set; }

        public int VBaseValue1 { get; set; }

        public int VBaseValue2 { get; set; }

        public int VBaseValue3 { get; set; }

        public int VBaseValue4 { get; set; }

        public int VBaseValue5 { get; set; }

        public int VBaseValue6 { get; set; }

        public int VBaseValue7 { get; set; }

        public int VBaseValue8 { get; set; }

        public int MCoef1 { get; set; }

        public int MCoef2 { get; set; }

        public int MCoef3 { get; set; }

        public int MCoef4 { get; set; }

        public int MCoef5 { get; set; }

        public int MCoef6 { get; set; }

        public int MCoef7 { get; set; }

        public int MCoef8 { get; set; }       


        public Magic () { }

        public Magic (ObjectId id, int magicId, int jobId, string magicName, string description, string magicType, int mpCost, int recastTime, int castTime, int vBaseValue1, int vBaseValue2, int vBaseValue3, int vBaseValue4, int vBaseValue5, int vBaseValue6, int vBaseValue7, int vBaseValue8, int mCoef1, int mCoef2, int mCoef3, int mCoef4, int mCoef5, int mCoef6, int mCoef7, int mCoef8)
        {
            _id = id;
            MagicId = magicId;
            JobId = jobId;
            MagicName = magicName;
            Description = description;
            MagicType = magicType;
            MpCost = mpCost;
            RecastTime = recastTime;
            CastTime = castTime;
            VBaseValue1 = vBaseValue1;
            VBaseValue2 = vBaseValue2;
            VBaseValue3 = vBaseValue3;
            VBaseValue4 = vBaseValue4;
            VBaseValue5 = vBaseValue5;
            VBaseValue6 = vBaseValue6;
            VBaseValue7 = vBaseValue7;
            VBaseValue8 = vBaseValue8;
            MCoef1 = mCoef1;
            MCoef2 = mCoef2;
            MCoef3 = mCoef3;
            MCoef4 = mCoef4;
            MCoef5 = mCoef5;
            MCoef6 = mCoef6;
            MCoef7 = mCoef7;
            MCoef8 = mCoef8;
        }

        public void DisplayMagic()
        {
            Generics.SpaceWriteLine("To be implemented !");
        }

        public static double CalcDmagicValue(Fighter caster, Fighter target, Magic magic)
        {
            var intDelta = caster.Int - target.Int;

            var adjustedMagicDamage = 0 + CalcVmagicValue(caster, target, magic) + ((intDelta-IntDeltaCorrecter(caster, target)+1) * CalcMmagicValue(caster, target, magic));

            return adjustedMagicDamage;
        }
      
        public static int CalcMmagicValue(Fighter caster, Fighter target, Magic magic)
        {
            var intDelta = caster.Int - target.Int;

            var valueRanges = new Dictionary<Func<int, bool>, int>
            {
                { delta => 0 <= delta && delta <= 49, magic.VBaseValue1 },
                { delta => 50 <= delta && delta <= 99, magic.VBaseValue2 },
                { delta => 100 <= delta && delta <= 199, magic.VBaseValue3 },
                { delta => 200 <= delta && delta <= 299, magic.VBaseValue4 },
                { delta => 300 <= delta && delta <= 399, magic.VBaseValue5 },
                { delta => 400 <= delta && delta <= 499, magic.VBaseValue6 },
                { delta => 500 <= delta && delta <= 599, magic.VBaseValue7 },
                { delta => 600 <= delta, magic.VBaseValue8 },
            };

            foreach (var kvp in valueRanges)
            {
                if (kvp.Key(intDelta))
                {
                    return kvp.Value;
                }
            }

            return 0;
        }

        public static int CalcVmagicValue(Fighter caster, Fighter target, Magic magic)
        {
            var intDelta = caster.Int - target.Int;
          
            var valueRanges = new Dictionary<Func<int, bool>, int>
            {
                { delta => 0 <= delta && delta <= 49, magic.MCoef1 },
                { delta => 50 <= delta && delta <= 99, magic.MCoef2 },
                { delta => 100 <= delta && delta <= 199, magic.MCoef3 },
                { delta => 200 <= delta && delta <= 299, magic.MCoef4 },
                { delta => 300 <= delta && delta <= 399, magic.MCoef5 },
                { delta => 400 <= delta && delta <= 499, magic.MCoef6 },
                { delta => 500 <= delta && delta <= 599, magic.MCoef7 },
                { delta => 600 <= delta, magic.MCoef8 },                              
            };

            foreach (var kvp in valueRanges)
            {
                if (kvp.Key(intDelta))
                {
                    return kvp.Value;
                }
            }
            
            return 0;
        }

        public static int IntDeltaCorrecter(Fighter caster, Fighter target)
        {
            var intDelta = caster.Int - target.Int;

            var valueRanges = new Dictionary<Func<int, bool>, int>
            {
                { delta => 0 <= delta && delta <= 49, 0 },
                { delta => 50 <= delta && delta <= 99, 50 },
                { delta => 100 <= delta && delta <= 199, 100 },
                { delta => 200 <= delta && delta <= 299, 200 },
                { delta => 300 <= delta && delta <= 399, 300 },
                { delta => 400 <= delta && delta <= 499, 400 },
                { delta => 500 <= delta && delta <= 599, 500 },
                { delta => 600 <= delta, 600},
            };

            foreach (var kvp in valueRanges)
            {
                if (kvp.Key(intDelta))
                {
                    return kvp.Value;
                }
            }

            return 0;
        }

        public static double CalcTMDA(Fighter target) 
        {
            double tmdaValue = 0;

            var targetActiveSpellList = target.ActiveSpellList.ToList();

            if (targetActiveSpellList.Exists(spell => spell == "Shell_III"))
            {
                return tmdaValue = 0.8125; 
            }

            if (targetActiveSpellList.Exists(spell => spell == "Shell_IV"))
            {
                return tmdaValue = 0.7383;
            }

            if (targetActiveSpellList.Exists(spell => spell == "Shell_V"))
            {
                return tmdaValue = 0.7071;
            }

            return tmdaValue;  
        }
    }
}