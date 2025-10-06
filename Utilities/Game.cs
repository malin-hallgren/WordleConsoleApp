using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities
{
    internal class Game
    {
        public List<BasicUser> ActiveUsers { get; set; } = new List<BasicUser>();

        public BasicUser currentUser { get; set; }
        public bool GameRunning { get; set; }

        public int Attempt {  get; private set; }

        public int MaxAttempts { get; private set; }

        public string CurrentGuess { get; private set; }

        public bool isCorrect { get; private set; }

        private int Tab { get; set; } = 8;

        public int StaticRows { get; private set; } = 4;

        public void SetGame(Game game)
        {
            Attempt = 0;
            MaxAttempts = 5;
            isCorrect = false;
        }

        public void setPlayer(DynamicMenu dynamicMenu)
        {  
            currentUser = ActiveUsers[dynamicMenu.MakeMenuChoice(ActiveUsers, "Select User")];
            if (currentUser.UserName == "New Player")
            {
                //take name input, consider enum
            }
        }

        public void DisplayWord(Word word)
        {
            Console.WriteLine($"Guess the word!\n\n\t{word.ScrambledWord}\n");
        }

        public void MakeGuess(Word word)
        {
            Formatter.TabToPos(Tab, StaticRows, Attempt);
            CurrentGuess = GuessInput(word.ScrambledWord.Length, word.ScrambledWord.Length + 1);
            isCorrect = CheckGuess(CurrentGuess, word.SelectedWord);
            Attempt += 1;
        }

        

        private bool CheckGuess(string guess, string target)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (guess[i] == target[i])
                {
                    Formatter.TabToPos(Tab + i, StaticRows, Attempt);
                    Formatter.HighlightOutput(guess[i], ConsoleColor.Green);
                    //Player.UpdateScore(Attempt, MaxAttempts, target.Length);
                }
                else
                {
                    Formatter.TabToPos(Tab + i, StaticRows, Attempt);
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
                    Formatter.TabToPos(Tab, StaticRows, Attempt);
                    Console.WriteLine("Faulty guess, make a guess no longer than the scrambled word.");
                    Formatter.TabToPos(Tab, StaticRows, Attempt+1);
                    Console.WriteLine("Press Enter to guess again");
                    Console.CursorVisible = false;
                    Console.ReadLine();
                    Formatter.ClearRow(Tab, StaticRows, Attempt, 4);
                    Console.CursorVisible = true;
                }
            }
        }  
    }
}
