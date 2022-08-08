using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BAL
{
    public static class BLLProducts
    {
        //get all bottles from db-activate product dal function
        public static IEnumerable<Bottle> GetAllBottlesAction()
        {
            return DALProducts.GetAllBottles();
        }
        //get top five bottles from db-activate product dal function
        public static IEnumerable<TopRatedBottle> GetTopFiveBottlesAction()
        {
            return DALProducts.GetTopFiveBottles();
        }
    }
}
