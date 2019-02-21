using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lm.test.admin.Models.Algorithm
{
    public class Point
    {
        public string Name { get; set; }

        public Coord Coord { get; set; }
    }

    public class Coord
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}
