using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXI_Versus.Jobs
{
    public interface IJob
    {
        const int JobId = 0;
        string JobName { get; set; }
        string JobDescription { get; set; }
        string[] Passives { get; set; }
        string Ultimate { get; set; }
        double Ultimate_value { get; set; }
        double JHpCoef { get; set; }
        double JMpCoef { get; set; }
        double JStrCoef { get; set; }
        double JIntCoef { get; set; }
        double JDexCoef { get; set; }
        double JAgiCoef { get; set; }
        double JChrCoef { get; set; }
        double JVitCoef { get; set; }
        double JMndCoef { get; set; }
        double WCap { get; set; }
    }
}
