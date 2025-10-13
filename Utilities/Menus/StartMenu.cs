using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class StartMenu : MenuUI
    {
        public static string Title { get; set; } = "Welcome to WordGuess, ";


        /// <summary>
        /// Initializes the StartMenu sequence
        /// </summary>
        /// <param name="word">the Word object for picking a word</param>
        /// <param name="game">the Game object to re/set game parameters</param>
        /// <returns>A bool to start the game, or quit it</returns>
        public bool StartMenuSelector(Word word, Game game)
        {
            while (true)
            {
                //Check which menu to show!
                int selected = MakeMenuChoice(PlayerStartMenu, Title);

                switch (selected)
                {
                    //Add if cases on option 1 to check if current user is player or manager
                    case 0:
                        Console.Clear();
                        word.PickWord();
                        game.SetGame(game);
                        return true;
                    case 1:
                        Console.Clear();
                        game.SetPlayer();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("coming soon");
                        Console.ReadLine();
                        break;
                    case 3:
                        game.isOngoing = false;
                        return false;
                }
            }
        }
    }
}
