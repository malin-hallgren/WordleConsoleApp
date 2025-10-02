using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.Words
{
    internal class Word
    {
        private string[] PrecursorPossibleWords;
        private List <string> PossibleWords { get; set; } 
        public string SelectedWord { get; private set; }
        public string ScrambledWord { get; private set; }
        

        public Word()
        {
            PrecursorPossibleWords = File.ReadAllLines(ReadFileDirectory());
            PossibleWords = PrecursorPossibleWords.ToList();
        }

        //Grabs the correct path for the file and makes it relative 
        private string ReadFileDirectory()
        {
            string baseDirectory = AppContext.BaseDirectory;
            return Path.Combine(baseDirectory, @"..\..\..\\Words\WordLibrary.txt");
        }
        public void PickWord()
        {
            var random = new Random();
            int randomIndex = random.Next(PossibleWords.Count);

            SelectedWord = PossibleWords[randomIndex].ToLower();
        }

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
    }
}
