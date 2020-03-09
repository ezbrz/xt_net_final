using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.Mercato.Handlers
{
    public class Filter
    {
        public string SearchPattern { get; set; }
        public bool IsDescending { get; set; }
        public string OrderProperty { get; set; }
        public int Category { get; set; }
    }
}
