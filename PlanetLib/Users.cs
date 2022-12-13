using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetLib
{
    [Serializable]
    public class Users
    {
        public Users() { }
        public Users(List<User> users)
        {
            foreach (var user in users)
                this.users.Add(user);
        }  
        public List<User> users = new List<User>();
    }
}
