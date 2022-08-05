using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NewOrder
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
       public List<Order_Item>Items { get; set; }
        public NewOrder()
        {

        }

        public NewOrder(int userId, List<Order_Item> items)
        {
            UserId = userId;
            OrderDate = DateTime.Now;
            Items = items;
        }

    }
}
