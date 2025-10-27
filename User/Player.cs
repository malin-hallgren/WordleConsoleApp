using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Utilities;
using WordleConsoleApp.HighScore;
using WordleConsoleApp.Utilities.Menus;

namespace WordleConsoleApp.User
{
    internal class Player : BasicUser
    {
        public int CurrentScore { get; set; }

        public int HighScore { get; set; }

        public Player(string userName = "New Player", int currentScore = 0)
        {
            UserName = userName;
            CurrentScore = currentScore;
        }

        /// <summary>
        /// Updates UserName of the player
        /// </summary>
        /// <param name="player">The player objects whose name will be updated</param>
        public Player UpdateShellName(Player player, Game game)
        {
            bool nameSet = false;

            do
            {
                Console.WriteLine("Please input a unique username:");
                string? inputtedName;
                int selected = 0;

                inputtedName = InputManager.CheckStringInput();
                if (game.ActiveUsers.Exists(x => x.UserName == inputtedName))
                {
                    selected = MenuController.Choice(MenuController.ExistingUser, $"A Player with name \"{inputtedName}\" already exists");
                    if (selected == 0)
                    {
                        continue;
                    }
                    else if (selected == 1)
                    {
                        player = (Player)game.ActiveUsers.Find(x => x.UserName.Contains(inputtedName));
                    }
                    else
                    {
                        player = (Player)game.SetUser();
                    }
                }
                else
                {
                    player.UserName = inputtedName;
                }   
                nameSet = true;

            } while (!nameSet);
            

            player.IsShellUser = false;
            Console.Clear();
            return player;
        }

        /// <summary>
        /// Checks if the CurrentScore of the player that runs the method is higher than the HighScore and sets it and informs player if approppriate
        /// </summary>
        public void CheckScore(Game game)
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                FormatManager.Highlight($"New High Score: {HighScore}!", ConsoleColor.Yellow);
                HighScoreBoard.UpdateScoreBoard(this);
            }

            CurrentScore = 0;
            JsonHelper.SaveList(game._activeUsersPath, game.ActiveUsers);
        }
    }
}
