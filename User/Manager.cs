using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Utilities;
using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace WordleConsoleApp.User
{
    internal class Manager : BasicUser
    {
        public string HashPassword { get; set; } = string.Empty;

        [JsonIgnore]
        public PasswordHasher<Manager> Hasher { get; private set; } = new();

        public Manager(string password)
        {
            UserName = "Admin";
            IsAdmin = true;
            HashPassword = Hasher.HashPassword(this, password);
        }

        public Manager()
        {
            UserName = "Admin";
            IsAdmin = true;
        }
        /// <summary>
        /// Checks whether password entered by User is matching stored password
        /// </summary>
        /// <param name="manager">The manager object selected by the user</param>
        /// <returns>A bool, true for matching, false for not matching</returns>
        public bool CheckPassword(Manager manager)
        {
            Console.WriteLine("Please input your password:");
            string? inputtedPassword;
            int loginAttempt = 0;
            var loginStatus = new PasswordVerificationResult();

            do
            {
                inputtedPassword = InputManager.CheckStringInput();
                loginAttempt++;

                loginStatus = Hasher.VerifyHashedPassword(this, HashPassword, inputtedPassword);

                if (loginStatus == PasswordVerificationResult.Failed)//(inputtedPassword != DefaultPassword)
                {
                    Console.WriteLine($"Wrong password, you have {3 - loginAttempt} attempts left");
                }

            } while (loginAttempt < 3 && loginStatus == PasswordVerificationResult.Failed);

            

            return loginStatus == PasswordVerificationResult.Success;
        }
    }
}
