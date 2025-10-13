using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.User
{
    internal abstract class BasicUser : IComparable<BasicUser>
    {
        public string UserName { get; set; }
        public bool IsCurrentUser { get; set; }
        
        public bool IsAdmin {  get; set; }

        public bool IsShellUser { get; set; }
        public override string ToString()
        {
            return $"{UserName}";
        }

        /// <summary>
        /// Prints a list of all the users in a list of BasicUsers
        /// </summary>
        /// <param name="users">The list of which contents will be printed</param>
        public static void PrintUserList(List<BasicUser> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine(user);
            }
        }

        public int CompareTo(BasicUser? user)
        {
            
            var comparison = user as BasicUser;


            return comparison.IsAdmin.CompareTo(this.IsAdmin);

        }
    }
}
