using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Utilities;
using WordleConsoleApp.HighScore;

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
        public void UpdateShellPlayerName(Player player)
        {
            Console.WriteLine("Please input a username:");
            player.UserName = InputManager.CheckStringInput();
            player.IsShellUser = false;
            Console.Clear();
        }

        /// <summary>
        /// Checks if the CurrentScore of the player that runs the method is higher than the HighScore and sets it and informs player if approppriate
        /// </summary>
        public void CheckNewHighScore(Game game)
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                FormatManager.HighlightOutput($"New High Score: {HighScore}!", ConsoleColor.Yellow);
                HighScoreBoard.UpdateScoreBoard(this);
            }

            CurrentScore = 0;
            JsonHelper.SaveListToPath(game._activeUsersPath, game.ActiveUsers);
        }
    }
}
