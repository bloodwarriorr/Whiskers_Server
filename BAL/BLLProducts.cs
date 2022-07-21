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
        public static IEnumerable<Bottle> GetAllBottlesAction()
        {
            return DALProducts.GetAllBottles();
        }
        public static IEnumerable<TopRatedBottle> GetTopFiveBottlesAction()
        {
            return DALProducts.GetTopFiveBottles();
        }
    }
}
