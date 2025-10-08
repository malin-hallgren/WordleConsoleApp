using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void UpdateScore(int attempt, int maxAttempt, int lengthOfWord)
        {
            CurrentScore += 10 * (maxAttempt - (attempt + 1)) * lengthOfWord;
        }

        public void CheckNewHighScore()
        {
            if (CurrentScore > HighScore)
            {
                HighScore = CurrentScore;
            }
        }
    }
}
