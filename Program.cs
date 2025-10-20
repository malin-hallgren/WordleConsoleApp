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

            game.SetUser();

            while (game.isOngoing)
            {
                if (game.CurrentUser is Player)
                {
                    bool hasStarted = false;
                    do
                    {
                        int selectedAction = MenuController.Choice(PlayerMenu.Options.Keys.ToList(), PlayerMenu.Title);
                        PlayerMenu.Options[PlayerMenu.Options.Keys.ToList()[selectedAction]]();

                        

                        if (selectedAction == 0)
                        {
                            hasStarted = true;
                        }

                        if (game.CurrentUser is Manager)
                        {
                            break;
                        }

                    } while (!hasStarted);

                    if (game.CurrentUser is Manager)
                    {
                        continue;
                    }

                    word.Scramble(word.SelectedWord);
                    word.Display(game.CurrentPlayer);

                    while (game.Attempt < game.MaxAttempts && !game.isCorrect)
                    {
                        game.MakeGuess(word);
                    }

                    FormatManager.TabToPos(game.Attempt + 1, 0);
                    if (game.isCorrect)
                    {
                        Console.WriteLine("\nGood job! You guessed the word!\n");
                        game.CurrentPlayer.CheckScore(game);
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
                    int selectedAction = MenuController.Choice(ManagerMenu.Options.Keys.ToList(), ManagerMenu.Title);
                    ManagerMenu.Options[ManagerMenu.Options.Keys.ToList()[selectedAction]]();

                    if (game.CurrentUser is Player)
                    {
                       continue;
                    }
                }
            }
        }
    }
}
