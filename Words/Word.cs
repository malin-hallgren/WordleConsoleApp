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
        private string CurrentDirectory;
        private string[] PrecursorPossibleWords;
        private List <string> PossibleWords { get; set; } 
        public string SelectedWord { get; private set; }

        public Word()
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            PrecursorPossibleWords = File.ReadAllLines(CurrentDirectory + @"\Words\WordLibrary.txt");
            PossibleWords = PrecursorPossibleWords.ToList();
            SelectedWord = PickWord();
        }

        public string PickWord()
        {
           
            
            foreach (var word in PossibleWords)
            {
                Console.WriteLine(word);
            }

            var random = new Random();
            int randomIndex = random.Next(PossibleWords.Count);

            Console.WriteLine($"Word picked was: {PossibleWords[randomIndex]}");
            return PossibleWords[randomIndex];
        }
    }
}
