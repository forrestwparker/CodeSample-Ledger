using CodeSample_Ledger.ConsoleUI;
using CodeSample_Ledger.Models;
using CodeSample_Ledger.DAL;
using System;

namespace CodeSample_Ledger.Controllers
{
    // Provdes UI and functionality for making transactions with an authenticated account.
    public class AccountController
    {
        //
        // Class constructors
        //

        // Cannot be instantiated with a default constructor.
        private AccountController()
        {
            mainMenu = ConfigureMainMenu();
        }

        // Only constructor for instantiation.
        public AccountController(Account account) : this()
        {
            this.account = account;
        }

        //
        // Class properties
        //
        
        // Main menu.
        private readonly Menu mainMenu;

        // Associated verified account.
        private readonly Account account;

        //
        // Class methods
        //

        // Configures the main menu.
        // Called by default constructor.
        private Menu ConfigureMainMenu()
        {
            var menu = new Menu();
            menu.title = "Account Menu";
            menu.AddMenuOption("Make a deposit", TryDeposit);
            menu.AddMenuOption("Make a withdrawal", TryWithdrawal);
            menu.AddMenuOption("See transaction history", SeeTransactionHistory);
            menu.AddMenuOption("Log Out", null);
            return menu;
        }

        // Runs controller.
        public void Run()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("Account: {0}", account.username);
                Console.WriteLine("Balance: ${0}", account.balance);
                Console.WriteLine();
            } while (mainMenu.Show() < mainMenu.numberOfOptions);
        }

        // Sets transaction constriants on prompts.
        private void SetTransactionPromptConstraints(Prompt<decimal> prompt, string typeOfTransaction)
        {
            var constraints = new Constraint<decimal>[3];
            constraints[0] = new Constraint<decimal>();
            constraints[0].AddConditional(x => x >= 0);
            constraints[0].constraintFailureErrorMessage = 
                String.Format("Amount of {0} must be non-negative.",
                              typeOfTransaction.ToLower());
            constraints[1] = new Constraint<decimal>();
            constraints[1].AddConditional(x => ValidDollarAmount(x));
            constraints[1].constraintFailureErrorMessage = 
                String.Format("Amount of {0} cannot include a fraction of a cent.",
                              typeOfTransaction.ToLower());
            constraints[2] = new Constraint<decimal>();
            // $1,000,000,000,000,000 seems like a good maximum value to allow on transactions.
            constraints[2].AddConditional(x => x <= (decimal)Math.Pow(10, 15));
            constraints[2].constraintFailureErrorMessage = 
                String.Format("Amount of {0} cannot exceed $1,000,000,000,000,000.",
                              typeOfTransaction.ToLower());
            prompt.AddConstraint(constraints);
        }

        // Attempts to make a deposit.
        private void TryDeposit()
        {
            Console.WriteLine("How much would you like to deposit?");
            Console.WriteLine("Enter a blank or zero amount to cancel.");
            var prompt = new Prompt<decimal>();
            prompt.text = "$";
            prompt.allowBlankResponse = true;
            SetTransactionPromptConstraints(prompt, "deposit");
            decimal amount = prompt.Show();
            if (amount != default(decimal)) {
                TransactionAccess.Deposit(account, amount);
                Console.WriteLine("Deposit complete.");
                PressEnter();
            }
            else
            {
                Console.WriteLine("Deposit cancelled.");
                PressEnter();
            }
        }

        // Attempts to make a withdrawal.
        private void TryWithdrawal()
        {
        }

        // Displays transaction history.
        private void SeeTransactionHistory()
        {

        }

        // Checks that the user entered in a valid amount of money.
        // I.e. No more than two decimal places.
        private bool ValidDollarAmount(decimal x)
        {
            decimal integral = Math.Truncate(x);
            var fraction = x - integral;
            return fraction == (decimal)Math.Truncate(100 * fraction);
        }

        // Waits until user presses <Enter> key.
        private void PressEnter()
        {
            Console.WriteLine("Press <Enter> to continue.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
