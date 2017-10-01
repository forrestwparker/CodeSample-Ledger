using System;

namespace CodeSample_Ledger.ConsoleMenu
{
    public static partial class ConsoleMenu
    {
        //
        // Menu formatting methods.
        //

        // Writes a menu title formatted with dashes underneath.
        // E.g.
        // Title of menu
        // -------------
        private static void WriteTitle(string title)
        {
            if (!String.IsNullOrEmpty(title))
            {
                Console.WriteLine("{0}\n{1}",
                    title,
                    new String('-', title.Length));
                Console.WriteLine();
            }
        }

        // Writes a menu of choices.
        // E.g.
        //
        //  1) Option 1
        //  ...
        //  9) Option 9
        // 10) Option 10
        private static void WriteOptions(string[] options)
        {
            int maxLeadingSpaces = (int)Math.Log10((double)options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                int leadingSpaces = maxLeadingSpaces - (int)Math.Log10((double)i);
                Console.WriteLine("{0}{1}) {2}",
                    new String(' ', leadingSpaces),
                    i,
                    options[i]);
            }
            Console.WriteLine();
        }

        // Writes a prompt and returns user input.
        // UI varies by function passed to WriteAction.
        private static T WritePrompt<T>(
            string query,
            Constraint<T>[] constraints,
            Action<string> writeAction
            )
        {
            writeAction(query);
            Console.ReadLine();
        }

        // Writes a choice menu and returns user input.
        // UI varies by function passed to WriteAction.
        private static int WriteChoiceMenu(
            string title,
            string[] options,
            string query,
            Action<string> writeAction
            )
        {
            var constraints = new Constraint<int>[1];
            constraints[0] = new Constraint<int>(
                new Func<int, bool>[2] {
                    x => x >= 1,
                    x => x <= options.Length},
                String.Format("Please enter an integer between 1 and {0}.", options.Length)
                );
            WriteTitle(title);
            WriteOptions(options);
            return WritePrompt(query, constraints, writeAction);
        }
    }
}
