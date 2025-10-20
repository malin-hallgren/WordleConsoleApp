using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Utilities;

namespace WordleConsoleApp.HighScore
{
    internal class HighScoreBoard
    {
        private readonly static string filePath = "highscores.json";
        public static Dictionary<string, int> HighScores { get; private set; } = new Dictionary<string, int>();


        static HighScoreBoard()
        {
            HighScores = JsonHelper.LoadDictFromPath<string, int>(filePath);
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
                    FormatManager.HighlightOutput(entry, ConsoleColor.Yellow);
                    break;
                case 2:
                    FormatManager.HighlightOutput(entry, ConsoleColor.DarkGray);
                    break;
                case 3:
                    FormatManager.HighlightOutput(entry, ConsoleColor.DarkYellow);
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
            SortByScore();
            JsonHelper.SaveDictToPath(filePath, HighScores);
        }

        private static void SortByScore()
        {
            HighScores = HighScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public static void ClearSingleScore(Player player)
        {
            HighScores.Remove(player.UserName);
        }
        public static void ClearScoreBoard(Game game)
        {
            HighScores.Clear();
            JsonHelper.SaveDictToPath(filePath, HighScores);

            foreach (var user in game.ActiveUsers)
            {
                if(user is Player)
                {
                    var player = (Player)user;
                    player.HighScore = 0;
                }
            }
        }

    }
}
