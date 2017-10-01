using System;
using static System.Console;

namespace CodeSample_Ledger.Menus
{
    public static partial class ConsoleMenu
    {
        //
        // Display methods.
        //

        // Displays a menu title with a row of
        // dashes underneath with the same width.
        //
        // E.g.
        // Title Of Menu
        // -------------
        private static void DisplayTitle(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                WriteLine("{0}\n{1}",
                    title,
                    new String('-', title.Length));
            }
        }

        // Displays a menu of options with right-aligned option numbering.
        // E.g.
        //
        //  1) Option 1
        //  ...
        //  9) Option 9
        // 10) Option 10
        private static void DisplayOptions(string[] options)
        {
            int maxLeadingSpaces = (int)Math.Log10(options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                // Mathematically, leadingSpaces will always be non-negative.
                int leadingSpaces = maxLeadingSpaces - (int)Math.Log10(i+1);
                WriteLine("{0}{1}) {2}",
                    new String(' ', leadingSpaces),
                    i+1,
                    options[i]);
            }
            WriteLine();
        }

        // Displays a prompt and returns user input.
        // UI varies by method passed to displayAction.
        private static T DisplayPrompt<T>(
            string query,
            Constraint<T>[] constraints,
            Action<string> displayAction,
            TryParser<T> tryParser
            )
        {
            T typedUserResponse;
            string errorMessage;
            displayAction(query);
            // While user response is invalid, display
            // corresponding errorMessage and query again.
            while (
                !ProcessUserResponse(
                    ReadLine(),
                    tryParser,
                    constraints,
                    out typedUserResponse,
                    out errorMessage
                    )
                )
            {
                WriteLine("Error: {0}", errorMessage);
                WriteLine();
                displayAction(query);
            }
            WriteLine();
            return typedUserResponse;
        }

        // Displays a choice menu and returns user input.
        // UI varies by method passed to displayAction.
        private static int DisplayChoicePrompt(
            string title,
            string[] options,
            string query,
            Action<string> displayAction
            )
        {
            var constraints = new Constraint<int>[1];

            // User must respond with a valid option number,
            // i.e. an integer between 1 and options.Length (inclusive).
            constraints[0] = new Constraint<int>(
                new Func<int, bool>[2] {
                    x => x >= 1,
                    x => x <= options.Length},
                String.Format("Please enter an integer between 1 and {0}.", options.Length)
                );

            DisplayTitle(title);
            DisplayOptions(options);
            return DisplayPrompt(query, constraints, displayAction, Int32.TryParse);
        }
    }
}
