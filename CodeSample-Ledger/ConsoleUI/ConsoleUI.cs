using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.ConsoleUI
{
    // Contains assorted static ConsoleUI methods that don't fit into their own class.
    public static class ConsoleUI
    {
        // Waits until user presses <Enter> key.
        public static void PressEnter()
        {
            Console.WriteLine("Press <Enter> to continue.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
