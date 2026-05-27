using System;
using System.Collections.Generic;
using System.Text;

namespace bolboring_finder_SKF.Models
{
    public class Bearing
    {
        public string Serial { get; set; }

        public double InnerDiameter { get; set; }

        public double OuterDiameter { get; set; }

        public bool Waterproof { get; set; }
    }
}
