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
        //activate register function in user dal side
        public static bool RegisterUser(User user)
        { 
            return DALUsers.SignUpAction(user);
        }
        //activate login function in user dal side
        public static User LoginUser(UserLoginDetails details)
        {
            return DALUsers.LoginAction(details);
        }
        //pull orders from db function in user dal side
        public static IEnumerable<Order> GetOrdersByUser(int userId)
        {
            return DALUsers.GetOrdersAction(userId);
        }
        //make a purchase-update db fields
        public static bool BuyCart(NewOrder order) { 

            return DALUsers.AddOrderAction(order);
        }
    }
}
