using CodeSample_Ledger.ConsoleUI;
using CodeSample_Ledger.DAL;
using CodeSample_Ledger.Models;
using System;
using System.Text.RegularExpressions;

namespace CodeSample_Ledger.Controllers
{
    // Provides UI and functionality for account creation and login.
    public class LoginController
    {
        //
        // Class constructors
        //

        // Default constructor.
        public LoginController()
        {
            mainMenu = ConfigureMainMenu();
        }

        //
        // Class properties
        //

        // Main menu
        // Configured by ConfigureMainMenu method.
        private readonly Menu mainMenu;

        // Error Messages.
        private const string failedLoginErrorMessage = 
            "The given username/password combination is not correct.";
        private const string passwordResetReminderMessage = 
            "If you have forgotten your password, please contact us to verify your identity.";
        private const string improperUsernameCharactersErrorMessage = 
            "Usernames may only contain letters of the english alphabet.";
        private const string improperUsernamePasswordLengthUnformattedErrorMessage = 
            "{0} must contain between {1} and {2} characters";
        private const string improperPasswordCharactersErrorMessage =
            "Passwords may only contain numbers and letters of the english alphabet";
        private const string usernameExistsErrorMessage =
            "That username has already be taken by another user.";

        // Minimum and maximum allowable username lengths.
        // Used in creation of error message, not in length constraints.
        private const int minUsernameLength = 8;
        private const int maxUsernameLength = 16;

        // Minimum and maximum allowable password lengths.
        // Used in creation of error message, not in length constraints.
        private const int minPasswordLength = 10;
        private const int maxPasswordLength = 32;

        //
        // Class methods
        //

        // Configures the main menu.
        // Called by default constructor.
        private Menu ConfigureMainMenu()
        {
            var menu = new Menu();
            menu.title = "Main Menu";
            menu.AddMenuOption("Log in", TryLogin);
            menu.AddMenuOption("Create Account", TryCreateAccount);
            menu.AddMenuOption("Quit program", null);
            return menu;
        }

        // Prompts user for username.
        // Usernames must contain only letters from the english alphabet.
        // Usernames must between 8 and 16 characters long.
        private string PromptUsername()
        {
            var prompt = new Prompt<string>();
            prompt.text = "Username: ";
            prompt.allowBlankResponse = true;
            var constraints = new Constraint<string>[2];
            constraints[0] = new Constraint<string>();
            constraints[0].AddConditional(x => Regex.IsMatch(x, @"^[a-zA-Z]+$"));
            constraints[0].constraintFailureErrorMessage = improperUsernameCharactersErrorMessage;
            constraints[1] = new Constraint<string>();
            constraints[1].AddConditional(x => Regex.IsMatch(x, @"^.{8,16}$"));
            constraints[1].constraintFailureErrorMessage =
                String.Format(improperUsernamePasswordLengthUnformattedErrorMessage,
                              "Usernames",
                              minUsernameLength,
                              maxUsernameLength);
            prompt.AddConstraint(constraints);
            return prompt.Show();
        }

        // Prompts user for password.
        // Passwords must contain only numbers and letters from the english alphabet.
        // Passwords must be between 10 and 32 characters long.
        private string PromptPassword()
        {
            var prompt = new Prompt<string>();
            prompt.text = "Password: ";
            prompt.allowBlankResponse = true;
            var constraints = new Constraint<string>[2];
            constraints[0] = new Constraint<string>();
            constraints[0].AddConditional(x => Regex.IsMatch(x, @"^[a-zA-Z0-9]+$"));
            constraints[0].constraintFailureErrorMessage = improperPasswordCharactersErrorMessage;
            constraints[1] = new Constraint<string>();
            constraints[1].AddConditional(x => Regex.IsMatch(x, @"^.{10,32}$"));
            constraints[1].constraintFailureErrorMessage =
                String.Format(improperUsernamePasswordLengthUnformattedErrorMessage,
                              "Passwords",
                              minPasswordLength,
                              maxPasswordLength);
            prompt.AddConstraint(constraints);
            return prompt.Show();
        }

        // Runs the controller.
        public void Run()
        {
            // Continues to provide the main menu until 
            // "Quit program" is selected.
            do { Console.Clear(); } while (mainMenu.Show() < mainMenu.numberOfOptions);
        }

        // Attempts to log into an account.
        private void TryLogin()
        {
            Console.WriteLine("User Login");
            Console.WriteLine("Enter a blank username or password to cancel logging in.");
            Console.WriteLine();
            string password = null;
            string username = PromptUsername();
            // If username is empty, goes back to menu.
            // Otherwise...
            if (!String.IsNullOrEmpty(username))
            {
                password = PromptPassword();
                // If password is empty, goes back to menu.
                // Otherwise...
                if (!String.IsNullOrEmpty(password))
                {
                    Console.WriteLine();
                    Console.WriteLine("Verifying account info. Please wait.");
                    Console.WriteLine();
                    Account account;
                    // Attempts to verify account info.
                    // If valid, access is granted and an account controller 
                    // is instantiated with the account.
                    if (AccountAccess.Grant(username, password, out account))
                    {
                        new AccountController(account).Run();
                    }
                    // Otherwise an error is displayed and goes back
                    // to the menu when <Enter> key is pressed.
                    else
                    {
                        Console.WriteLine(failedLoginErrorMessage);
                        Console.WriteLine(passwordResetReminderMessage);
                        PressEnter();
                    }
                }
            }
            // If user entered a blank username or password, cancel login.
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                Console.WriteLine("Login cancelled.");
                PressEnter();
            }
        }

        // Attempts to create new account.
        private void TryCreateAccount()
        {
            Console.WriteLine("Create an account");
            Console.WriteLine("Enter a blank username or password to cancel account creation.");
            Console.WriteLine();
            string password = null;
            string username;
            bool usernameExists;
            // Prompts for a new username.
            // Repeats prompt until a blank or valid, nonexisting username is entered.
            do
            {
                username = PromptUsername();
                usernameExists = AccountAccess.CheckUsernameExists(username);
                if (usernameExists)
                {
                    Console.WriteLine(usernameExistsErrorMessage);
                    Console.WriteLine();
                }
            } while (!String.IsNullOrEmpty(username) && usernameExists);
            // If username is blank, goes back to menu.
            // Otherwise...
            if (!String.IsNullOrEmpty(username))
            {
                password = PromptPassword();
                if (!String.IsNullOrEmpty(password))
                {
                    AccountAccess.MakeAccount(username, password);
                    Console.WriteLine("Account created.");
                    PressEnter();
                }
            }
            // If user entered a blank username or password, cancel account creation.
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                Console.WriteLine("Account creation cancelled.");
                PressEnter();
            }
        }

        // Waits until user presses <Enter> key.
        private void PressEnter()
        {
            Console.WriteLine("Press <Enter> to continue.");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        }
    }
}
