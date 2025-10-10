using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordleConsoleApp.Words;

namespace WordleConsoleApp.Utilities.Menus
{
    internal class ManagerMenu : MenuUI
    {
        public static string Title { get; set; } = "Welcome to WordGuess, what would you like to manage, ";
        public bool StartMenuSelector(Word word, Game game)
        {
            while (true)
            {
                //Check which menu to show!
                int selected = MakeMenuChoice(ManagerStartMenu, Title);

                switch (selected)
                {
                    //Add if cases on option 1 to check if current user is player or manager
                    case 0:
                        Console.Clear();
                        Console.WriteLine("coming soon (settings)");
                        return true;
                    case 1:
                        Console.Clear();
                        Console.WriteLine("coming soon (Switch User)");
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("coming soon (High Scores)");
                        Console.ReadLine();
                        break;
                    case 3:
                        return false;
                }
            }
        }
    }
}
