using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.Utilities
{
    internal class Formatter
    {

        //look inot passing string and casting to ConsoleColor Enum
        public static void HighlightOutput<T>(T input, ConsoleColor color)
        {
            
            ConsoleColor originalForeground = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(input);
            Console.ForegroundColor = originalForeground;
        }

        public static void TabToPos(int tab, int staticRows, int variableRows)
        {
            int row = staticRows + variableRows;
            Console.SetCursorPosition(tab, row);
        }

        public static void ClearRow(int tab, int staticRows, int variableRows)
        {
            TabToPos(tab, staticRows, variableRows);
            Console.Write(new string(' ', Console.BufferWidth));
        }

        public static void ClearRow(int tab, int staticRows, int variableRows, int numOfRows)
        {
            for (int i = 0; i < numOfRows; i++)
            {
                ClearRow(tab, staticRows, variableRows + i);
            }
            TabToPos(tab, staticRows, variableRows);
        }
    }
}
