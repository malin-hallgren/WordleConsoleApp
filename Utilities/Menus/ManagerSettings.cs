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

        public static string Title { get; set; } = "What do you wish to change?";

        public ManagerSettings(Game game, Word word) : base(game, word)
        {
            CurrentGame = game;
            CurrentWord = word;
        }

        public static void OpenSettings(string settingsTitle)
        {
            int selectedKey = Choice(Options.Keys.ToList(), settingsTitle);
            Options[Options.Keys.ToList()[selectedKey]]();
        }

        public static void AddWord()
        {
            Console.WriteLine("Input the word you wish to add:");
            string word = InputManager.CheckStringInput().ToLower();
            CurrentWord.PossibleWords.Add(word);
            JsonHelper.SaveList(CurrentWord._filePath, CurrentWord.PossibleWords);

            OpenSettings(Title);
        }

        public static void RemoveWord()
        {
            string deleteHeader = "Which word do you wish to remove? Default words may not be deleted and are not displayed here";
            List<string> deletableWords = CurrentWord.PossibleWords.Except(CurrentWord.DefaultWords).ToList();

            deletableWords.Add("Wipe All");
            deletableWords.Add("Back");

            int choice = Choice(deletableWords, deleteHeader);

            if (choice == deletableWords.Count - 1)
            {
                Console.WriteLine("No Words deleted, returning to Manager Settings Menu");
                Console.ReadLine();
            }
            else if (choice == deletableWords.Count - 2)
            {
                choice = Choice(ConfirmationMenu[1..], ConfirmationMenu[0] + "delete all custom words?");
                if (choice + 1 == 1)
                {
                    CurrentWord.PossibleWords = CurrentWord.PossibleWords.Intersect(CurrentWord.DefaultWords).ToList();
                    Console.WriteLine("All custom words removed, returning to Manager Settings Menu");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Wiping of words aborted, returning to Manager Settings Menu");
                    Console.ReadLine();
                }
            }
            else
            {
                CurrentWord.PossibleWords.RemoveAt(choice + CurrentWord.DefaultWords.Count);
            }

            JsonHelper.SaveList(CurrentWord._filePath, CurrentWord.PossibleWords);

            OpenSettings(Title);
        }

        public static void RemoveUser()
        {
            CurrentGame.ActiveUsers.RemoveAll(x => x.UserName == "New Player");

            CurrentGame.ActiveUsers.Add(new Player("Wipe All"));
            CurrentGame.ActiveUsers.Add(new Player("Back"));
            int toRemove = Choice(CurrentGame.ActiveUsers, "Which user to remove?");

            if (CurrentGame.ActiveUsers[toRemove].IsAdmin)
            {
                Console.WriteLine("Admin user cannot be removed");
            }
            else if (CurrentGame.ActiveUsers[toRemove].UserName == "Wipe All")
            {
                Manager savedManager = CurrentGame.CurrentManager;

                CurrentGame.ActiveUsers.Clear();
                HighScoreBoard.ClearScoreBoard(CurrentGame);
                CurrentGame.ActiveUsers.Add(savedManager);

                Console.WriteLine("All Users and High Scores wiped!");
                Console.ReadLine();
            }
            else if (CurrentGame.ActiveUsers[toRemove].UserName != "Back")
            {
                Console.WriteLine($"Player {CurrentGame.ActiveUsers[toRemove]} has been removed and their scores on the High Score Board has been wiped");
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
