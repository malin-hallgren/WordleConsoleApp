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

        public static void UpdateScoreBoard(string name, int score)
        {
            if(!HighScores.TryAdd(name, score))
            {
                HighScores.Remove(name);
                HighScores.Add(name, score);
            }
            SortByScore();
            JsonHelper.SaveDictToPath(filePath, HighScores);
            //SaveHighScores();
        }

        private static void SortByScore()
        {
            HighScores = HighScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

    }
}
