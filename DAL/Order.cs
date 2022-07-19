using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Order
    {
        public int OrderCode { get; set; }
        public Order_Item Items { get; set; }
        public DateTime DateTime { get; set; }
        public Order()
        {

        }

        public Order(Order_Item items, DateTime dateTime, int orderCode)
        {
            Items = items;
            DateTime = dateTime;
            OrderCode = orderCode;
        }
    }
}
