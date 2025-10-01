using WordleConsoleApp.Words;

namespace WordleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var word = new Word();

            

            while (game.StartMenu())
            {
                word.PickWord();
                Console.ReadLine();
            }
        }
    }
}
