using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Country
    {
        public int CountryCode { get; set; }
        public string Name { get; set; }
        public Country()
        {

        }

        public Country(int countryCode, string name)
        {
            CountryCode = countryCode;
            Name = name;
        }
    }
}
