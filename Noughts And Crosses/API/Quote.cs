using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noughts_And_Crosses.API
{
    class Quote
    {
        public string quote { get; set; }
        public string length { get; set; }
        public string author { get; set; }
        public List<string> tags { get; set; }
        public string category { get; set; }
        public string id { get; set; }
    }
}
