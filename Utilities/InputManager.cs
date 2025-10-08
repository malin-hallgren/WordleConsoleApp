using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.Utilities
{
    internal class InputManager
    {

        public static string GuessInput(int minRange, int maxRange, int tab, int staticRows, int attempts)
        {
            string output = "";

            while (true)
            {
                output = Console.ReadLine();

                if (output.Length >= minRange && output.Length < maxRange)
                {
                    return output;
                }
                else
                {
                    FormatManager.TabToPos(tab, staticRows, attempts);
                    Console.WriteLine("Faulty guess, make a guess no longer than the scrambled word.");
                    FormatManager.TabToPos(tab, staticRows, attempts + 1);
                    Console.WriteLine("Press Enter to guess again");
                    Console.CursorVisible = false;
                    Console.ReadLine();
                    FormatManager.ClearRow(tab, staticRows, attempts, 4);
                    Console.CursorVisible = true;
                }
            }
        }

        public static string CheckUserNameInput()
        {
            while (true)
            {
                string? userInput = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(userInput))
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("User name cannot be blank, please make an input:");
                }
            }
        }
    }
}
