using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Order> Orders { get; set; }
        public User()
        {

        }

        public User(string email, string password, string firstName, string lastName, List<Order> orders)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Orders = orders;
        }
    }
}
