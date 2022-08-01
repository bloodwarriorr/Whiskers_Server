using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Region
    {
        public int RegionCode { get; set; }
        public string RegionName { get; set; }
        public Country Country { get; set; }
        public Region()
        {

        }

        public Region(int regionCode, string name, Country country)
        {
            RegionCode = regionCode;
            RegionName = name;
            Country = country;
        }
    }
}
