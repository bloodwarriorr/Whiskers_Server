using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Order_Item
    {
        public int OrderCode { get; set; }
        public int Barcode { get; set; }
        public int Qty { get; set; }
        public int BrandCode { get; set; }
        public string BrandName { get; set; }
        public string Bottle_Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public Order_Item()
        {

        }

        public Order_Item(int orderCode, int barcode, int qty, int brandCode, string brandName, string bottle_Name, double price, string image)
        {
            OrderCode = orderCode;
            Barcode = barcode;
            Qty = qty;
            BrandCode = brandCode;
            BrandName = brandName;
            Bottle_Name = bottle_Name;
            Price = price;
            Image = image;
        }
    }
}
