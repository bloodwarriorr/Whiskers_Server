using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public static class DALUsers
    {
       static string strCon =
           ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        static SqlConnection con;
        static string sqlString;
        static SqlCommand command;
        static DALUsers()
        {
            con = new SqlConnection(strCon);
        }

        public static bool SignUpAction(User user)
        {
            sqlString = @"insert [dbo].[Users]([email],[first_name],[Last_Name],[password]) 
            values (@email,@firstName,@lastName,@password)";
            command = new SqlCommand(sqlString, con);
            try
            {
                con.Open();
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                new Exception("error with register " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;


        }

        public static User LoginAction(UserLoginDetails details)
        {

            SqlDataReader reader = null;
            try
            {
                con.Open();
                sqlString = $"Select TOP 1 * from Users where email = @email and password = @password";
                command = new SqlCommand(sqlString, con);
                command.Parameters.AddWithValue("@email", details.Email);
                command.Parameters.AddWithValue("@password", details.Password);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new User()
                    {
                        Email = (string)reader["email"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"],
                        Password = (string)reader["password"]
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {
                reader.Close();
                con.Close();
            }
            return null;


        }

        public static IEnumerable<Order> GetOrdersAction(int userId)
        {

            SqlDataReader reader = null;
            List<Order> orders = new List<Order>();
            List<Bottle> bottles = new List<Bottle>();
            List<Order_Item> itemsInOrder = new List<Order_Item>();
            List<int> qty = new List<int>();
            int id = 0;
            DateTime date = new DateTime();

            try
            {
                con.Open();
                sqlString = "exec init_orders @userId";
                command = new SqlCommand(sqlString, con);
                command.Parameters.AddWithValue("@userId", userId);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    id = (int)reader["order_code"];
                }
                reader.Close();
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    date = (DateTime)reader["date"];
                    if (id != (int)reader["order_code"])
                    {

                        orders.Add(new Order(new Order_Item(bottles, qty), (DateTime)reader["date"], id));
                        bottles = new List<Bottle>();
                        qty = new List<int>();
                        id = (int)reader["order_code"];
                    }


                    Country country = new Country((int)reader["country_code"], (string)reader["country_name"]);
                    Region region = new Region((int)reader["region_code"], (string)reader["region_name"], country);
                    Brand brand = new Brand((int)reader["brand_code"], (string)reader["brand_name"], region);
                    Taste taste = new Taste((int)reader["taste_code"], (byte)reader["sweet"],
                        (byte)reader["floral"], (byte)reader["fruit"],
                        (byte)reader["body"], (byte)reader["richness"],
                        (byte)reader["smoke"], (byte)reader["wine"]);
                    Type type = new Type((int)reader["type_code"], (string)reader["type"]);
                    bottles.Add(new Bottle(brand, (int)reader["barcode"],
                        (string)reader["name"], (string)reader["age"],
                        (double)reader["price"], type, taste, (string)reader["image"],
                        (double)reader["abv"], (string)reader["description"]));
                    qty.Add((int)reader["qty"]);





                }
                orders.Add(new Order(new Order_Item(bottles, qty), date, id));
                return orders;

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }
            finally
            {
                reader.Close();
                con.Close();
            }
            return null;
        }

        //public static object AddOrderAction(int userId) { 
        //    DateTime orderDate=DateTime.Now;
        //    sqlString = @"exec create_order @date,@userId";
        //    command = new SqlCommand(sqlString, con);
        //    try
        //    {
        //        con.Open();
        //        command.Parameters.AddWithValue("@userId", userId);
        //        command.Parameters.AddWithValue("@date", orderDate);
        //        var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
        //        returnParameter.Direction = ParameterDirection.ReturnValue;

        //        command.ExecuteNonQuery();
        //        var result = returnParameter.Value;

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {

        //        new Exception("error with register " + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return null;

        //}
        private static int GetOrderCode(DateTime orderDate, int userId)
        {
            SqlDataReader reader = null;
            sqlString = @"SELECT TOP (1) PERCENT order_code FROM dbo.Orders WHERE(user_id = @userId) ORDER BY order_code DESC";
            command = new SqlCommand(sqlString, con);
            try
            {

                command.Parameters.AddWithValue("@userId", userId);
                //command.Parameters.AddWithValue("@date", orderDate);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return (int)reader["order_code"];
                }
            }
            catch (Exception ex)
            {

                new Exception("error with add order " + ex.Message);
            }
            finally
            {
                reader.Close();
            }
            return -1;
        }
        private static void InsertIntoOrderItems(Dictionary<string, int> items, int order_code)
        {
            int index1 = 0;
            List<string> barcodes = items.Keys.ToList();
            sqlString = @"insert [dbo].[order_items]([order_code],[barcode],[qty]) 
            values (@order_code,@barcode,@qty)";
            command = new SqlCommand(sqlString, con);
            command.Parameters.AddWithValue("@barcode", Convert.ToInt32(barcodes[index1]));
            command.Parameters.AddWithValue("@qty", items[barcodes[index1]]);
            try
            {

                command.Parameters.AddWithValue("@order_code", order_code);
                while (index1 < barcodes.Count())
                {


                    command.ExecuteNonQuery();
                    index1++;
                    if (index1 == barcodes.Count())
                    {
                        break;
                    }
                    command.Parameters["@barcode"].Value = Convert.ToInt32(barcodes[index1]);
                    command.Parameters["@qty"].Value = items[barcodes[index1]];
                }


            }
            catch (Exception ex)
            {

                new Exception("error with add order " + ex.Message);
            }


        }
        public static bool AddOrderAction(NewOrder order)
        {

            DateTime orderDate = DateTime.Now;
            sqlString = @"exec create_order @date,@userId";
            command = new SqlCommand(sqlString, con);
            try
            {
                con.Open();
                command.Parameters.AddWithValue("@userId", order.UserId);
                command.Parameters.AddWithValue("@date", orderDate);
                command.ExecuteNonQuery();
                int order_code = GetOrderCode(orderDate, order.UserId);
                InsertIntoOrderItems(order.Items, order_code);

                return true;
            }
            catch (Exception ex)
            {

                new Exception("error with add order " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;


        }




    }
}
