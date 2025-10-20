using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class UserSelect
    {
        
        /// <summary>
        /// Checks if there is currently a new player option in the list of active users, and adds one if there isn't
        /// </summary>
        /// <param name="users">The list of users to check</param>
        private static void NewPlayerOption(List<BasicUser> users)
        {
            if (!users.Any(user => user.IsShellUser == true))
            {
                var newPlayerOption = new Player();
                newPlayerOption.IsShellUser = true;
                users.Add(newPlayerOption);
            }
        }

        /// <summary>
        /// Checks if there is an admin in the current list, and adds on if not
        /// </summary>
        /// <param name="users">The list to check</param>
        private static void NewManagerOption(List<BasicUser> users)
        {
            if (!users.Any(user => user.IsAdmin == true))
            {
                var newManager = new Manager();
                users.Add(newManager);
            }
        }


        /// <summary>
        /// Initializes the list of BasicUser for the UserSelectMenu and sorts it
        /// </summary>
        /// <param name="users">the list to setup and sort</param>
        public static void SetUpUserList(List<BasicUser> users)
        {
            NewPlayerOption(users);
            NewManagerOption(users);
            users.Sort();
        }

        
    }
}
