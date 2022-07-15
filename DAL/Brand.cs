using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Brand
    {
       public int BrandCode{ get; set; }
       public string Name{ get; set; }
       public Region Region{ get; set; }
        public Brand()
        {

        }

        public Brand(int brandCode, string name, Region region)
        {
            BrandCode = brandCode;
            Name = name;
            Region = region;
        }
    }
}
