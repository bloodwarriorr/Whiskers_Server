using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public static class BLLUsers
    {
        public static bool RegisterUser(User user)
        {
            //code security/ roles / algorithm / 
            return DALUsers.SignUpAction(user);
        }

        public static User LoginUser(UserLoginDetails details)
        {
            //code security/ roles / algorithm / 
            return DALUsers.LoginAction(details);
        }
        public static IEnumerable<Order> GetOrdersByUser(int userId)
        {
            //code security/ roles / algorithm / 
            return DALUsers.GetOrdersAction(userId);
        }
        
    }
}
