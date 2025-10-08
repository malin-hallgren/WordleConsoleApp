using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities
{
    internal class DynamicMenu
    {

        public List<string> MenuOptions { get; set; }

        //Selector for the start menu
        public int MakeMenuChoice(List<BasicUser> options, string title = "WordGuess")
        {
            Console.CursorVisible = false;
            int selected = 0;
            int prevSelected = selected;
            Console.Clear();
            if (!options.Any(user => user.IsShellUser == true))
            {
                var newPlayerOption = new Player();
                newPlayerOption.IsShellUser = true;
                options.Add(newPlayerOption);
            }
            

            DrawMenu(options, selected, prevSelected, title);

            while (true)
            {
                var pressedKey = Console.ReadKey(true).Key; //Using true to avoid printing the key to console

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        prevSelected = selected;
                        selected = (selected - 1 + options.Count) % options.Count;
                        ClearLine(options, selected, prevSelected);
                        DrawMenu(options, selected, prevSelected, title);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        prevSelected = selected;
                        selected = (selected + 1) % options.Count;
                        ClearLine(options, selected, prevSelected);
                        DrawMenu(options, selected, prevSelected, title);
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        Console.Clear();
                        return selected;
                }
            }

        }

        //Highlights for the selected element
        private void DrawMenu(List<BasicUser> options, int selected, int prevSelected, string title)
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

                    Console.WriteLine(options[i].UserName);

                    //resets colour to prevent permanent higlighting of this option
                    Console.BackgroundColor = originalBackground;
                    Console.ForegroundColor = originalForeground;
                }
                else
                {
                    //write without the highlight
                    Console.WriteLine(options[i].UserName);
                }
            }
        }

        private static void ClearLine(List<BasicUser> options, int selected, int prevSelected)
        {
            Console.SetCursorPosition(0, selected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, prevSelected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 0);
        }
    }
}
