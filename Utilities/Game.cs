using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Utilities.Menus;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities
{
    internal class Game
    {
        public List<BasicUser> ActiveUsers { get; set; } = new List<BasicUser>();

        private BasicUser CurrentUser { get; set; }

        public Player CurrentPlayer { get; set; }

        public Manager CurrentManager { get; set; }
        private uint AmountCorrectLetters { get; set; }
        
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
            AmountCorrectLetters = 0;
        }

        public void setPlayer(UserSelectMenu userSelectMenu)
        {
            userSelectMenu.NewPlayerOption(ActiveUsers);
            CurrentUser = ActiveUsers[MenuUI.MakeMenuChoice(ActiveUsers, "Select User")];

            foreach (BasicUser user in ActiveUsers)
            {
                user.IsCurrentUser = false;
            }
            CurrentUser.IsCurrentUser = true;
            

            //This needs to stay, but potentially add one for manager too so we get the correct types 
            if (CurrentUser is Player)
            {
                CurrentPlayer = (Player)CurrentUser;

                if (CurrentPlayer.IsShellUser)
                {
                    CurrentPlayer.UpdateShellPlayerName(CurrentPlayer);
                }
            }
            else
            {
                CurrentManager = (Manager)CurrentUser;
            }

            StartMenu.Title += CurrentUser.UserName;
        }
        
        //Takes guess input and sends it into the checker to see if the guess was correct
        public void MakeGuess(Word word)
        {
            FormatManager.TabToPos(Tab, StaticRows, Attempt);
            CurrentGuess = InputManager.GuessInput(word.ScrambledWord.Length, word.ScrambledWord.Length + 1, Tab, StaticRows, Attempt);
            isCorrect = CheckGuess(CurrentGuess, word.SelectedWord);
            Attempt += 1;
        }

        
        //Prints out the current guess with highlights for correct letters and checks if the guess is fully correct
        private bool CheckGuess(string guess, string target)
        {
            int guessScore = 0;
            int addedCorrectLetters = 0;
            uint currentCorrectLetters = 0;

            for (int i = 0; i < target.Length; i++)
            {
                if (guess[i] == target[i])
                {
                    FormatManager.TabToPos(Tab + i, StaticRows, Attempt);
                    FormatManager.HighlightOutput(guess[i], ConsoleColor.Green);
                    currentCorrectLetters++;
                    
                }
                else
                {
                    FormatManager.TabToPos(Tab + i, StaticRows, Attempt);
                    Console.Write(guess[i]);
                }
            }
            if (currentCorrectLetters > AmountCorrectLetters)
            {
                addedCorrectLetters = (int)currentCorrectLetters - (int)AmountCorrectLetters;
                AmountCorrectLetters = currentCorrectLetters;
                currentCorrectLetters = 0;
            }

            guessScore = UpdateScore(addedCorrectLetters, Attempt, MaxAttempts, CurrentPlayer);

            if (guess == target)
            {
                PrintScore(guessScore, true, CurrentPlayer);
                return true;
            }
            else
            {
                PrintScore(guessScore, false, CurrentPlayer);
                return false;
            }
        }

        //Updates the players score for the current guess based on the amount of newly correct letters
        //also sets score in player object
        private int  UpdateScore(int newCorrectLetters, int attempt, int maxAttempt, Player player)
        {
            int guessScore = 0;
            
            guessScore += 10 * (maxAttempt - attempt) * newCorrectLetters;
            
            player.CurrentScore += guessScore;
            return guessScore;
        }

        //Print out the score next to the guess, if the guess is correct prints the final score below all the guess scores
        private void PrintScore(int guessScore, bool isCorrect, Player player)
        {
            char[] output = new char[5];
            Array.Fill(output, ' ');
            string printableScore;
            int startIndex;

            printableScore = guessScore.ToString();
            startIndex = output.Length - printableScore.Length - 1;

            output[startIndex - 1] = '+';

            int printableIndex = 0;

            for (int i = startIndex + 1; i < output.Length; i++)
            {
                
                output[i] = printableScore[printableIndex];
                printableIndex++;
            }

            FormatManager.TabToPos(2 * Tab + Tab / 2, StaticRows, Attempt);
            foreach (char c in output)
            {
                Console.Write(c);
            }

            //The tab maths are off the chart, but it is working, and it is staying relative to something, meaning if we chang the value of Tab
            //we don't have to change that much... the +2 is technically Tab / 4
            if (isCorrect)
            {
                FormatManager.TabToPos((2 * Tab) + (Tab / 2) + 2, StaticRows, Attempt +1);
                FormatManager.HighlightOutput(player.CurrentScore, ConsoleColor.DarkYellow);
            }
        }
    }
}
