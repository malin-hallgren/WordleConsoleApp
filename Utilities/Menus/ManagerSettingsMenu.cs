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
        public static Dictionary<string, Action> ManagerSettingsOptions = new Dictionary<string, Action> ()
        {
            {"Add Word", AddWord},
            {"Clear High Scores", () => HighScoreBoard.ClearScoreBoard(_currentGame)},
            {"Remove User", RemoveUserMenu},
            {"Back", () => ManagerMenuSelector(_currentWord, _currentGame)}
        };

        public static List<string> ManagerSettingsStrings = ManagerSettingsOptions.Keys.ToList();

        public static void AddWord()
        {
            string word = InputManager.CheckStringInput().ToLower();
            _currentWord.PossibleWords.Add(word);
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
