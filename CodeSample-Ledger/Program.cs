using CodeSample_Ledger.ConsoleUI;
using CodeSample_Ledger.Controllers;
using System;

namespace CodeSample_Ledger
{
    class Program
    {
        static void Main(string[] args)
        {
            new LoginController().Run();
        }
    }
}
