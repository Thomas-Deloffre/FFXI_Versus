using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus
{
    public class FighterMap : BsonClassMap<Fighter>
    {
        public FighterMap()
        {
            MapIdProperty(c => c._id)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));

            MapProperty(c => c.FighterId)
                .SetElementName("FighterId");

            MapProperty(c => c.CharName)
                .SetElementName("CharName");

            MapProperty(c => c.RaceId)
                .SetElementName("RaceId");

            MapProperty(c => c.RaceName)
                .SetElementName("RaceName");

            MapProperty(c => c.JobId)
                .SetElementName("JobId");

            MapProperty(c => c.JobName)
                .SetElementName("JobName");

            MapProperty(c => c.JobDescription)
                .SetElementName("JobDescription");

            MapProperty(c => c.Background)
                .SetElementName("Background");

            MapProperty(c => c.Exclamation)
                .SetElementName("Exclamation");

            MapProperty(c => c.Age)
                .SetElementName("Age");

            MapProperty(c => c.Relatives)
                .SetElementName("Relatives");

            MapProperty(c => c.BaseHp)
                .SetElementName("BaseHp");

            MapProperty(c => c.BaseMp)
                .SetElementName("BaseMp");

            MapProperty(c => c.BaseStr)
                .SetElementName("BaseStr");

            MapProperty(c => c.BaseDex)
                .SetElementName("BaseDex");

            MapProperty(c => c.BaseVit)
                .SetElementName("BaseCVit");

            MapProperty(c => c.BaseAgi)
                .SetElementName("BaseAgi");

            MapProperty(c => c.BaseInt)
                .SetElementName("BaseInt");           
          
            MapProperty(c => c.BaseMnd)
                .SetElementName("BaseCMnd");

            MapProperty(c => c.BaseChr)
                .SetElementName("BaseChr");

        }
    }
}
