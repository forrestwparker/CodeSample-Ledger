using CodeSample_Ledger.ConsoleUI;
using CodeSample_Ledger.DAL;
using CodeSample_Ledger.Models;
using System;

namespace CodeSample_Ledger.Controllers
{
    public class LoginController
    {
        //
        // Class properties
        //

        // Main menu
        // Configured in default constructor
        private readonly Menu mainMenu = new Menu();
        
        private const string failedLoginErrorMessage = "The given username/password combination is not correct.";
        private const string passwordResetMessage = "If you have forgotten your password, please contact us to verify your identity.";


        public void Run()
        {
            // Show main menu and do appropriate
            // action based on user selection.
            int selection;
            do
            {
                Console.Clear();
                selection = mainMenu.Show();
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

        private void AttemptLogin()
        {
            Console.WriteLine("User Login");
            var prompt = new Prompt<string>();
            prompt.promptText = "Username: ";
            string username = prompt.Show();
            prompt.promptText = "Password: ";
            string password = prompt.Show();
            Account account;
            if (AccountActions.Login(username, password, out account))
            {
                new AccountController(account).Run();
            }
            else
            {
                Console.WriteLine(failedLoginErrorMessage);
                Console.WriteLine(passwordResetMessage);
                Console.WriteLine();
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        private void AttemptCreateAccount()
        {
            //var 
        }

        //
        // Class constructors
        //

        public LoginController()
        {
            // Set main menu
            mainMenu.title = "Main Menu";
            mainMenu.options = new string[]
            {
                "Log in",
                "Create account",
                "Quit"
            };
        }
    }
}
