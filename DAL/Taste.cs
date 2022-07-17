using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Taste
    {
        public int TasteCode { get; set; }
        public byte Sweet { get; set; }
        public byte Floral { get; set; }
        public byte Fruit { get; set; }
        public byte Body { get; set; }
        public byte Richness { get; set; }
        public byte Smoke { get; set; }
        public byte Wine { get; set; }
        public Taste()
        {

        }

        public Taste(int tasteCode, byte sweet, byte floral, byte fruit, byte body, byte richness, byte smoke, byte wine)
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
