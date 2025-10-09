using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal abstract class MenuUI
    {
        public List<string> PlayerStartMenu = new List<string> { "Start Game", "Switch User", "High Scores", "Quit" };
        public List<string> ManagerStartMenu = new List<string> { "Settings", "Switch User", "High Scores", "Quit" };

        public static int MakeMenuChoice<T>(List<T> options, string title = "WordGuess")
        {
            Console.CursorVisible = false;
            int selected = 0;
            int prevSelected = selected;
            Console.Clear();

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
        private static void DrawMenu<T>(List<T> options, int selected, int prevSelected, string title)
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

        private static void ClearLine<T>(List<T> options, int selected, int prevSelected)
        {
            Console.SetCursorPosition(0, selected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, prevSelected + 2);
            Console.Write(new string(' ', Console.BufferWidth));
            Console.SetCursorPosition(0, 0);
        }

        ////Selector for the start menu
        //public int MakeMenuChoice(string title = "WordGuess")
        //{
        //    Console.CursorVisible = false;
        //    int selected = 0;
        //    int prevSelected = selected;
        //    Console.Clear();


        //    DrawMenu(StartMenuOptions, selected, prevSelected, title);

        //    while (true)
        //    {
        //        var pressedKey = Console.ReadKey(true).Key; //Using true to avoid printing the key to console

        //        switch (pressedKey)
        //        {
        //            case ConsoleKey.UpArrow:
        //            case ConsoleKey.W:
        //                prevSelected = selected;
        //                selected = (selected - 1 + StartMenuOptions.Length) % StartMenuOptions.Length;
        //                ClearLine(StartMenuOptions, selected, prevSelected);
        //                DrawMenu(StartMenuOptions, selected, prevSelected, title);
        //                break;
        //            case ConsoleKey.DownArrow:
        //            case ConsoleKey.S:
        //                prevSelected = selected;
        //                selected = (selected + 1) % StartMenuOptions.Length;
        //                ClearLine(StartMenuOptions, selected, prevSelected);
        //                DrawMenu(StartMenuOptions, selected, prevSelected, title);
        //                break;
        //            case ConsoleKey.Enter:
        //                Console.CursorVisible = true;
        //                return selected;
        //        }
        //    }

        //}

        ////Highlights for the selected element
        //private void DrawMenu(string[] options, int selected, int prevSelected, string title)
        //{
        //    Console.WriteLine($"{title}\n");

        //    for (int i = 0; i < options.Length; i++)
        //    {
        //        //Checks if we should highlight the option
        //        if (i == selected)
        //        {
        //            //Saves original colours
        //            ConsoleColor originalBackground = Console.BackgroundColor;
        //            ConsoleColor originalForeground = Console.ForegroundColor;

        //            //Inverts colours for highlight
        //            Console.BackgroundColor = ConsoleColor.Gray;
        //            Console.ForegroundColor = ConsoleColor.Black;

        //            Console.WriteLine(options[i]);

        //            //resets colour to prevent permanent higlighting of this option
        //            Console.BackgroundColor = originalBackground;
        //            Console.ForegroundColor = originalForeground;
        //        }
        //        else
        //        {
        //            //write without the highlight
        //            Console.WriteLine(options[i]);
        //        }
        //    }
        //}

        //private static void ClearLine(string[] options, int selected, int prevSelected)
        //{
        //    Console.SetCursorPosition(0, selected + 2);
        //    Console.Write(new string(' ', Console.BufferWidth));
        //    Console.SetCursorPosition(0, prevSelected + 2);
        //    Console.Write(new string(' ', Console.BufferWidth));
        //    Console.SetCursorPosition(0, 0);
        //}
    }
}
