using FFXI_Versus.Mechanics;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus.Memos
{
    public class WeaponSkills_Memo
    {
        //**__Below is a compendium of all Weaponskills instanciations__**

        WeaponSkill Amatsu_Hanadoki = new WeaponSkill(
            new ObjectId(),
            1,
            4,
            "Amatsu : Hanadoki",
            "Deals Light elemental damage that varies with TP.",
            "Str",
            "Mnd",
            0.50,
            0.30,
            0.5,
            1.5,
            2.5
        );

        WeaponSkill Empirical_Research = new WeaponSkill(
            new ObjectId(),
            2,
            4,
            "Amatsu : Hanadoki",
            "Deals Light elemental damage that varies with TP.",
            "Int",
            "Chr",
            0.60,
            0.15,
            0.75,
            1.7,
            2.2
        );


    }
}
