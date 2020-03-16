using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Models
{
    public class LoginData
    {
        public LoginData()
        {     
        }

        public LoginData(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
