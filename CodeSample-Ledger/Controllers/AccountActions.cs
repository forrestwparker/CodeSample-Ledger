using CodeSample_Ledger.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.Controllers
{
    public static class AccountActions
    {
        public static void MainMenu()
        {
            bool Quit = false;
            do
            {
                string Selection = ConsoleView.ChoiceMenuPromptInline(
                    "Main Menu",
                    new string[]
                    {
                        "Login",
                        "Create an account",
                        "Quit"
                    },
                    "Make a selection:"
                    );
                switch ()
            } while (!Quit);
        }
        /*
        private static void Login()
        {
            using (var context = new LedgerContext())
            {
                if 
            }
        }
        */
        public static void CreateAccount()
        {
            string Username;
            bool UserName
            do
            {
                Username = ConsoleView.PromptNewline("What username would you like to create?");
                IsInLedger = Is
                if (IsInLedger(Username))
                {

                }
            }
        }
        
    }
}
