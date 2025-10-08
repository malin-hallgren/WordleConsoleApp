using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Utilities;

namespace WordleConsoleApp.User
{
    internal class Player : BasicUser
    {
        public int CurrentScore { get; set; }

        public int HighScore { get; private set; }

        public Player(string name = "New Player", int currentScore = 0)
        {
            UserName = name;
            CurrentScore = currentScore;
        }
        

        public void CheckNewHighScore()
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
                FormatManager.HighlightOutput($"New High Score: {HighScore}!", ConsoleColor.Yellow);
            }

            ResetScore();
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }
    }
}
