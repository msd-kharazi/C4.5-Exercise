using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4._5_Exercise.CSharp
{
    public class AllDataModel
    {
        public InfoModel MetaData { get; set; }
        public double InformationGain { get; set; }
        public double SplitInfo { get; set; }
        public double GainRatio { get; set; }
        public List<string> Data { get; set; }

    } 
}
