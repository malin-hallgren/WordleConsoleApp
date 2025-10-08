using WordleConsoleApp.User;
using WordleConsoleApp.Utilities;
using WordleConsoleApp.Words;

namespace WordleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var word = new Word();
            var menu = new MenuUI();
            var dynamicMenu = new DynamicMenu();
            game.ActiveUsers.Add(new Manager());
            
            //create new player object, ask if user want to play as this player or 
            //create new. Save old player. List<BasicUser>? ActiveUsers, go by isCurrent bool
            while (menu.StartMenu(word, game))
            {
                game.setPlayer(dynamicMenu);
                word.ScrambleWord(word.SelectedWord);
                game.DisplayWord(word);

                while(game.Attempt < game.MaxAttempts && !game.isCorrect)
                {
                    game.MakeGuess(word);
                }

                FormatManager.TabToPos(0, game.StaticRows, game.Attempt + 1);
                if (game.isCorrect)
                {
                    Console.WriteLine("\nGood job! You guessed the word!\n");
                    game.CurrentPlayer.CheckNewHighScore();
                }
                else
                {
                    Console.WriteLine("Sorry you did not guess the word.");
                    Console.WriteLine($"The word was: {word.SelectedWord}");
                }

                Console.WriteLine("\nPress Enter to return to menu...");
                Console.ReadLine();
            }
        }
    }
}
