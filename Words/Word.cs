using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.User;
using WordleConsoleApp.Utilities;

namespace WordleConsoleApp.Words
{
    internal class Word
    {
        private string _filePath = "WordLibrary.json";
        public List<string> PossibleWords { get; set; } = new List<string>
        {
            "duck", 
            "desk", 
            "chair", 
            "phone", 
            "bottle", 
            "screen", 
            "editor", 
            "laptop", 
            "monitor", 
            "program", 
            "software", 
            "computer",
            "terminal",
            "developer"
        };
        public string SelectedWord { get; private set; }
        public string ScrambledWord { get; private set; }
        

        public Word()
        {
            PossibleWords.AddRange(JsonHelper.LoadListFromPath<string>(_filePath).Where(word => !PossibleWords.Contains(word)));
        }

        /// <summary>
        /// picks a random word from the list of possible words
        /// </summary>
        public void PickWord()
        {
            var random = new Random();
            int randomIndex = random.Next(PossibleWords.Count);

            SelectedWord = PossibleWords[randomIndex].ToLower();
        }

        /// <summary>
        /// Scrambles the selected word for the player to guess
        /// </summary>
        /// <param name="word">the word to be scramnbled</param>
        public void ScrambleWord(string word)
        {
            Random random = new Random();

            int[] scrambler = new int[word.Length];
            Array.Fill(scrambler, -1);

            for (int i = 0; i < word.Length; i++)
            {
                bool isUnique = false;
                int numToAdd = 0;

                while (!isUnique)
                {
                    numToAdd = random.Next(0, word.Length);
                    isUnique = !scrambler.Contains(numToAdd);
                }
                scrambler[i] = numToAdd;
            }

            char[] scrambleArray = new char[word.Length];

            for (int i = 0; i < word.Length; i++)
            {
                scrambleArray[i] = word[scrambler[i]];
            }
            
            ScrambledWord = new string(scrambleArray);
        }

        /// <summary>
        /// Displays the scrambled word
        /// </summary>
        /// <param name="player">the current player object</param>
        public void DisplayWord(Player player)
        {
            Console.WriteLine($"Guess the word {player.UserName}!\n\n\t{ScrambledWord}\n");
        }

        
    }
}
