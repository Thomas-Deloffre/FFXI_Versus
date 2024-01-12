using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus
{
    public interface IFighter
    {

        int Hp { get; }

        int Mp { get; }

        int Str { get; }

        int Dex { get; }

        int Vit { get; }

        int Agi { get; }

        int Int { get; }

        int Mnd { get; }

        int Chr { get; }

        void DisplayStats();
       
        void DisplayCharacter();

        void JoinFight();
    }

}
