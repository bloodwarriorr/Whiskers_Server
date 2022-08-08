using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BC = BCrypt.Net.BCrypt;

namespace BAL
{
    public static class BLLUsers
    {
        //activate register function in user dal side
        public static bool RegisterUser(User user)
        {
            user.Password = BC.HashPassword(user.Password);
            return DALUsers.SignUpAction(user);
        }
        //activate login function in user dal side
        public static User LoginUser(UserLoginDetails details)
        {
           
            User user = DALUsers.LoginAction(details);
            if (user == null || !BC.Verify(details.Password,user.Password))
            {
                return null;
            }
            else
            {
                return user;
            }
        }
        //pull orders from db function in user dal side
        public static IEnumerable<Order> GetOrdersByUser(int userId)
        {
            return DALUsers.GetOrdersAction(userId);
        }
        //make a purchase-update db fields
        public static bool BuyCart(NewOrder order)
        {

            return DALUsers.AddOrderAction(order);
        }
    }
}
