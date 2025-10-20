using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class MenuController
    {
        public static List<string> ConfirmationMenu = new List<string> { "Are you sure you?", "Yes", "No" };

        protected static Game CurrentGame;
        protected static Word CurrentWord;

        public MenuController( Game game, Word word)
        {
            CurrentGame = game;
            CurrentWord = word;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Generic type, most likely to be strings or objects</typeparam>
        /// <param name="options">List of the aforementioned type to be presented as menu options</param>
        /// <param name="title">Title of the menu to be presented above the choices</param>
        /// <returns>returns an int to be used to pick out which index of the list was chosen</returns>
        public static int Choice<T>(List<T> options, string title = "WordGuess")
        {
            Console.CursorVisible = false;
            int selected = 0;
            int prevSelected = selected;
            Console.Clear();

            Draw(options, selected, prevSelected, title);

            while (true)
            {
                
                var pressedKey = Console.ReadKey(true).Key; //Using true to avoid printing the key to console

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        prevSelected = selected;
                        selected = (selected - 1 + options.Count) % options.Count;
                        ClearLine(selected, prevSelected);
                        Draw(options, selected, prevSelected, title);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        prevSelected = selected;
                        selected = (selected + 1) % options.Count;
                        ClearLine(selected, prevSelected);
                        Draw(options, selected, prevSelected, title);
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        Console.Clear();
                        return selected;
                }
            }
        }

        /// <summary>
        /// Creates a highlight for selecting a menu option
        /// </summary>
        /// <typeparam name="T">the generic type of data in the list</typeparam>
        /// <param name="options">the list that contains our nenu options</param>
        /// <param name="selected">the currently selected element</param>
        /// <param name="prevSelected">the previously selected element</param>
        /// <param name="title">the title of the menu</param>
        private static void Draw<T>(List<T> options, int selected, int prevSelected, string title)
        {
            Console.WriteLine($"{title}\n");


            for (int i = 0; i < options.Count; i++)
            {
                //Checks if we should highlight the option
                if (i == selected)
                {
                    //Saves original colours
                    ConsoleColor originalBackground = Console.BackgroundColor;
                    ConsoleColor originalForeground = Console.ForegroundColor;

                    //Inverts colours for highlight
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    Console.WriteLine(options[i]);

                    //resets colour to prevent permanent higlighting of this option
                    Console.BackgroundColor = originalBackground;
                    Console.ForegroundColor = originalForeground;
                }
                else
                {
                    //write without the highlight
                    Console.WriteLine(options[i]);
                }
            }
        }

        /// <summary>
        /// Clears the lines that needs to be updated in the menu
        /// </summary>
        /// <param name="selected">the line that is currently selected</param>
        /// <param name="prevSelected">the line that was most previously selected</param>
        private static void ClearLine( int selected, int prevSelected)
        {
            Console.SetCursorPosition(0, selected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, prevSelected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 0);
        }
    }
}
