using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.User
{
    internal abstract class BasicUser
    {
        public string UserName { get; set; }
        public bool IsCurrentUser { get; set; }
    }
}
