using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus.Races
{
    public interface IRace
    {
        const int RaceId = 0;
        string RaceName { get; set; }
        string RaceIntro { get; set; }
        double HpCoef { get; set; }
        double MpCoef { get; set; }
        double StrCoef { get; set; }
        double IntCoef { get; set; }
        double DexCoef { get; set; }
        double AgiCoef { get; set; }
        double ChaCoef { get; set; }
        double EndCoef { get; set; }
        double MndCoef { get; set; }
    }
}
