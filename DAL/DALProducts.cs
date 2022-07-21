using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProducts
    {
        static string strCon =
        ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static SqlConnection con;
        static string sqlString;
        static SqlCommand command;
        static DALProducts()
        {
            con = new SqlConnection(strCon);
        }
        public static IEnumerable<Bottle> GetAllBottles()
        {
            SqlDataReader reader = null;
            List<Bottle> bottles = new List<Bottle>();
            try
            {
                con.Open();
                sqlString = "exec get_bottles_full";
                command = new SqlCommand(sqlString, con);
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Country country = new Country((int)reader["country_code"], (string)reader["country"]);
                    Region region = new Region((int)reader["region_code"], (string)reader["region"], country);
                    Brand brand = new Brand((int)reader["brand_code"], (string)reader["brand"], region);
                    Taste taste = new Taste((int)reader["taste_code"], (byte)reader["sweet"],
                        (byte)reader["floral"], (byte)reader["fruit"],
                        (byte)reader["body"], (byte)reader["richness"],
                        (byte)reader["smoke"], (byte)reader["wine"]);
                    Type type = new Type((int)reader["type_code"], (string)reader["type"]);
                    bottles.Add(new Bottle(brand, (int)reader["barcode"],
                        (string)reader["name"], (string)reader["age"],
                        (double)reader["price"], type, taste, (string)reader["image"],
                        (double)reader["abv"], (string)reader["description"]));
                }
               
                return bottles;

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
        public static IEnumerable<TopRatedBottle> GetTopFiveBottles() {
            SqlDataReader reader = null;
            List<TopRatedBottle> topFiveBottles = new List<TopRatedBottle>();
            try
            {
                con.Open();
                sqlString = "exec top_five_bottles";
                command = new SqlCommand(sqlString, con);
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    TopRatedBottle topRatedBottle = new TopRatedBottle((int)reader["bottle_id"], (int)reader["SUM"]);
                    topFiveBottles.Add(topRatedBottle);
                }

                return topFiveBottles;

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
