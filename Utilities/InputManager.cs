using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.Utilities
{
    internal class InputManager
    {
        /// <summary>
        /// Takes input from the Console and checks that it is valid
        /// </summary>
        /// <param name="minRange">the shortest the input may be</param>
        /// <param name="maxRange">the longest the input may be (exclusive!)</param>
        /// <param name="tab">amount of tabs to start printing message</param>
        /// <param name="staticRows">amount of static rows down to start printing message</param>
        /// <param name="attempts">amount of variable rows down to start printing message</param>
        /// <returns>a valid string input</returns>
        public static string GuessInput(int minRange, int maxRange, int attempts)
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
                    FormatManager.TabToPos(attempts);
                    Console.WriteLine("Faulty guess, make a guess no longer than the scrambled word.");
                    FormatManager.TabToPos(attempts + 1);
                    Console.WriteLine("Press Enter to guess again");
                    Console.CursorVisible = false;
                    Console.ReadLine();
                    FormatManager.ClearRow(attempts, 4);
                    Console.CursorVisible = true;
                }
            }
        }
        /// <summary>
        /// Checks that string input is not null or whitespace
        /// </summary>
        /// <returns></returns>
        public static string CheckStringInput()
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
                    Console.WriteLine("Input cannot be blank, please make an input:");
                }
            }
        }
    }
}
