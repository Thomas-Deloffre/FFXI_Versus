using FFXI_Versus.Mechanics;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FFXI_Versus
{
    public class WeaponskillManager
    {
        public static string ConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        public static IMongoCollection<WeaponSkill> InitializeDatabase()
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
                IMongoCollection<WeaponSkill> versus_database = client.GetDatabase("FFXI_Versus").GetCollection<WeaponSkill>("WeaponSkill_list");

                return versus_database;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }

        public static WeaponSkill DeleteWeaponSkillByName(string wsName)
        {
            IMongoCollection<WeaponSkill> weaponSkills = InitializeDatabase();

            var filter = Builders<WeaponSkill>.Filter
                .Eq(c => c.WeaponSkillName, wsName);

            if (filter == null)
            {
                throw new InvalidOperationException($"No Weaponskill has been found with the name {wsName}");
            }

            WeaponSkill weaponSkill = weaponSkills.Find(filter).FirstOrDefault();

            if (weaponSkill != null)
            {
                weaponSkills.FindOneAndDelete<WeaponSkill>(filter);
                Console.WriteLine($"{weaponSkill.WeaponSkillName} has been removed from the list of playable characters");
            }
            else
            {
                Console.WriteLine($"No Weaponskill has been found with the name {wsName}");
            }

            return weaponSkill;
        }

        public static WeaponSkill GetWeaponSkillById(int wsId)
        {
            IMongoCollection<WeaponSkill> weaponSkills = InitializeDatabase();

            var filter = Builders<WeaponSkill>.Filter
                .Eq(c => c.WeaponSkillId, wsId);

            if (filter == null)
            {
                throw new InvalidOperationException($"No Weaponskill has been found with the id {wsId}");
            }

            WeaponSkill weaponSkill = weaponSkills.Find(filter).FirstOrDefault();

            if (weaponSkill != null)
            {
                return weaponSkill;
            }
            else
            {
                Console.WriteLine($"No fighter has been found with the id {wsId}");
                return weaponSkill;
            }


        }

        public static WeaponSkill GetWeaponSkillByName(string wsName)
        {
            IMongoCollection<WeaponSkill> weaponSkills = InitializeDatabase();

            var filter = Builders<WeaponSkill>.Filter
                .Eq(c => c.WeaponSkillName, wsName);

            if (filter == null)
            {
                throw new InvalidOperationException($"No Weaponskill has been found with the name {wsName}");
            }

            WeaponSkill weaponSkill = weaponSkills.Find(filter).FirstOrDefault();

            if (weaponSkill != null)
            {
                weaponSkill.DisplayWeaponSkill();
                return weaponSkill;
            }
            else
            {
                Console.WriteLine($"NNo Weaponskill has been found with the name {wsName}");
                return weaponSkill;
            }
        }

        public static void ClearWeaponSkillBase()
        {
            IMongoCollection<WeaponSkill> versusDatabase = InitializeDatabase();

            var deleteResult = versusDatabase.DeleteMany(_ => true);

            Console.WriteLine("Weaponskill list has been cleared");
            Console.WriteLine($"Number of stored weaponSkills deleted: {deleteResult.DeletedCount}");
            Console.WriteLine($"Number of stored weaponSkills : {versusDatabase.CountDocuments(new BsonDocument())}");
        }

        public static WeaponSkill AddWeaponSkill(WeaponSkill newWeaponSkill)
        {
            IMongoCollection<WeaponSkill> versusDatabase = InitializeDatabase();

            var wpNamefilter = Builders<WeaponSkill>.Filter.Eq("WeaponSkillName", newWeaponSkill);
            var existingWSName = versusDatabase.Find(wpNamefilter).FirstOrDefault();

            if (existingWSName != null)
            {
                Console.WriteLine($" the weaponskill '{newWeaponSkill.WeaponSkillName}' already exist !");
                return null;
            }

            var wpIdfilter = Builders<WeaponSkill>.Filter.Eq("WeaponSkillId", newWeaponSkill.WeaponSkillId);
            var existingWeaponId = versusDatabase.Find(wpIdfilter).FirstOrDefault();

            if (existingWeaponId != null)
            {
                Console.WriteLine($" WeaponskillId '{newWeaponSkill.WeaponSkillId}' already exist !");
                return null;
            }

            versusDatabase.InsertOne(newWeaponSkill);

            Console.WriteLine($"{newWeaponSkill.WeaponSkillName} has been added to weaponskill list \n Number of weaponskills in base : {versusDatabase.CountDocuments(wpIdfilter)}");

            newWeaponSkill.DisplayWeaponSkill();

            return newWeaponSkill;
        }

        public static WeaponSkill ShortcutAdd_WeaponSkill(ObjectId id, int weapondskillId, int jobId, string weaponskillName, string description, double wcs1, double wcs2, double fTPCoef1, double fTPCoef2, double fTPCoef3)
        {
            {
                IMongoCollection<WeaponSkill> versusDatabase = InitializeDatabase();

                WeaponSkill newWeaponSkill = new WeaponSkill()
                {
                    _id = id,
                    WeaponSkillId = weapondskillId,
                    JobId = jobId,
                    WeaponSkillName = weaponskillName,
                    Description = description,
                    WSC1 = wcs1,
                    WSC2 = wcs2,
                    FTPCoef1 = fTPCoef1,
                    FTPCoef2 = fTPCoef2,
                    FTPCoef3 = fTPCoef3
                };

                var wpNamefilter = Builders<WeaponSkill>.Filter.Eq("WeaponSkillName", newWeaponSkill);
                var existingWSName = versusDatabase.Find(wpNamefilter).FirstOrDefault();

                if (existingWSName != null)
                {
                    Console.WriteLine($" the weaponskill '{newWeaponSkill.WeaponSkillName}' already exist !");
                    return null;
                }

                var wpIdfilter = Builders<WeaponSkill>.Filter.Eq("WeaponSkillId", newWeaponSkill.WeaponSkillId);
                var existingWeaponId = versusDatabase.Find(wpIdfilter).FirstOrDefault();

                if (existingWeaponId != null)
                {
                    Console.WriteLine($" WeaponskillId '{newWeaponSkill.WeaponSkillId}' already exist !");
                    return null;
                }

                versusDatabase.InsertOne(newWeaponSkill);

                Console.WriteLine($"{newWeaponSkill.WeaponSkillName} has been added to weaponskill list \n Number of weaponskills in base : {versusDatabase.CountDocuments(wpIdfilter)}");

                newWeaponSkill.DisplayWeaponSkill();

                return newWeaponSkill;
            }
        }

        public static WeaponSkill ModifyWeaponSkill(WeaponSkill weaponskillToModify, int weaponskillId, int jobId, string weaponskillName, string description, double wcs1, double wcs2, double fTPCoef1, double fTPCoef2, double fTPCoef3)
        {
            IMongoCollection<WeaponSkill> versusDatabase = InitializeDatabase();

            var wsNamefilter = Builders<WeaponSkill>.Filter.Eq("WeaponSkillName", weaponskillToModify);

            var wsNameCount = versusDatabase.CountDocuments(wsNamefilter);

            if (wsNameCount == 0)
            {
                throw new InvalidOperationException($"No weaponskill with the name {weaponskillToModify.WeaponSkillName} exists in the list!");
            }

            var fighterUpdate = Builders<WeaponSkill>.Update
                .Set(w => w.WeaponSkillId, weaponskillId)
                .Set(w => w.JobId, jobId)
                .Set(w => w.WeaponSkillName, weaponskillName)
                .Set(w => w.Description, description)
                .Set(w => w.WSC1, wcs1)
                .Set(w => w.WSC2, wcs2)
                .Set(w => w.FTPCoef1, fTPCoef1)
                .Set(w => w.FTPCoef2, fTPCoef2)
                .Set(w => w.FTPCoef3, fTPCoef3);
                              
            if (wsNameCount > 1)
            {
                throw new InvalidOperationException($"Multiple weaponskills found with the name {weaponskillToModify.WeaponSkillName}. This should not happen! Please delete the duplicate before any modification on this character");
            }

            versusDatabase.UpdateOne(wsNamefilter, fighterUpdate);

            return weaponskillToModify;
        }
    }
}