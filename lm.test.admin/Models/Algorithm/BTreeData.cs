using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lm.test.admin.Models.Algorithm
{
    public class TreeData
    {
        public List<LeafData> data { get; set; }
    }

    public class LeafData
    {
        public string name { get; set; }

        public string value { get; set; }

        public string symbol { get; set; }

        public List<int> symbolSize { get; set; }

        public List<LeafData> children { get; set; }

        public ItemStyle itemStyle { get; set; }
    }

    public class ItemStyle
    {
        public string borderColor { get; set; }

        public string color { get; set; }
    }
}
