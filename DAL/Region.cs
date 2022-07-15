using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Region
    {
        public int RegionCode { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public Region()
        {

        }

        public Region(int regionCode, string name, Country country)
        {
            RegionCode = regionCode;
            Name = name;
            Country = country;
        }
    }
}
