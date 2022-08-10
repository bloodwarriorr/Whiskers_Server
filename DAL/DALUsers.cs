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
        //sign up function
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
        //login function
        public static User LoginAction(UserLoginDetails details)
        {

            SqlDataReader reader = null;
            User user;
            try
            {
                con.Open();
                sqlString = $"Select TOP 1 * from Users where email = @email";
                command = new SqlCommand(sqlString, con);
                command.Parameters.AddWithValue("@email", details.Email);
                //command.Parameters.AddWithValue("@password", details.Password);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new User
                    {
                        Email = (string)reader["email"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"],
                        Password = (string)reader["password"],
                        Id = (int)reader["user_id"],


                    };
                    reader.Close();
                    con.Close();
                    user.Orders = ((List<Order>)GetOrdersAction(user.Id));
                    return user;
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
        //get orders per logged in user
        public static IEnumerable<Order> GetOrdersAction(int userId)
        {

            SqlDataReader reader = null;
            List<Order> orders = new List<Order>();
            List<Order_Item> itemsInOrder = new List<Order_Item>();
            Order_Item item = new Order_Item();
            int id = 0;
            DateTime date = new DateTime();

            try
            {
                con.Open();
                sqlString = "exec get_order_items_by_user_id @userId";
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

                    if (id != (int)reader["order_code"])
                    {
                        orders.Add(new Order(itemsInOrder, date, id));
                        itemsInOrder = new List<Order_Item>();
                        id = (int)reader["order_code"];

                    }

                    date = (DateTime)reader["date"];
                    item = new Order_Item((int)reader["order_code"],
                    (int)reader["barcode"],
                    (int)reader["qty"],
                    (int)reader["brand_code"],
                    (string)reader["brand_name"],
                    (string)reader["bottle_name"],
                    (double)reader["price"],
                    (string)reader["image"]);
                    itemsInOrder.Add(item);



                }
                orders.Add(new Order(itemsInOrder, date, id));
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
        //make a purchase for specific user
        //public static object AddOrderAction(int userId)
        //{
        //    DateTime orderDate = DateTime.Now;
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


        //get a order code in order to assign items to specific order
        private static int GetOrderCode(int userId)
        {
            SqlDataReader reader = null;
            sqlString = @"SELECT TOP (1) PERCENT order_code FROM dbo.Orders WHERE(user_id = @userId) ORDER BY order_code DESC";
            command = new SqlCommand(sqlString, con);
            try
            {

                command.Parameters.AddWithValue("@userId", userId);
               
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
        //help method for assigning items into a specific order of a user-fill in order data
        private static void InsertIntoOrderItems(List<Order_Item> items, int order_code)
        {
            
            sqlString = @"insert [dbo].[order_items]([barcode],[order_code],[qty],[brand_code],[brand_name],[bottle_name],[price],[image])
            values(@barcode,@order_code,@qty,@brandCode,@brandName,@bottleName,@price,@image)";
         
            command = new SqlCommand(sqlString, con);
            command.Parameters.AddWithValue("@barcode", items[0].Barcode);
            command.Parameters.AddWithValue("@order_code", order_code);
            command.Parameters.AddWithValue("@qty", items[0].Qty);
            command.Parameters.AddWithValue("@brandCode", items[0].BrandCode);
            command.Parameters.AddWithValue("@brandName", items[0].BrandName);
            command.Parameters.AddWithValue("@bottleName", items[0].Bottle_Name);
            command.Parameters.AddWithValue("@price", items[0].Price);
            command.Parameters.AddWithValue("@image", items[0].Image);
           
            try
            {
                command.ExecuteNonQuery();
                for (int i = 1; i < items.Count; i++)
                {
                    command.Parameters["@barcode"].Value= items[i].Barcode;
                    command.Parameters["@order_code"].Value = order_code;
                    command.Parameters["@qty"].Value = items[i].Qty;
                    command.Parameters["@brandCode"].Value = items[i].BrandCode;
                    command.Parameters["@brandName"].Value = items[i].BrandName;
                    command.Parameters["@bottleName"].Value = items[i].Bottle_Name;
                    command.Parameters["@price"].Value = items[i].Price;
                    command.Parameters["@image"].Value = items[i].Image;
                    command.ExecuteNonQuery();
                }
              
        

            }
            catch (Exception ex)
            {

                new Exception("error with add order " + ex.Message);
            }


        }
        //add order-make a purchase of a user
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
                int order_code = GetOrderCode(order.UserId);
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
