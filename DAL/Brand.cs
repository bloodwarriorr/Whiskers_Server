using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Brand
    {
       public int BrandCode{ get; set; }
       public string BrandName{ get; set; }
       public Region Region{ get; set; }
        public Brand()
        {

        }

        public Brand(int brandCode, string name, Region region)
        {
            BrandCode = brandCode;
            BrandName = name;
            Region = region;
        }
    }
}
