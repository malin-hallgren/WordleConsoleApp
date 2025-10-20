using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.HighScore;
using WordleConsoleApp.User;
using WordleConsoleApp.Utilities.Menus;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities
{
    internal class Game
    {
        public string _activeUsersPath = "activeusers.josn";
        public List<BasicUser> ActiveUsers { get; set; } = new List<BasicUser>(); //this needs to be serialised to be persistent

        public BasicUser CurrentUser { get; set; }

        public Player CurrentPlayer { get; set; }

        public Manager CurrentManager { get; set; }

        public bool isOngoing { get; set; } = true;
        private uint AmountCorrectLetters { get; set; }
        
        public int Attempt {  get; private set; }

        public int MaxAttempts { get; private set; }

        public string CurrentGuess { get; private set; }

        public bool isCorrect { get; private set; }

        public int Tab { get; private set; } = 8;

        public int StaticRows { get; private set; } = 4;

        

        /// <summary>
        /// Resets the game to a game start state
        /// </summary>
        /// <param name="game">the game object which to reset</param>
        public void SetGame(Game game, Word word)
        {
            Attempt = 0;
            MaxAttempts = 5;
            isCorrect = false;
            AmountCorrectLetters = 0;
            word.Pick();
        }

        /// <summary>
        /// Sets up which User to play as and casts to corect type for using subclass specific methods
        /// </summary>
        public BasicUser SetUser()
        {
            ActiveUsers = JsonHelper.LoadList<BasicUser>(_activeUsersPath);

            UserSelect.SetUpUserList(ActiveUsers);
            

            bool validChoice = false;

            do
            {
                CurrentUser = ActiveUsers[MenuController.Choice(ActiveUsers, "Select User")];

                if (CurrentUser is Player) //check what happens when a new user is created and why it becomes a manager
                {
                    CurrentPlayer = (Player)CurrentUser;

                    if (CurrentPlayer.IsShellUser)
                    {
                        CurrentPlayer.UpdateShellName(CurrentPlayer);
                        
                    }

                    PlayerMenu.Title = $"Welcome to WordGuess, {CurrentUser.UserName}!";

                    validChoice = true;
                }
                else
                {
                    CurrentManager = (Manager)CurrentUser;

                    if (CurrentManager.CheckPassword(CurrentManager))
                    {
                        ManagerMenu.Title = $"Welcome to WordGuess, {CurrentManager.UserName}! What would you like to manage?";
                        validChoice = true;
                    }

                    else
                    {
                        Console.WriteLine("Login failed.");
                        Console.WriteLine("Press ENTER to return to User Selection Menu");
                    }
                }
            } while (!validChoice);

            JsonHelper.SaveList(_activeUsersPath, ActiveUsers);

            foreach (BasicUser user in ActiveUsers)
            {
                user.IsCurrentUser = false;
            }
            CurrentUser.IsCurrentUser = true;

            return CurrentUser;
        }
        


        /// <summary>
        /// Allows player to make a guess
        /// </summary>
        /// <param name="word">The current word object at play</param>
        public void MakeGuess(Word word)
        {
            FormatManager.TabToPos(Attempt);
            CurrentGuess = InputManager.GuessInput(word.ScrambledWord.Length, word.ScrambledWord.Length + 1, Attempt);
            isCorrect = CheckGuess(CurrentGuess, word.SelectedWord);
            Attempt += 1;
        }

        
        /// <summary>
        /// Checks the player's guess against the target word and prints a version highlighting correct letters and adds to the player's score
        /// </summary>
        /// <param name="guess">the player's guess</param>
        /// <param name="target">the current target word</param>
        /// <returns>returns true if guess is correct</returns>
        private bool CheckGuess(string guess, string target)
        {
            int guessScore = 0;
            int addedCorrectLetters = 0;
            uint currentCorrectLetters = 0;

            for (int i = 0; i < target.Length; i++)
            {
                if (guess[i] == target[i])
                {
                    FormatManager.TabToPos(Attempt, Tab + i);
                    FormatManager.Highlight(guess[i], ConsoleColor.Green);
                    currentCorrectLetters++;
                    
                }
                else
                {
                    FormatManager.TabToPos(Attempt, Tab + i);
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

        /// <summary>
        /// Updates the current player's score
        /// </summary>
        /// <param name="newCorrectLetters">amount of correct letters in surplus from the last guess</param>
        /// <param name="attempt">how many attempts have been made</param>
        /// <param name="maxAttempt">the max amount of attempts that may be made</param>
        /// <param name="player">the current player</param>
        /// <returns>the calculated score for this guess</returns>
        private int  UpdateScore(int newCorrectLetters, int attempt, int maxAttempt, Player player)
        {
            int guessScore = 0;
            
            guessScore += 10 * (maxAttempt - attempt) * newCorrectLetters;
            
            player.CurrentScore += guessScore;
            return guessScore;
        }

        /// <summary>
        /// Prints the score for the guess
        /// </summary>
        /// <param name="guessScore">the score to print</param>
        /// <param name="isCorrect">whether the guess is completely correct or not</param>
        /// <param name="player">the current player</param>
        private void PrintScore(int guessScore, bool isCorrect, Player player) //does this need a player object?
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

            FormatManager.TabToPos(Attempt, 2 * Tab + Tab / 2, StaticRows);
            foreach (char c in output)
            {
                Console.Write(c);
            }

            //The tab maths are off the chart, but it is working, and it is staying relative to something, meaning if we chang the value of Tab
            //we don't have to change that much... the +2 is technically Tab / 4
            if (isCorrect)
            {
                FormatManager.TabToPos(Attempt + 1, (2 * Tab) + (Tab / 2) + 2, StaticRows);
                FormatManager.Highlight(player.CurrentScore, ConsoleColor.DarkYellow);
            }
        }
    }
}
