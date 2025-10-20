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
    internal class PlayerMenu : MenuController
    {
        public static string Title { get; set; } = "Welcome to WordGuess, ";

        public static Dictionary<string, Action> Options = new Dictionary<string, Action>()
        {
            {"Start Game", () =>  { CurrentGame.SetGame(CurrentGame, CurrentWord); CurrentWord.Pick();} },
            {"Switch User", () => { CurrentGame.SetUser(); CurrentGame.SetGame(CurrentGame, CurrentWord); } },
            {"High Scores", () => HighScoreBoard.PrintScoreBoard()},
            {"Quit Game",  () => Environment.Exit(0)}
        };

        public PlayerMenu(Game game, Word word) : base(game, word) 
        {
            CurrentGame = game;
            CurrentWord = word;
        }
    }
}
