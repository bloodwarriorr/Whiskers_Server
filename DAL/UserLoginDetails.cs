using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserLoginDetails
    {
     

        public string Email { get; set; }

        public string Password { get; set; }
        public UserLoginDetails(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
