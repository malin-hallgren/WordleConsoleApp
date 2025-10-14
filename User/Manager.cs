using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Utilities;

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

        public bool CheckPassword(Manager manager)
        {
            Console.WriteLine("Please input your password:");
            string? inputtedPassword;
            int loginAttempt = 0;

            do
            {
                inputtedPassword = InputManager.CheckStringInput();
                loginAttempt++;

                if(inputtedPassword != Password)
                {
                    Console.WriteLine($"Wrong password, you have {3 - loginAttempt} attempts left");
                }
                
            } while (loginAttempt < 3 && inputtedPassword != Password);

            return inputtedPassword == Password;


        }
    }
}
