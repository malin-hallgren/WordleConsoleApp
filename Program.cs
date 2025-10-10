using WordleConsoleApp.User;
using WordleConsoleApp.Utilities;
using WordleConsoleApp.Utilities.Menus;
using WordleConsoleApp.Words;

namespace WordleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var word = new Word();
            var startMenu = new StartMenu(); //make this a start menu, alternatively, run start menu from Game
            //game.ActiveUsers.Add(new Manager()); //does this need to happen here? cand we create this like the new player 

            game.setPlayer(); // could this be returning a player or manager into var user

            //create new player object, ask if user want to play as this player or 
            //create new. Save old player. List<BasicUser>? ActiveUsers, go by isCurrent bool
            while (startMenu.StartMenuSelector(word, game))
            {
                word.ScrambleWord(word.SelectedWord);
                word.DisplayWord(game.CurrentPlayer);

                while (game.Attempt < game.MaxAttempts && !game.isCorrect)
                {
                    game.MakeGuess(word);
                }

                FormatManager.TabToPos(game.Attempt + 1, 0);
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
