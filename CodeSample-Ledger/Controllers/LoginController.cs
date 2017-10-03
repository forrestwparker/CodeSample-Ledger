using CodeSample_Ledger.ConsoleUI;
using CodeSample_Ledger.DAL;
using CodeSample_Ledger.Models;
using System;

namespace CodeSample_Ledger.Controllers
{
    public static class LoginController
    {
        public static void Run()
        {
            // Configure main menu
            var mainMenu = new Menu();
            mainMenu.title = "Main Menu";
            mainMenu.options = new string[]
            {
                "Log in",
                "Create account",
                "Quit"
            };

            // Show main menu and do appropriate
            // action based on user selection.
            int selection;
            do
            {
                Console.Clear();
                selection = mainMenu.Show();
                Console.WriteLine();
                switch (selection)
                {
                    case 1:
                        AttemptLogin();
                        break;
                    case 2:
                        AttemptCreateAccount();
                        break;
                    default:
                        break;
                }
            } while (selection < mainMenu.options.Length);
        }

        private static void AttemptLogin()
        {
            var prompt = new Prompt<string>();
            prompt.text = "Username: ";
            string username = prompt.Show();
            prompt.text = "Password: ";
            string password = prompt.Show();
            Account account;
            if (AccountActions.Login(username, password, out account))
            {
                AccountController.Run(account);
            }
            else
            {
                Console.WriteLine("The given username/password combination is not correct.");
                Console.WriteLine("If you have forgotten your password, please contact us to verify your identity.");
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        private static void AttemptCreateAccount()
        {
            //var 
        }
    }
}
