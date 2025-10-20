using WordleConsoleApp.User;
using WordleConsoleApp.Utilities;
using WordleConsoleApp.Utilities.Menus;
using WordleConsoleApp.Words;
using WordleConsoleApp.HighScore;

namespace WordleConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var word = new Word();
            var menu = new MenuController(game, word);
            var highScoreBoard = new HighScoreBoard(); //not needed?

            game.SetUser();

            //create new player object, ask if user want to play as this player or 
            //create new. Save old player. List<BasicUser>? ActiveUsers, go by isCurrent bool

            
            while (game.isOngoing)
            {
                if (game.CurrentUser is Player)
                {
                    //if(!PlayerMenu.StartMenuSelector(word, game))
                    //{
                    //    continue;
                    //}

                    bool hasStarted = false;
                    do
                    {
                        int selectedAction = MenuController.MakeMenuChoice(PlayerMenu.PlayerMenuOptions.Keys.ToList(), PlayerMenu.Title);
                        PlayerMenu.PlayerMenuOptions[PlayerMenu.PlayerMenuOptions.Keys.ToList()[selectedAction]]();

                        if (selectedAction == 0)
                        {
                            hasStarted = true;
                        }

                    } while (!hasStarted);

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
                        game.CurrentPlayer.CheckNewHighScore(game);
                    }
                    else
                    {
                        Console.WriteLine("Sorry you did not guess the word.");
                        Console.WriteLine($"The word was: {word.SelectedWord}");
                    }

                    Console.WriteLine("\nPress Enter to return to menu...");
                    Console.ReadLine();
                }
                else
                {
                    //if(!ManagerMenu.ManagerMenuSelector(word, game))
                    //{
                    //    continue;
                    //}
                }
            }
        }
    }
}
