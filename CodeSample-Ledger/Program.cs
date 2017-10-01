using CodeSample_Ledger.Menus;
using System;

namespace CodeSample_Ledger
{
    class Program
    {
        static void Main(string[] args)
        {
            var opts = new string[20];
            for (var i = 0; i < opts.Length; i++)
            {
                opts[i] = String.Format("option {0}", i+1);
            }
            ConsoleMenu.ChoicePrompt(
                "Title of Menu",
                opts,
                "Choose an option: ");
            Console.WriteLine("Have a good day!");
        }
    }
}
