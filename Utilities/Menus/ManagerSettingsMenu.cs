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
            //Remove New Player Option
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
                _currentGame.ActiveUsers.Add(savedManager);

                Console.WriteLine("All Users wiped!");
            }
            else if (_currentGame.ActiveUsers[toRemove].UserName != "Back")
            {
                Console.WriteLine($"Player {_currentGame.ActiveUsers[toRemove]} has been removed and their scores on the High Score Board has been wiped");
                HighScoreBoard.ClearSingleScore((Player)_currentGame.ActiveUsers[toRemove]);
                _currentGame.ActiveUsers.RemoveAt(toRemove);
                Console.ReadLine();
            }

            
            JsonHelper.SaveListToPath(_currentGame._activeUsersPath, _currentGame.ActiveUsers);
            //Remove wipe and back
        }

        public static void WipeUsers()
        {

        }
    }
}
