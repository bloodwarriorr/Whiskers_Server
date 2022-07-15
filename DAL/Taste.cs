using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Taste
    {
        public int TasteCode { get; set; }
        public int Sweet { get; set; }
        public int Floral { get; set; }
        public int Fruit { get; set; }
        public int Body { get; set; }
        public int Richness { get; set; }
        public int Smoke { get; set; }
        public int Wine { get; set; }
        public Taste()
        {

        }

        public Taste(int tasteCode, int sweet, int floral, int fruit, int body, int richness, int smoke, int wine)
        {
            TasteCode = tasteCode;
            Sweet = sweet;
            Floral = floral;
            Fruit = fruit;
            Body = body;
            Richness = richness;
            Smoke = smoke;
            Wine = wine;
        }
    }
}
