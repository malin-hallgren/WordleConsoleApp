using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp
{
    internal class Game
    {
        public string[] MenuOptions { get; set; } = { "Start Game", "High Scores", "Settings", "Quit" };
        public bool GameRunning { get; set; }

        public bool StartMenu()
        {
            while (true)
            {
                int selected = MakeMenuChoice();

                switch (selected)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("I am running!");
                        return true;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("coming soon");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("coming soon");
                        Console.ReadLine();
                        break;
                    case 3:
                        return false;
                }
            }
            
        }

        //Selector for the start menu
        public int MakeMenuChoice(string title = "WordGuess")
        {
            Console.CursorVisible = false;
            int selected = 0;
            int prevSelected = selected;
            Console.Clear();


            DrawMenu(MenuOptions, selected, prevSelected, title);

            while (true)
            {
                var pressedKey = Console.ReadKey(true).Key; //Using true to avoid printing the key to console

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        prevSelected = selected;
                        selected = (selected - 1 + MenuOptions.Length) % MenuOptions.Length;
                        ClearLine(MenuOptions, selected, prevSelected);
                        DrawMenu(MenuOptions, selected, prevSelected, title);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        prevSelected = selected;
                        selected = (selected + 1) % MenuOptions.Length;
                        ClearLine(MenuOptions, selected, prevSelected);
                        DrawMenu(MenuOptions, selected, prevSelected, title);
                        break;
                    case ConsoleKey.Enter:
                        Console.CursorVisible = true;
                        return selected;
                }
            }

        }

        //Highlights for the selected element
        private void DrawMenu(string[] options, int selected, int prevSelected, string title)
        {
            Console.WriteLine($"{title}\n");

            for (int i = 0; i < options.Length; i++)
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

        private static void ClearLine(string[] options, int selected, int prevSelected)
        {
            Console.SetCursorPosition(0, selected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, prevSelected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 0);
        }
    }
}
