using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.HighScore;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class ManagerMenu : MenuController
    {
        public static string Title { get; set; } = "Welcome to WordGuess, what would you like to manage, ";

        public ManagerMenu(Game game, Word word) : base(game, word)
        {
            CurrentGame = game;
            CurrentWord = word;
        }

        //public static bool ManagerMenuSelector(Word word, Game game)
        //{
        //    CurrentWord = word;
        //    CurrentGame = game;
        //    while (true)
        //    {
        //        int selected = MakeMenuChoice(ManagerStartMenu, Title);

        //        switch (selected)
        //        {
        //            //Add if cases on option 1 to check if current user is player or manager
        //            case 0:
        //                Console.Clear();
        //                string selectedKey = ManagerSettingsMenu.ManagerSettingsStrings[MakeMenuChoice(ManagerSettingsMenu.ManagerSettingsStrings, "What do you wish to change?")];
        //                ManagerSettingsMenu.ManagerSettingsOptions[selectedKey]();
        //                return true;
        //            case 1:
        //                Console.Clear();
        //                game.SetUser();
        //                return false;
        //            case 2:
        //                Console.Clear();
        //                HighScoreBoard.PrintScoreBoard();
        //                Console.ReadLine();
        //                return false;
        //            case 3:
        //                game.isOngoing = false;
        //                return false;
        //        }
        //    }
        //}
    }
}
