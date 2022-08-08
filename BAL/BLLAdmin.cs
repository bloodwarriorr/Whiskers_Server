using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public static class BLLAdmin
    {
        //add bottle to bottles table-activate admin dal function
        public static bool AddBottleAction(Bottle bottle)
        {
            return DALAdmin.AddBottleToDB(bottle);
        }
        //delete bottle from bottles table-activate admin dal function-optional
        public static bool DeleteBottleAction(int barcode)
        {
            return DALAdmin.DeleteBottleFromDB(barcode);
        }
        //delete user from users table-activate admin dal function
        public static bool DeleteUserAction(int userId)
        {
            return DALAdmin.DeleteUserFromDB(userId);
        }
        //update bottle price inside bottles table-activate admin dal function
        public static bool UpdateBottleAction(double price, int bracode)
        {
            return DALAdmin.UpdateBottlePriceDB(price, bracode);
        }
        //get all users+users data(orders,etc)-activate admin dal function
        public static IEnumerable<UserSummary> GetAllUsersAction()
        {
            return DALAdmin.GetAllUsers();
        }

    }
}
