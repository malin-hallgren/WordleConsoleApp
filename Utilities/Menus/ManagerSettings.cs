using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.HighScore;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class ManagerSettings : ManagerMenu
    {
        //Add option to clear user-added words
        public static Dictionary<string, Action> Options = new Dictionary<string, Action>()
        {
            {"Add Word", AddWord},
            {"Remove Word", RemoveWord},
            {"Clear High Scores", () => HighScoreBoard.ClearScoreBoard(CurrentGame)},
            {"Remove User", RemoveUser},
            {"Back", () => { } }
        };

        public static string returnString = "Press ENTER to return to Manager Settings Menu";

        public static new string Title { get; set; } = "What do you wish to change?";

        public ManagerSettings(Game game, Word word) : base(game, word)
        {
            CurrentGame = game;
            CurrentWord = word;
        }


        /// <summary>
        /// Opens the Manager settings menu
        /// </summary>
        /// <param name="settingsTitle">Title of the menu to display</param>
        public static void OpenSettings(string settingsTitle)
        {
            int selectedKey = Choice(Options.Keys.ToList(), settingsTitle);
            Options[Options.Keys.ToList()[selectedKey]]();
        }

        /// <summary>
        /// Opens menu to adds a word to the list of possible words and saves it to the json
        /// </summary>
        public static void AddWord()
        {
            Console.WriteLine("Input the word you wish to add:");
            string word = InputManager.CheckStringInput().ToLower();
            CurrentWord.PossibleWords.Add(word);
            JsonHelper.SaveList(CurrentWord._filePath, CurrentWord.PossibleWords);

            Console.WriteLine($"\"{word}\" was added to the list of possible words. " + returnString);
            Console.ReadLine();

            OpenSettings(Title);
        }

        /// <summary>
        /// opens menu allowing for removal of a word from the existing word list and saves the new word list
        /// </summary>
        public static void RemoveWord()
        {
            string deleteHeader = "Which word do you wish to remove? Default words may not be deleted and are not displayed here";
            List<string> deletableWords = CurrentWord.PossibleWords.Except(CurrentWord.DefaultWords).ToList();

            deletableWords.Add("Wipe All");
            deletableWords.Add("Back");

            int choice = Choice(deletableWords, deleteHeader);

            if (choice == deletableWords.Count - 1)
            {
                Console.WriteLine("No Words deleted, " + returnString);
                Console.ReadLine();
            }
            else if (choice == deletableWords.Count - 2)
            {
                choice = Choice(ConfirmationMenu[1..], ConfirmationMenu[0]);
                if (choice + 1 == 1)
                {
                    CurrentWord.PossibleWords = CurrentWord.PossibleWords.Intersect(CurrentWord.DefaultWords).ToList();
                    Console.WriteLine("All custom words removed, " + returnString);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Wiping of words aborted, " + returnString);
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine($"\"{CurrentWord.PossibleWords[choice + CurrentWord.DefaultWords.Count]}\" was removed from the list of possible words. " + returnString);
                CurrentWord.PossibleWords.RemoveAt(choice + CurrentWord.DefaultWords.Count);
                Console.ReadLine();
            }

            JsonHelper.SaveList(CurrentWord._filePath, CurrentWord.PossibleWords);

            OpenSettings(Title);
        }

        /// <summary>
        /// Opens menu to select and remove a user, or wipe all users
        /// </summary>
        public static void RemoveUser()
        {
            CurrentGame.ActiveUsers.RemoveAll(x => x.UserName == "New Player");

            CurrentGame.ActiveUsers.Add(new Player("Wipe All"));
            CurrentGame.ActiveUsers.Add(new Player("Back"));
            int toRemove = Choice(CurrentGame.ActiveUsers, "Which user to remove?");
            int choice = 0;

            if (CurrentGame.ActiveUsers[toRemove].IsAdmin)
            {
                Console.WriteLine("Admin user cannot be removed, " + returnString);
            }
            else if (CurrentGame.ActiveUsers[toRemove].UserName == "Wipe All")
            {
                choice = Choice(ConfirmationMenu[1..], ConfirmationMenu[0]);

                if (choice + 1 == 1)
                {
                    Manager savedManager = CurrentGame.CurrentManager;

                    CurrentGame.ActiveUsers.Clear();
                    HighScoreBoard.ClearScoreBoard(CurrentGame);
                    CurrentGame.ActiveUsers.Add(savedManager);

                    Console.WriteLine("All Users and High Scores wiped! " + returnString);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Wiping of Users aborted, " + returnString);
                }
                
            }
            else if (CurrentGame.ActiveUsers[toRemove].UserName != "Back")
            {
                Console.WriteLine($"Player {CurrentGame.ActiveUsers[toRemove]} has been removed and their scores on the High Score Board has been wiped. " + returnString);
                HighScoreBoard.ClearSingleScore((Player)CurrentGame.ActiveUsers[toRemove]);
                CurrentGame.ActiveUsers.RemoveAt(toRemove);
                Console.ReadLine();
            }

            CurrentGame.ActiveUsers.RemoveAll(x => x.UserName == "Back" || x.UserName == "Wipe All");
            JsonHelper.SaveList(CurrentGame._activeUsersPath, CurrentGame.ActiveUsers);

            OpenSettings(Title);
        }
    }
}
