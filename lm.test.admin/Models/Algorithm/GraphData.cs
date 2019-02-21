using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lm.test.admin.Models.Algorithm
{
    public class GraphData
    {
        public List<G_Serie> series { get; set; }
    }

    public class G_Serie
    {
        public List<G_Series_Link> links { get; set; }

        public List<G_Series_Data> data { get; set; }
    }

    public class G_Series_Link
    {
        public string source { get; set; }

        public string target { get; set; }

        public string value { get; set; }

        public G_Series_Link_Label label { get; set; }
    }

    public class G_Series_Data
    {
        public string name { get; set; }

        public int x { get; set; }

        public int y { get; set; }
    }

    public class G_Series_Link_Label
    {
        public string formatter { get; set; }

        public string color { get; set; }
    }
}
