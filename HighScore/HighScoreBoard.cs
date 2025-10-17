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
            HighScores = FileManager.LoadDictFromPath<string, int>(filePath);
        }

        [ObsoleteAttribute("This is replaced by generic SaveToPath")]
        private static void SaveHighScores()
        {
            try
            {
                string json = System.Text.Json.JsonSerializer.Serialize(HighScores);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving high scores: {ex.Message}");
            }
        }

        [ObsoleteAttribute("This is replaced by generic LoadFromPath")]
        private static void LoadHighScores()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    HighScores = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
                }
                else
                {
                    Console.WriteLine("No High Scores found, New High Score board created!");
                    HighScores = new Dictionary<string, int>();
                    SaveHighScores();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading high scores: {ex.Message}\nCreating a new file, Rip your old High Scores, they are gone :(");
                HighScores = new Dictionary<string, int>();
                SaveHighScores();
            }
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
            FileManager.SaveDictToPath(filePath, HighScores);
            //SaveHighScores();
        }

        private static void SortByScore()
        {
            HighScores = HighScores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

    }
}
