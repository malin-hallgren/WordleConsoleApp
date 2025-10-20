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
    internal class ManagerSettingsMenu : ManagerMenu
    {
        //Add option to clear user-added words
        public static Dictionary<string, Action> ManagerSettingsOptions = new Dictionary<string, Action> ()
        {
            {"Add Word", AddWord},
            {"Remove Word", RemoveWord},
            {"Clear High Scores", () => HighScoreBoard.ClearScoreBoard(_currentGame)},
            {"Remove User", RemoveUserMenu},
            {"Back", () => ManagerMenuSelector(_currentWord, _currentGame)}
        };

        public static List<string> ManagerSettingsStrings = ManagerSettingsOptions.Keys.ToList();

        public static void AddWord()
        {
            Console.WriteLine("Input the word you wish to add:");
            string word = InputManager.CheckStringInput().ToLower();
            _currentWord.PossibleWords.Add(word);
            JsonHelper.SaveListToPath(_currentWord._filePath, _currentWord.PossibleWords);
        }

        public static void RemoveWord()
        {
            string deleteHeader = "Which word do you wish to remove? Default words may not be deleted and are not displayed here";
            //List<string> deletableWords = _currentWord.PossibleWords.Skip(_currentWord.DefaultWords.Count).ToList();
            List<string> deletableWords = _currentWord.PossibleWords.Except(_currentWord.DefaultWords).ToList();

            deletableWords.Add("Wipe All");
            deletableWords.Add("Back");

            int choice = MakeMenuChoice(deletableWords, deleteHeader);

            if (choice == deletableWords.Count - 1)
            {
                Console.WriteLine("No Words deleted, returning to Manager Settings Menu");
                Console.ReadLine();
            }
            else if (choice == deletableWords.Count - 2)
            {
                choice = MakeMenuChoice(ConfirmationMenu[1..], ConfirmationMenu[0] + "delete all custom words?");
                if (choice + 1 == 1)
                {
                    _currentWord.PossibleWords = _currentWord.PossibleWords.Intersect(_currentWord.DefaultWords).ToList();
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
                _currentWord.PossibleWords.RemoveAt(choice + _currentWord.DefaultWords.Count);
            }

            JsonHelper.SaveListToPath(_currentWord._filePath, _currentWord.PossibleWords);
        }

        public static void RemoveUserMenu()
        {
            _currentGame.ActiveUsers.RemoveAll(x => x.UserName == "New Player");

            _currentGame.ActiveUsers.Add(new Player("Wipe All"));
            _currentGame.ActiveUsers.Add(new Player("Back"));
            int toRemove = MenuUI.MakeMenuChoice(_currentGame.ActiveUsers, "Which user to remove?");

            if (_currentGame.ActiveUsers[toRemove].IsAdmin)
            {
                Console.WriteLine("Admin user cannot be removed");
            }
            else if (_currentGame.ActiveUsers[toRemove].UserName == "Wipe All")
            {
                Manager savedManager = _currentGame.CurrentManager;

                _currentGame.ActiveUsers.Clear();
                HighScoreBoard.ClearScoreBoard(_currentGame);
                _currentGame.ActiveUsers.Add(savedManager);

                Console.WriteLine("All Users and High Scores wiped!");
            }
            else if (_currentGame.ActiveUsers[toRemove].UserName != "Back")
            {
                Console.WriteLine($"Player {_currentGame.ActiveUsers[toRemove]} has been removed and their scores on the High Score Board has been wiped");
                HighScoreBoard.ClearSingleScore((Player)_currentGame.ActiveUsers[toRemove]);
                _currentGame.ActiveUsers.RemoveAt(toRemove);
                Console.ReadLine();
            }

            _currentGame.ActiveUsers.RemoveAll(x => x.UserName == "Back" || x.UserName == "Wipe All");
            JsonHelper.SaveListToPath(_currentGame._activeUsersPath, _currentGame.ActiveUsers);
        }

        public static void WipeUsers()
        {

        }
    }
}
