using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Words;

namespace WordleConsoleApp
{
    internal class Game
    {
        public string[] StartMenuOptions { get; set; } = { "Start Game", "High Scores", "Settings", "Quit" };
        public bool GameRunning { get; set; }

        public int Attempt {  get; private set; }

        public string CurrentGuess { get; private set; }

        public bool isCorrect { get; private set; } = false;

        private int Tab { get; set; } = 8;

        private int StaticRows { get; set; } = 4;

        public void DisplayWord(Word word)
        {
            Console.WriteLine($"Guess the word!\n\n\t{word.ScrambledWord}\n");
        }

        public void MakeGuess(Word word)
        {
            TabToPos(Tab, StaticRows, Attempt);
            CurrentGuess = GuessInput(word.ScrambledWord.Length, word.ScrambledWord.Length + 1);
            isCorrect = CheckGuess(CurrentGuess, word.SelectedWord);
            Attempt += 1;
        }

        public void TabToPos(int tab, int staticRows, int variableRows)
        {
            int row = staticRows + variableRows;
            Console.SetCursorPosition(tab, row);
        }

        public void ClearRow(int tab, int staticRows, int variableRows)
        {
            TabToPos(tab, staticRows, variableRows);
            Console.Write(new string(' ', Console.BufferWidth));
        }

        public void ClearRows(int tab, int staticRows, int variableRows, int numOfRows)
        {
            for (int i = 0; i < numOfRows; i++)
            {
                ClearRow(tab, staticRows, variableRows + i);
            }
            TabToPos(tab, staticRows, variableRows);
        }

        private bool CheckGuess(string guess, string target)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (guess[i] == target[i])
                {
                    //Saves original colours
                    ConsoleColor originalBackground = Console.BackgroundColor;
                    ConsoleColor originalForeground = Console.ForegroundColor;

                    //Inverts colours for highlight
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    TabToPos(Tab + i, StaticRows, Attempt);
                    Console.Write(guess[i]);

                    //resets colour to prevent permanent higlighting of this option
                    Console.BackgroundColor = originalBackground;
                    Console.ForegroundColor = originalForeground;
                }
                else
                {
                    TabToPos(Tab + i, StaticRows, Attempt);
                    Console.Write(guess[i]);
                }
            }

            if (guess == target)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GuessInput(int minRange, int maxRange)
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
                    TabToPos(Tab, StaticRows, Attempt);
                    Console.WriteLine("Faulty guess, make a guess no longer than the scrambled word.");
                    TabToPos(Tab, StaticRows, Attempt+1);
                    Console.WriteLine("Press Enter to guess again");
                    Console.ReadLine();
                    ClearRows(Tab, StaticRows, Attempt, 2);
                }
            }
        }
        public bool StartMenu(Word word)
        {
            while (true)
            {
                int selected = MakeMenuChoice();

                switch (selected)
                {
                    case 0:
                        Console.Clear();
                        word.PickWord();
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


            DrawMenu(StartMenuOptions, selected, prevSelected, title);

            while (true)
            {
                var pressedKey = Console.ReadKey(true).Key; //Using true to avoid printing the key to console

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        prevSelected = selected;
                        selected = (selected - 1 + StartMenuOptions.Length) % StartMenuOptions.Length;
                        ClearLine(StartMenuOptions, selected, prevSelected);
                        DrawMenu(StartMenuOptions, selected, prevSelected, title);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        prevSelected = selected;
                        selected = (selected + 1) % StartMenuOptions.Length;
                        ClearLine(StartMenuOptions, selected, prevSelected);
                        DrawMenu(StartMenuOptions, selected, prevSelected, title);
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
