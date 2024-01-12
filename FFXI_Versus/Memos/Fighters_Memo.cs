using FFXI_Versus.Jobs;
using FFXI_Versus.Races;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus.Memos
{
    internal class Fighters_Memo
    {
        //**__Below is a compendium of all Weaponskills instanciations__**

        Fighter Iroha = new Fighter(
                new ObjectId(),
                1,
                "Iroha",
                Hume.RaceId,
                Samurai.JobId,
                "Your beloved student from the future who comes back to save us all.",
                "Master, together we will purge Evil from this world !",
                27,
                new string[] { "Hero, Lion, Prishe, Arciela, Lilisette, Cait Sith" },
                26000,
                1850,
                210,
                205,
                180,
                220,
                195,
                185,
                170,
                282,
                new int[] { 1, 4, 5, 6 }
                );

        Fighter Shantotto = new Fighter(
                new ObjectId(),
                2,
                "Arciela",
                Hume.RaceId,
                RedMage.JobId,
                "A black mage from the Federation of Windurst and a hero of the Crystal War.",
                "O-hohohohoho ! Let us see what you're good at !",
                50,
                new string[] { "Ajido-Marujido", "Koru-Moru", "oran-Oran" },
                22500,
                2380,
                120,
                185,
                160,
                170,
                245,
                190,
                175,
                240,
                new int[] { 1, 4, 5, 6 }
                );

        Fighter Arciela = new Fighter(
                new ObjectId(),
                3,
                "Arciela",
                Hume.RaceId,
                RedMage.JobId,
                "Arciela V Adoulin. The princess of the Adoulin kingdom and direct blood descendant of the Founder King August ",
                "Let me help you for a while, but you still owe me a cake !",
                28,
                new string[] { "Morimar", "Ygnas", "Sajj'aka" },
                24500,
                2120,
                140,
                190,
                175,
                180,
                215,
                205,
                200,
                257,
                new int[] { 1, 4, 5, 6 }
                );


        Fighter Lion = new Fighter(
                new ObjectId(),
                4,
                "Lion",
                Hume.RaceId,
                RedMage.JobId,
                "Arciela V Adoulin. The princess of the Adoulin kingdom and direct blood descendant of the Founder King August ",
                "Let me help you for a while, but you still owe me a cake !",
                28,
                new string[] { "Morimar", "Ygnas", "Sajj'aka" },
                24500,
                2120,
                140,
                190,
                175,
                180,
                215,
                205,
                200,
                257,
                new int[] { 1, 4, 5, 6 }
                );
    }
}
