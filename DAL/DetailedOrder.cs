using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DetailedOrder
    {
       public int UserId { get; set; }
        public string FirstName { get; set; }
        public int OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int Qty { get; set; }
        public double Total { get; set; }
        public List<Order> UserOrders { get; set; }
        public DetailedOrder() { }
        public DetailedOrder(int userId, string firstName, int orderCode, DateTime orderDate, int qty, double total)
        {
            UserId = userId;
            FirstName = firstName;
            OrderCode = orderCode;
            OrderDate = orderDate;
            Qty = qty;
            Total = total;
            UserOrders = new List<Order>();
        }
    }
}
