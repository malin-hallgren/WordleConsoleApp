using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Utilities;
using WordleConsoleApp.Utilities.Menus;

namespace WordleConsoleApp.HighScore
{
    internal class HighScoreBoard
    {
        private readonly static string filePath = "highscores.json";
        public static Dictionary<string, int> HighScores { get; private set; } = new Dictionary<string, int>();


        static HighScoreBoard()
        {
            HighScores = JsonHelper.LoadDict<string, int>(filePath);
        }
        public static void PrintScoreBoard()
        {
            int rank = 1;

            foreach (var score in HighScores)
            {
                string rankString = $"{rank}.\t{score.Key}\t{score.Value}\n";
                HighScoreColor(rankString, rank);
                rank++;
            }

            Console.CursorVisible = false;
            Console.WriteLine("\nPress ENTER to return to menu...");
            Console.ReadLine();
            Console.CursorVisible = true;
        }

        public static void HighScoreColor(string entry, int rank)
        {
            switch (rank)
            {
                case 1:
                    FormatManager.Highlight(entry, ConsoleColor.Yellow);
                    break;
                case 2:
                    FormatManager.Highlight(entry, ConsoleColor.DarkGray);
                    break;
                case 3:
                    FormatManager.Highlight(entry, ConsoleColor.DarkYellow);
                    break;
                default:
                    Console.Write(entry);
                    break;
            }
        }

        public static void UpdateScoreBoard(Player player)
        {
            if(!HighScores.TryAdd(player.UserName, player.HighScore))
            {
                HighScores.Remove(player.UserName);
                HighScores.Add(player.UserName, player.HighScore);
            }
            ScoreSort();
            JsonHelper.SaveDict(filePath, HighScores);
        }

        private static void ScoreSort()
        {
            HighScores = HighScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public static void ClearSingleScore(Player player)
        {
            HighScores.Remove(player.UserName);
        }
        public static void ClearScoreBoard(Game game)
        {
            int choice = MenuController.Choice(MenuController.ConfirmationMenu[1..], MenuController.ConfirmationMenu[0]);
            if ( choice +1 == 1)
            {
                HighScores.Clear();
                JsonHelper.SaveDict(filePath, HighScores);

                foreach (var user in game.ActiveUsers)
                {
                    if (user is Player)
                    {
                        var player = (Player)user;
                        player.HighScore = 0;
                    }
                }

                JsonHelper.SaveList<BasicUser>(game._activeUsersPath, game.ActiveUsers);
                Console.WriteLine("High Score Board cleared, All high scores on individual users cleared!\n" + ManagerSettings.returnString);
            }

            else
            {
                Console.WriteLine("Clearing High Score board has been aborted, " + ManagerSettings.returnString);
            }

            Console.ReadLine();
            ManagerSettings.OpenSettings(ManagerSettings.Title);
        }

    }
}
