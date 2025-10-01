using WordleConsoleApp.Words;

namespace WordleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var word = new Word();

            

            while (game.StartMenu(word))
            {
                word.ScrambleWord(word.SelectedWord);
                game.DisplayWord(word);

                while(game.Attempt < 5 && !game.isCorrect)
                {
                    game.MakeGuess(word);
                }

                game.TabToPos(0, 4, game.Attempt + 1);
                Console.WriteLine("\nyou failed");
                Console.ReadLine();
            }
        }
    }
}
