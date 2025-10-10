using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class UserSelectMenu
    {
        /// <summary>
        /// Checks if there is currently a new player option in the list of active users, and adds one if there isn't
        /// </summary>
        /// <param name="users">The list of users to check</param>
        public void NewPlayerOption(List<BasicUser> users)
        {
            if (!users.Any(user => user.IsShellUser == true))
            {
                var newPlayerOption = new Player();
                newPlayerOption.IsShellUser = true;
                users.Add(newPlayerOption);
            }
        }
    }
}
