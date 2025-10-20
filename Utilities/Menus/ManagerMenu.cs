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
    internal class ManagerMenu : MenuController
    {
        public static string Title { get; set; } = "Welcome to WordGuess, what would you like to manage, ";

        public static Dictionary<string, Action> Options = new Dictionary<string, Action>()
        {
            {"Settings", () =>  ManagerSettings.OpenSettings(ManagerSettings.Title) },
            {"Switch User", () => { CurrentGame.SetUser(); CurrentGame.SetGame(CurrentGame, CurrentWord); } },
            {"High Scores", () => HighScoreBoard.PrintScoreBoard()},
            {"Print User List", () => BasicUser.PrintUserList(CurrentGame.ActiveUsers) },
            {"Display Word List", () => CurrentWord.PrintPossibleList(CurrentWord) },
            {"Quit Game",  () => Environment.Exit(0)}
        };

        public ManagerMenu(Game game, Word word) : base(game, word)
        {
            CurrentGame = game;
            CurrentWord = word;
        }
    }
}
