using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.User
{
    internal class Manager : BasicUser
    {
        public string Password { get; private set; } = "admin_000";

        public Manager()
        {
            UserName = "Admin";
            IsAdmin = true;
        }
    }
}
