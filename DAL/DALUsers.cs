using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DALUsers
    {
        static string strCon = @"Data Source=sql.bsite.net\MSSQL2016;Initial Catalog=bloodwarrior3_;Persist Security Info=True;User ID=bloodwarrior3_;Password=Aa1234$";
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

        public static User LoginAction(UserLoginDetails details) {

            SqlDataReader reader = null;
            try
            {
                con.Open();
                sqlString = $"Select TOP 1 * from Users where email = @email and password = @password";
                command = new SqlCommand(sqlString, con);
                command.Parameters.AddWithValue("@email", details.Email);
                command.Parameters.AddWithValue("@password", details.Password);
                reader=command.ExecuteReader();
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

        public static IEnumerable<Order> GetOrdersAction(int userId) { 
            
            SqlDataReader reader = null;
            List<Order> orders = new List<Order>();
            List<Bottle> bottles=new List<Bottle>();
            List<Order_Item> itemsInOrder=new List<Order_Item>();
            List<int>qty=new List<int>(); 
         
            try
            {
                con.Open();
                sqlString = "exec init_orders @userId";
                command = new SqlCommand(sqlString, con);
                command.Parameters.AddWithValue("@userId", userId);
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                        
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
                        orders.Add(new Order(new Order_Item(bottles, qty), (DateTime)reader["date"]));
                        
                  
                }
                
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



        
    }
}
