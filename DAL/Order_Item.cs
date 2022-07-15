using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    internal class Order_Item
    {
       public List<Bottle>Bottles { get; set; }
       public List<int> Qty { get; set; }
        public Order_Item()
        {

        }

        public Order_Item(List<Bottle> bottles, List<int> qty)
        {
            Bottles = bottles;
            Qty = qty;
        }
    }
}
