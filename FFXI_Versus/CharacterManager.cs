using Azure;
using Azure.Core;
using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using FFXI_Versus.Jobs;
using FFXI_Versus.Races;

namespace FFXI_Versus
{
    public class CharacterManager
    {
        private static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "characters.json");
        public static string ConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        public static void MongoConnectionTest()
        {

            if (ConnectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }

            var settings = MongoClientSettings.FromConnectionString(ConnectionString);

            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                Console.WriteLine($"Pinged your deployment. You successfully connected to MongoDB! Cluster Id is n°{client.Cluster.ClusterId}");

                IMongoCollection<Fighter> versus_database = client.GetDatabase("FFXI_Versus").GetCollection<Fighter>("Character_list");

                var filter = Builders<Fighter>.Filter.Eq("CharName", "Altana");

                var first_entry = versus_database.Find(filter).First();

                Console.WriteLine($"The first entity here is {first_entry.CharName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static IMongoCollection<Fighter> InitializeDatabase()
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
                IMongoCollection<Fighter> versus_database = client.GetDatabase("FFXI_Versus").GetCollection<Fighter>("Character_list");

                return versus_database;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return null;
            }
        }


        public static Fighter DeleteFighterById(int fighterId)
        {
            IMongoCollection<Fighter> fighters = InitializeDatabase();

            var filter = Builders<Fighter>.Filter
                .Eq(c => c.FighterId, fighterId);

            if (filter == null)
            {
                throw new InvalidOperationException($"No fighter has been found with the id {fighterId}");
            }

            Fighter fighter = fighters.Find(filter).FirstOrDefault();

            if (fighter != null)
            {
                fighters.FindOneAndDelete<Fighter>(filter);
                Console.WriteLine($"{fighter.CharName} has been removed from the list of playable characters");
            }
            else
            {
                Console.WriteLine($"No fighter has been found with the id {fighterId}");
            }

            return fighter;
        }

        public static Fighter DeleteFighterByName(string charName)
        {
            IMongoCollection<Fighter> fighters = InitializeDatabase();

            var filter = Builders<Fighter>.Filter
                .Eq(c => c.CharName, charName);

            if (filter == null)
            {
                throw new InvalidOperationException($"No fighter has been found with the id {charName}");
            }

            Fighter fighter = fighters.Find(filter).FirstOrDefault();

            if (fighter != null)
            {
                fighters.FindOneAndDelete<Fighter>(filter);
                Console.WriteLine($"{fighter.CharName} has been removed from the list of playable characters");
            }
            else
            {
                Console.WriteLine($"No fighter has been found with the name {charName}");
            }

            return fighter;
        }

        public static Fighter GetFighterById(int fighterId)
        {
            IMongoCollection<Fighter> fighters = InitializeDatabase();

            var filter = Builders<Fighter>.Filter
                .Eq(c => c.FighterId, fighterId);

            if (filter == null)
            {
                throw new InvalidOperationException($"No fighter has been found with the id {fighterId}");
            }

            Fighter fighter = fighters.Find(filter).FirstOrDefault();

            if (fighter != null)
            {
                return fighter;
            }
            else
            {
                Console.WriteLine($"No fighter has been found with the id {fighterId}");
                return fighter;
            }


        }

        public static Fighter GetFighterByName(string fighterName)
        {
            IMongoCollection<Fighter> fighters = InitializeDatabase();

            var filter = Builders<Fighter>.Filter
                .Eq(c => c.CharName, fighterName);

            if (filter == null)
            {
                throw new InvalidOperationException($"No fighter has been found with the id {fighterName}");
            }

            Fighter fighter = fighters.Find(filter).FirstOrDefault();

            if (fighter != null)
            {
                fighter.DisplayCharacter();
                return fighter;
            }
            else
            {
                Console.WriteLine($"No fighter has been found with the id {fighterName}");
                return fighter;
            }
        }

        public static void ClearFighterBase()
        {
            IMongoCollection<Fighter> versusDatabase = InitializeDatabase();

            var deleteResult = versusDatabase.DeleteMany(_ => true);

            Console.WriteLine("Playable fighter list has been cleared");
            Console.WriteLine($"Number of stored fighters deleted: {deleteResult.DeletedCount}");
            Console.WriteLine($"Number of stored fighters : {versusDatabase.CountDocuments(new BsonDocument())}");
        }

        public static Fighter AddFighter(Fighter newFighter)
        {
            IMongoCollection<Fighter> versusDatabase = InitializeDatabase();

            var charNamefilter = Builders<Fighter>.Filter.Eq("CharName", newFighter);
            var existingCharName = versusDatabase.Find(charNamefilter).FirstOrDefault();

            if (existingCharName != null)
            {
                Console.WriteLine($" Character named '{newFighter.CharName}' already exist !");
                return null;
            }

            var fighterIdFilter = Builders<Fighter>.Filter.Eq("FighterId", newFighter.FighterId);
            var existingFighterId = versusDatabase.Find(fighterIdFilter).FirstOrDefault();

            if (existingFighterId != null)
            {
                Console.WriteLine($" FighterId '{newFighter.FighterId}' already exist !");
                return null;
            }

            versusDatabase.InsertOne(newFighter);

            Console.WriteLine($"{newFighter.CharName} has been added to the playable character list \n Number of playable fighters : {versusDatabase.CountDocuments(fighterIdFilter)}");

            newFighter.DisplayCharacter();

            return newFighter;
        }

