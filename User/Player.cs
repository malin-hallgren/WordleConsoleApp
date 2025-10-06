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

        public void UpdateScore(int attempt, int maxAttempt, int lengthOfWord)
        {
            CurrentScore += 10 * (maxAttempt - (attempt + 1)) * lengthOfWord;
        }
    }
}
