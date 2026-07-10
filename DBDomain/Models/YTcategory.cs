using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YTA.DBDomain.Models
{
    public class YTcategory
    {
        public string ID { get; set; }
        public string Name { get; set; }

        public override string ToString() 
        { 
            return Name; 
        }
    }
}
