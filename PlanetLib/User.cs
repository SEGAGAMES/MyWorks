using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetLib
{
    public class User
    {
        string login;
        int pin;
        public User(int pin, string login)
        {
            Pin = pin;
            Login = login;
        }

        public string Login { get => login; set => login = value; }
        public int Pin { get => pin; set=> pin = value; }

    }
}
