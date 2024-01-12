using FFXI_Versus.Mechanics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FFXI_Versus
{
    public class MagicManager
    {
        public static string ConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        public static IMongoCollection<Magic> InitializeDatabase()
        {

            if (ConnectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }

            var settings = MongoClientSettings.FromConnectionString(ConnectionString);

            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);

            try
            {
                IMongoCollection<Magic> versus_database = client.GetDatabase("FFXI_Versus").GetCollection<Magic>("Magic_list");

                return versus_database;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }

        public static Magic DeleteMagicByName(string wsName)
        {
            IMongoCollection<Magic> Magics = InitializeDatabase();

            var filter = Builders<Magic>.Filter
                .Eq(c => c.MagicName, wsName);

            if (filter == null)
            {
                throw new InvalidOperationException($"No Magic has been found with the name {wsName}");
            }

            Magic Magic = Magics.Find(filter).FirstOrDefault();

            if (Magic != null)
            {
                Magics.FindOneAndDelete<Magic>(filter);
                Console.WriteLine($"{Magic.MagicName} has been removed from the list of playable characters");
            }
            else
            {
                Console.WriteLine($"No Magic has been found with the name {wsName}");
            }

            return Magic;
        }

        public static Magic GetMagicById(int wsId)
        {
            IMongoCollection<Magic> Magics = InitializeDatabase();

            var filter = Builders<Magic>.Filter
                .Eq(c => c.MagicId, wsId);

            if (filter == null)
            {
                throw new InvalidOperationException($"No Magic has been found with the id {wsId}");
            }

            Magic Magic = Magics.Find(filter).FirstOrDefault();

            if (Magic != null)
            {
                return Magic;
            }
            else
            {
                Console.WriteLine($"No fighter has been found with the id {wsId}");
                return Magic;
            }


        }

        public static Magic GetMagicByName(string wsName)
        {
            IMongoCollection<Magic> Magics = InitializeDatabase();

            var filter = Builders<Magic>.Filter
                .Eq(c => c.MagicName, wsName);

            if (filter == null)
            {
                throw new InvalidOperationException($"No Magic has been found with the name {wsName}");
            }

            Magic Magic = Magics.Find(filter).FirstOrDefault();

            if (Magic != null)
            {
                Magic.DisplayMagic();
                return Magic;
            }
            else
            {
                Console.WriteLine($"NNo Magic has been found with the name {wsName}");
                return Magic;
            }
        }

        public static void ClearMagicBase()
        {
            IMongoCollection<Magic> versusDatabase = InitializeDatabase();

            var deleteResult = versusDatabase.DeleteMany(_ => true);

            Console.WriteLine("Magic list has been cleared");
            Console.WriteLine($"Number of stored Magics deleted: {deleteResult.DeletedCount}");
            Console.WriteLine($"Number of stored Magics : {versusDatabase.CountDocuments(new BsonDocument())}");
        }

        public static Magic AddMagic(Magic newMagic)
        {
            IMongoCollection<Magic> versusDatabase = InitializeDatabase();

            var wpNamefilter = Builders<Magic>.Filter.Eq("MagicName", newMagic);
            var existingWSName = versusDatabase.Find(wpNamefilter).FirstOrDefault();

            if (existingWSName != null)
            {
                Console.WriteLine($" the Magic '{newMagic.MagicName}' already exist !");
                return null;
            }

            var wpIdfilter = Builders<Magic>.Filter.Eq("MagicId", newMagic.MagicId);
            var existingWeaponId = versusDatabase.Find(wpIdfilter).FirstOrDefault();

            if (existingWeaponId != null)
            {
                Console.WriteLine($" MagicId '{newMagic.MagicId}' already exist !");
                return null;
            }

            versusDatabase.InsertOne(newMagic);

            Console.WriteLine($"{newMagic.MagicName} has been added to Magic list \n Number of Magics in base : {versusDatabase.CountDocuments(wpIdfilter)}");

            newMagic.DisplayMagic();

            return newMagic;
        }

        public static Magic ShortcutAdd_Magic(ObjectId id, int magicId, int jobId, string magicName, string description, string magicType, double mpCost, int castTime, int recastTime, int vBaseValue1, int vBaseValue2, int vBaseValue3, int vBaseValue4, int vBaseValue5, int vBaseValue6, int vBaseValue7, int vBaseValue8, int mCoef1, int mCoef2, int mCoef3, int mCoef4, int mCoef5, int mCoef6, int mCoef7, int mCoef8)
        {
            {
                IMongoCollection<Magic> versusDatabase = InitializeDatabase();

                Magic newMagic = new Magic()
                {
                    _id = id,
                    MagicId = magicId,
                    JobId = jobId,
                    MagicName = magicName,
                    Description = description,
                    MpCost = mpCost,
                    CastTime = castTime,
                    RecastTime = recastTime,
                    VBaseValue1 = vBaseValue1,
                    VBaseValue2 = vBaseValue2,
                    VBaseValue3 = vBaseValue3,
                    VBaseValue4 = vBaseValue4,
                    VBaseValue5 = vBaseValue5,
                    VBaseValue6 = vBaseValue6,
                    VBaseValue7 = vBaseValue7,
                    MCoef1 = mCoef1,
                    MCoef2 = mCoef2,
                    MCoef3 = mCoef3,
                    MCoef4 = mCoef4,
                    MCoef5 = mCoef5,
                    MCoef6 = mCoef6,
                    MCoef7 = mCoef7,
                    MCoef8 = mCoef8
                };

                var wpNamefilter = Builders<Magic>.Filter.Eq("MagicName", newMagic);
                var existingWSName = versusDatabase.Find(wpNamefilter).FirstOrDefault();

                if (existingWSName != null)
                {
                    Console.WriteLine($" the Magic '{newMagic.MagicName}' already exist !");
                    return null;
                }

                var wpIdfilter = Builders<Magic>.Filter.Eq("MagicId", newMagic.MagicId);
                var existingWeaponId = versusDatabase.Find(wpIdfilter).FirstOrDefault();

                if (existingWeaponId != null)
                {
                    Console.WriteLine($" MagicId '{newMagic.MagicId}' already exist !");
                    return null;
                }

                versusDatabase.InsertOne(newMagic);

                Console.WriteLine($"{newMagic.MagicName} has been added to Magic list \n Number of Magics in base : {versusDatabase.CountDocuments(wpIdfilter)}");

                newMagic.DisplayMagic();

                return newMagic;
            }
        }

        public static Magic ModifyMagic(Magic MagicToModify, int MagicId, int jobId, string magicName, string description, string magicType, double mpCost, int castTime, int recastTime, int vBaseValue1, int vBaseValue2, int vBaseValue3, int vBaseValue4, int vBaseValue5, int vBaseValue6, int vBaseValue7, int vBaseValue8, int mCoef1, int mCoef2, int mCoef3, int mCoef4, int mCoef5, int mCoef6, int mCoef7, int mCoef8)
        {
            IMongoCollection<Magic> versusDatabase = InitializeDatabase();

            var wsNamefilter = Builders<Magic>.Filter.Eq("MagicName", MagicToModify);

            var wsNameCount = versusDatabase.CountDocuments(wsNamefilter);

            if (wsNameCount == 0)
            {
                throw new InvalidOperationException($"No Magic with the name {MagicToModify.MagicName} exists in the list!");
            }

            var fighterUpdate = Builders<Magic>.Update
                .Set(w => w.MagicId, MagicId)
                .Set(w => w.JobId, jobId)
                .Set(w => w.MagicName, magicName)
                .Set(w => w.Description, description)
                .Set(w => w.MagicType, magicType)
                .Set(w => w.MpCost, mpCost)
                .Set(w => w.CastTime, castTime)
                .Set(w => w.RecastTime, recastTime)
                .Set(w => w.VBaseValue1, vBaseValue1)
                .Set(w => w.VBaseValue2, vBaseValue2)
                .Set(w => w.VBaseValue1, vBaseValue3)
                .Set(w => w.VBaseValue2, vBaseValue4)
                .Set(w => w.VBaseValue3, vBaseValue5)
                .Set(w => w.VBaseValue4, vBaseValue6)
                .Set(w => w.VBaseValue5, vBaseValue7)
                .Set(w => w.MCoef1, mCoef1)
                .Set(w => w.MCoef2, mCoef2)
                .Set(w => w.MCoef3, mCoef3)
                .Set(w => w.MCoef4, mCoef4)
                .Set(w => w.MCoef5, mCoef5)
                .Set(w => w.MCoef6, mCoef6)
                .Set(w => w.MCoef7, mCoef7)
                .Set(w => w.MCoef8, mCoef8);

            if (wsNameCount > 1)
            {
                throw new InvalidOperationException($"Multiple Magics found with the name {MagicToModify.MagicName}. This should not happen! Please delete the duplicate before any modification on this character");
            }

            versusDatabase.UpdateOne(wsNamefilter, fighterUpdate);

            return MagicToModify;
        }
    }
}