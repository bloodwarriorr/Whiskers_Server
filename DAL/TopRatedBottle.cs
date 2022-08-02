using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TopRatedBottle
    {
        public int Barcode { get; set; }
        public int Amount { get; set; }
        public TopRatedBottle() { }
        public TopRatedBottle(int barcode, int amount)
        {
            Barcode = barcode;
            Amount = amount;
        }
    }
}
