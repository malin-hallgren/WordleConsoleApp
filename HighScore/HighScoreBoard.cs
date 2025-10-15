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
        public static Dictionary<string, int> HighScores { get; private set; } = new Dictionary<string, int>();
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
                    Console.WriteLine(entry);
                    break;
            }
        }

        public static void UpdateScoreBoard(string name, int score)
        {
            if(!HighScores.TryAdd(name, score))
            {
                RemoveOldEntry(name);
                HighScores.Add(name, score);
            }

            
            SortByScore();
        }

        private static void SortByScore()
        {
            HighScores = HighScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        private static void RemoveOldEntry(string name)
        {
            HighScores.Remove(name);
        }
    }
}
