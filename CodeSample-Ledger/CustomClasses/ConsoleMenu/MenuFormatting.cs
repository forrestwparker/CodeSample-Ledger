using System;

namespace CodeSample_Ledger.ConsoleMenus
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
        private static void WriteTitle(string Title)
        {
            if (!String.IsNullOrEmpty(Title))
            {
                Console.WriteLine("{0}\n{1}",
                    Title,
                    new String('-', Title.Length));
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
        private static void WriteChoices(string[] Choices)
        {
            int MaxLeadingSpaces = (int)Math.Log10((double)Choices.Length);
            for (int i = 0; i < Choices.Length; i++)
            {
                int LeadingSpaces = MaxLeadingSpaces - (int)Math.Log10((double)i);
                Console.WriteLine("{0}{1}) {2}",
                    new String(' ', LeadingSpaces),
                    i,
                    Choices[i]);
            }
            Console.WriteLine();
        }

        // Writes a prompt and returns user input.
        // UI varies by function passed to WriteAction.
        private static void WritePrompt<T>(
            string Query,
            Action<string> WriteAction,
            Func<T, bool>[] Constraints,
            out T TypedUserResponse
            )
        {
            WriteAction(Query);
            Console.ReadLine();
        }

        // Writes a choice menu and returns user input.
        // UI varies by function passed to WriteAction.
        private static void WriteChoiceMenu(
            string Title,
            string[] Options,
            string Query,
            Action<string> WriteAction,
            out int TypedUserResponse
            )
        {
            var Constraints = new Func<int, bool>[2] { x => x >= 1, x => x <= Options.Length };
            WriteTitle(Title);
            WriteChoices(Options);
            WritePrompt(Query, WriteAction, Constraints, out TypedUserResponse);
        }
    }
}
