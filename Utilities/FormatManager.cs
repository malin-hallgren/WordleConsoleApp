using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordleConsoleApp.Utilities
{
    internal class FormatManager
    {
        /// <summary>
        /// Highlights the input param in the selected color
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input">The text to color</param>
        /// <param name="color">The ConsoleColor to use for highlight</param>
        public static void HighlightOutput<T>(T input, ConsoleColor color)
        {
            
            ConsoleColor originalForeground = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.Write(input);
            Console.ForegroundColor = originalForeground;
        }

        /// <summary>
        /// Tabs the cursor to a specified position
        /// </summary>
        /// <param name="tab">How many tabs in the cursor should be set as</param>
        /// <param name="staticRows">amount of static, unchanging rows</param>
        /// <param name="variableRows">Amount of rows that have dynamically been added to the window</param>
        public static void TabToPos(int variableRows, int tab = 8, int staticRows = 4)
        {
            int row = staticRows + variableRows;
            Console.SetCursorPosition(tab, row);
        }


        /// <summary>
        /// Clears a specific row
        /// </summary>
        /// <param name="tab">amount of tabs in to start clearing. Usually 0 for a full row</param>
        /// <param name="staticRows">amount of static, unchanging rows</param>
        /// <param name="variableRows">Amount of rows that have dynamically been added to the window</param>
        public static void ClearRow(int variableRows)
        {
            TabToPos(variableRows);
            Console.Write(new string(' ', Console.BufferWidth));
        }

        /// <summary>
        /// Clears a specific row
        /// </summary>
        /// <param name="tab">amount of tabs in to start clearing. Usually 0 for a full row</param>
        /// <param name="staticRows">amount of static, unchanging rows</param>
        /// <param name="variableRows">Amount of rows that have dynamically been added to the window</param>
        /// <param name="numOfRows">Amount of rows to be cleared</param>
        public static void ClearRow(int variableRows, int numOfRows)
        {
            for (int i = 0; i < numOfRows; i++)
            {
                ClearRow(variableRows + i);
            }
            TabToPos(variableRows);
        }
    }
}
