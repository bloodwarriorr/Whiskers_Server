using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALAdmin
    {
        static string strCon =
          ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static SqlConnection con;
        static string sqlString;
        static SqlCommand command;
        static DALAdmin()
        {
            con = new SqlConnection(strCon);
        }

        public static bool AddBottleToDB(Bottle bottle)
        {
            sqlString = @"exec insert_bottle @brand_name,@region_code,@barcode,@price,@type_code,@image,@abv,@taste_code,@description,@age,
            @sweet,@floral,@fruit,@body,@richness,@smoke,@wine,@bottle_name,";
            command = new SqlCommand(sqlString, con);
            try
            {
                con.Open();
                command.Parameters.AddWithValue("@brand_name", bottle.Brand.BrandName);
                command.Parameters.AddWithValue("@region_code", bottle.Brand.Region.RegionCode);
                command.Parameters.AddWithValue("@barcode", bottle.Barcode);
                command.Parameters.AddWithValue("@price", bottle.Price);
                command.Parameters.AddWithValue("@type_code", bottle.Type.TypeCode);
                command.Parameters.AddWithValue("@image", bottle.Image);
                command.Parameters.AddWithValue("@abv", bottle.ABV);
                command.Parameters.AddWithValue("@taste_code", bottle.Taste.TasteCode);
                command.Parameters.AddWithValue("@description", bottle.Description);
                command.Parameters.AddWithValue("@age", bottle.Age);
                command.Parameters.AddWithValue("@sweet", bottle.Taste.Sweet);
                command.Parameters.AddWithValue("@floral", bottle.Taste.Floral);
                command.Parameters.AddWithValue("@fruit", bottle.Taste.Fruit);
                command.Parameters.AddWithValue("@body", bottle.Taste.Body);
                command.Parameters.AddWithValue("@richness", bottle.Taste.Richness);
                command.Parameters.AddWithValue("@smoke", bottle.Taste.Smoke);
                command.Parameters.AddWithValue("@wine", bottle.Taste.Wine);
                command.Parameters.AddWithValue("@bottle_name", bottle.BottleName);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                new Exception("error with Add bottle " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;


        }

        public static bool DeleteBottleFromDB(int barcode)
        {
            sqlString = @"Delete from [dbo].[Bottles] where barcode=@barcode";
            command = new SqlCommand(sqlString, con);
            try
            {
                con.Open();
                command.Parameters.AddWithValue("@barcode", barcode);

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                new Exception("error with Delete bottle " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }

        public static bool DeleteUserFromDB(int userId)
        {
            sqlString = @"Delete from [dbo].[Users] where user_id=@userId";
            command = new SqlCommand(sqlString, con);
            try
            {
                con.Open();
                command.Parameters.AddWithValue("@userId", userId);

                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                new Exception("error with Delete user " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;
        }
        //update whole bottle
        //public static bool UpdateBottleInDB(Bottle bottle, int bracode)
        //{
        //    sqlString = @"Update [dbo].[Bottles] set[barcode]=@barcode,[brand_code]=@brand_code,[name]=@name,[price]=@price
        //    ,[type_code]=@type_code,[image]=@image,[abv]=@abv,[taste_code]=@taste_code,[description]=@description,[age]=@age
        //    where barcode=@selectedBottle";
        //    command = new SqlCommand(sqlString, con);
        //    try
        //    {
        //        con.Open();
        //        command.Parameters.AddWithValue("@barcode", bottle.Barcode);
        //        command.Parameters.AddWithValue("@price", bottle.Price);
        //        command.Parameters.AddWithValue("@brand_code", bottle.Brand.BrandCode);
        //        command.Parameters.AddWithValue("@name", bottle.BottleName + " " + bottle.Brand.BrandName);
        //        command.Parameters.AddWithValue("@type_code", bottle.Type.TypeCode);
        //        command.Parameters.AddWithValue("@image", bottle.Image);
        //        command.Parameters.AddWithValue("@abv", bottle.ABV);
        //        command.Parameters.AddWithValue("@taste_code", bottle.Taste.TasteCode);
        //        command.Parameters.AddWithValue("@description", bottle.Description);
        //        command.Parameters.AddWithValue("@age", bottle.Age);
        //        command.Parameters.AddWithValue("@selectedBottle", bracode);
        //        command.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {

        //        new Exception("error with Add bottle " + ex.Message);
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }
        //    return false;


        //}

        //update bottle price
        public static bool UpdateBottlePriceDB(double price, int barcode)
        {
            sqlString = @"Update [dbo].[Bottles] set[price]=@price
            where barcode=@barcode";
            command = new SqlCommand(sqlString, con);
            try
            {
                con.Open();
                command.Parameters.AddWithValue("@barcode", barcode);
                command.Parameters.AddWithValue("@price", price);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {

                new Exception("error with Add bottle " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return false;


        }





        ///get all users with each of user orders
        public static IEnumerable<UserSummary> GetAllUsers()
        {
            SqlDataReader reader = null;
            List<UserSummary> userSummaries = new List<UserSummary>();
            List<int> userIds = new List<int>();
            try
            {
                con.Open();
                sqlString = "exec get_users_orders_total";
                command = new SqlCommand(sqlString, con);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = (int)reader["user_id"];
                    userIds.Add(id);
                    UserSummary userSummary = new UserSummary(id, (string)reader["email"],
                        (string)reader["first_name"], (string)reader["last_name"],
                        (int)reader["total_qty"], (double)reader["total_price"]);

                    userSummaries.Add(userSummary);

                }
                reader.Close();
                con.Close();
                for (int i = 0; i < userIds.Count; i++)
                {
                    userSummaries[i].UserOrders = (List<Order>)GetOrdersActionPerUser(userIds[i]);
                }

                return userSummaries;

            }
            catch (Exception ex)
            {
                new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
            }

            return null;

        }
        //get orders by user id
        public static IEnumerable<Order> GetOrdersActionPerUser(int userId)
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
                sqlString = "Exec get_order_items_by_user_id @userId ";
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
        //get all orders of all users
        //public static IEnumerable<DetailedOrder> GetAllOrdersByUser()
        //{
        //    SqlDataReader reader = null;
        //    List<DetailedOrder> ordersSummary = new List<DetailedOrder>();
        //    List<int> userIds = new List<int>();
        //    try
        //    {
        //        con.Open();
        //        sqlString = "exec get_orders_by_user";
        //        command = new SqlCommand(sqlString, con);
        //        reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            int userId = (int)reader["user_id"];
        //            userIds.Add(userId);
        //            DetailedOrder detailedOrder = new DetailedOrder(userId, (string)reader["first_name"]
        //                , (int)reader["order_code"], (DateTime)reader["date"], (int)reader["qty"], (double)reader["total"]);

        //            ordersSummary.Add(detailedOrder);



        //        }

        //        reader.Close();
        //        con.Close();
        //        for (int i = 0; i < userIds.Count; i++)
        //        {
        //            ordersSummary[i].UserOrders = (List<Order>)GetOrdersActionPerUser(userIds[i]);
        //        }
        //        return ordersSummary;

        //    }
        //    catch (Exception ex)
        //    {
        //        new InvalidOperationException("error in ExcNQ DAL " + ex.Message);
        //    }
        //    finally
        //    {
        //        reader.Close();
        //        con.Close();
        //    }
        //    return null;
        //}







    }
}
