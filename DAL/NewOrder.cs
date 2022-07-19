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
        public Dictionary<string, int> Items { get; set; }
        public NewOrder()
        {

        }

        public NewOrder(int userId, Dictionary<string, int> items)
        {
            UserId = userId;
            OrderDate = DateTime.Now;
            Items = items;
        }

    }
}
