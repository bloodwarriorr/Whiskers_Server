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
        public static bool AddBottleAction(Bottle bottle)
        {
            return DALAdmin.AddBottleToDB(bottle);
        }

        public static bool DeleteBottleAction(int barcode)
        {
            return DALAdmin.DeleteBottleFromDB(barcode);
        }

        public static bool DeleteUserAction(int userId)
        {
            return DALAdmin.DeleteUserFromDB(userId);
        }

        public static bool UpdateBottleAction(Bottle bottle, int bracode)
        {
            return DALAdmin.UpdateBottleInDB(bottle, bracode);
        }
        public static IEnumerable<UserSummary> GetAllUsersAction()
        {
            return DALAdmin.GetAllUsers();
        }

        public static IEnumerable<DetailedOrder> GetAllOrdersPerUserAction()
        {
            return DALAdmin.GetAllOrdersByUser();
        }
    }
}
