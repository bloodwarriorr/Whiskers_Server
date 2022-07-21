using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserSummary
    {
        

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int UserTotalItemsPurchased { get; set; }
        public double UserTotalSpent { get; set; }
        public List<Order> UserOrders { get; set; }

        public UserSummary(int userId, string userEmail, string userFirstName, string userLastName, int userTotalItemsPurchased, double userTotalSpent)
        {
            UserId = userId;
            UserEmail = userEmail;
            UserFirstName = userFirstName;
            UserLastName = userLastName;
            UserTotalItemsPurchased = userTotalItemsPurchased;
            UserTotalSpent = userTotalSpent;
            UserOrders= new List<Order>();
        }
    }
}
