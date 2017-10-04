using CodeSample_Ledger.ConsoleUI;
using CodeSample_Ledger.Models;
using System;

namespace CodeSample_Ledger.Controllers
{
    public class AccountController
    {
        //
        // Class properties
        //
        
        private Account account;

        private Menu mainMenu = new Menu();

        //
        // Class methods
        //

        public void Run(Account account)
        {
            int selection;
            do
            {
                Console.Clear();
                Console.WriteLine("User: {0}", account.username);
                Console.WriteLine("Balance: {0}", account.balance);
                Console.WriteLine();
                selection = mainMenu.Show();
                switch (selection)
                {
                    case 1:
                        MakeDeposit();
                        break;
                    case 2:
                        MakeWithdrawal();
                        break;
                    default:
                        break;
                }
            } while (selection < mainMenu.options.Length);
        }

        private void MakeDeposit()
        {
            var prompt = new Prompt<double>();
            prompt.displayAction = Console.WriteLine;
            prompt.promptText = "How much are you depositing?\n"+
                          "(Enter $0 to cancel)\n"+
                          "$";
            prompt.constraints = new Constraint<double>[2];
            prompt.constraints[0] = new Constraint<double>(1);
            prompt.constraints[0].conditionals[0] = x => x < 0;
            prompt.constraints[0].constraintFailureErrorMessage = "Dollar mount must be non-negative.";
            prompt.constraints[1] = new Constraint<double>(1);
            prompt.constraints[1].conditionals[1] = x => 
        }

        private void MakeWithdrawal()
        {

        }

        //
        // Class constructors
        //

        private AccountController()
        {
            mainMenu.title = "Account Menu";
            mainMenu.options = new string[]
            {
                "Make a deposit",
                "Make a withdrawal",
                "Log out"
            };

        }

        public AccountController(Account account) : this()
        {
            this.account = account;
        }
    }
}
