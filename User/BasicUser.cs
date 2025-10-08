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
        
        public bool IsAdmin {  get; set; }

        public bool IsShellUser { get; set; }
        public override string ToString()
        {
            return $"User : {UserName}\nCurrent User : {IsCurrentUser}\nAdmin : {IsAdmin}";
        }

        public static void PrintUserList(List<BasicUser> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }
    }
}