        public static Fighter ShortcutAdd_Fighter(ObjectId id, int fighterId, string charname, int raceId, int jobId, string background, string exclamation, int age, string[] relatives, int baseHp, int baseMp, int baseStr, int baseDex, int baseVit, int baseAgi, int baseInt, int baseMnd, int baseChr, int weapBaseDmg, int[] wsList)
        {
            {
                IMongoCollection<Fighter> versusDatabase = InitializeDatabase();

                Fighter newFighter = new Fighter()
                {
                    _id = id,
                    FighterId = fighterId,
                    CharName = charname,
                    RaceId = raceId,
                    JobId = jobId,
                    Background = background,
                    Exclamation = exclamation,
                    Age = age,
                    Relatives = relatives,
                    BaseHp = baseHp,
                    BaseMp = baseMp,
                    BaseStr = baseStr,
                    BaseDex = baseDex,
                    BaseAgi = baseAgi,
                    BaseVit = baseVit,
                    BaseInt = baseInt,
                    BaseMnd = baseMnd,
                    BaseChr = baseChr,
                    WeapBaseDmg = weapBaseDmg,
                    WsList = wsList
                };

                var charNamefilter = Builders<Fighter>.Filter.Eq("CharName", newFighter);

                var existingCharName = versusDatabase.Find(charNamefilter).FirstOrDefault();

                if (existingCharName != null)
                {
                    Console.WriteLine($" Character named '{newFighter.CharName}' already exist !");
                    return null;
                }

                var fighterIdFilter = Builders<Fighter>.Filter.Eq("FighterId", newFighter.FighterId);
                var existingFighterId = versusDatabase.Find(fighterIdFilter).FirstOrDefault();

                if (existingFighterId != null)
                {
                    Console.WriteLine($" FighterId '{newFighter.FighterId}' already exist !");
                    return null;
                }

                versusDatabase.InsertOne(newFighter);

                Console.WriteLine($"{newFighter.CharName} has been added to the playable character list \n Number of playable fighters : {versusDatabase.CountDocuments(fighterIdFilter)}");

                return newFighter;
            }
        }

        public static Fighter DirectAdd_Fighter()
        {
            IMongoCollection<Fighter> versusDatabase = InitializeDatabase();

            Fighter Iroha = new Fighter(
                new ObjectId(),
                1,
                "Iroha",
                Hume.RaceId,
                Ninja.JobId,
                "Your beloved student from the future who comes back to save us all.",
                "Master, together we will purge Evil from this world !",
                27,
                new string[] { "Lion, Arciela" },
                2700,
                1850,
                160,
                210,
                160,
                175,
                150,
                120,
                150,
                256,
                new int[] {1,4,5,6}             
                );

            var charNamefilter = Builders<Fighter>.Filter.Eq("CharName", Iroha);

            var existingCharName = versusDatabase.Find(charNamefilter).FirstOrDefault();

            if (existingCharName != null)
            {
                Console.WriteLine($" Character named '{Iroha.CharName}' already exist !");
                return null;
            }

            var fighterIdFilter = Builders<Fighter>.Filter.Eq("FighterId", Iroha.FighterId);
            var existingFighterId = versusDatabase.Find(fighterIdFilter).FirstOrDefault();

            if (existingFighterId != null)
            {
                Console.WriteLine($" FighterId '{Iroha.FighterId}' already exist !");
                return null;
            }

            versusDatabase.InsertOne(Iroha);

            Console.WriteLine($"{Iroha.CharName} has been added to the playable character list \n Number of playable fighters : {versusDatabase.CountDocuments(fighterIdFilter)}");

            return Iroha;
        }


        public static Fighter ModifyFighter(Fighter fighterToModify, int fighterId, string charname, int raceId, int jobId, string background, string exclamation, int age, string[] relatives, int baseHp, int baseMp, int baseStr, int baseDex, int baseVit, int baseAgi, int baseInt, int baseMnd, int baseChr, int weapBaseDmg)
        {
            IMongoCollection<Fighter> versusDatabase = InitializeDatabase();

            var charNamefilter = Builders<Fighter>.Filter.Eq("CharName", fighterToModify);

            var charNameCount = versusDatabase.CountDocuments(charNamefilter);

            if (charNameCount == 0)
            {
                throw new InvalidOperationException($"No character with the name {fighterToModify.CharName} exists in the list!");
            }

            var fighterUpdate = Builders<Fighter>.Update
                .Set(f => f.FighterId, fighterId)
                .Set(f => f.CharName, charname)
                .Set(f => f.RaceId, raceId)
                .Set(f => f.JobId, jobId)
                .Set(f => f.Background, background)
                .Set(f => f.Exclamation, exclamation)
                .Set(f => f.Age, age)
                .Set(f => f.Relatives, relatives)
                .Set(f => f.BaseHp, baseHp)
                .Set(f => f.BaseMp, baseMp)
                .Set(f => f.BaseStr, baseStr)
                .Set(f => f.BaseDex, baseDex)
                .Set(f => f.BaseVit, baseVit)
                .Set(f => f.BaseAgi, baseAgi)
                .Set(f => f.BaseInt, baseInt)
                .Set(f => f.BaseMnd, baseMnd)
                .Set(f => f.BaseChr, baseChr)
                .Set(f => f.WeapBaseDmg, weapBaseDmg);

            if (charNameCount > 1)
            {
                throw new InvalidOperationException($"Multiple characters found with the name {fighterToModify.CharName}. This should not happen! Please delete the duplicate before any modification on this character");
            }

            versusDatabase.UpdateOne(charNamefilter, fighterUpdate);

            return fighterToModify;
        }        
    }
}   
