using System;

namespace CodeSample_Ledger.ConsoleMenu
{

    // This class provides all the menu functionality used by the ledger.
    // There are three categories of methods:
    // - Menu methods
    // - (private) menu formatting methods
    // - User response processing and return type methods
    public static partial class ConsoleMenu
    {
        //
        // Prompt methods.
        //

        // Creates a prompt and returns a double
        // from the same line subject to the given constraints.
        public static double PromptInline(string query, Constraint<double>[] constraints)
        {
            return WritePrompt(query, constraints, Console.Write);
        }

        // Creates a prompt and returns an int
        // from the same line subject to the given constraints.
        public static int PromptInline(string query, Constraint<int>[] constraints)
        {
            return WritePrompt(query, constraints, Console.Write);
        }

        // Creates a prompt and returns a string
        // from the same line subject to the given constraints.
        public static string PromptInline(string query, Constraint<string>[] constraints)
        {
            return WritePrompt(query, constraints, Console.Write);
        }

        // Creates a prompt and returns a double
        // from the same line subject to the given constraints.
        public static double PromptNewline(string query, Constraint<double>[] constraints)
        {
            return WritePrompt(query, constraints, Console.WriteLine);
        }

        // Creates a prompt and returns an int
        // from the same line subject to the given constraints.
        public static int PromptNewline(string query, Constraint<int>[] constraints)
        {
            return WritePrompt(query, constraints, Console.WriteLine);
        }

        // Creates a prompt and returns a string
        // from the same line subject to the given constraints.
        public static string PromptNewline(string query, Constraint<string>[] constraints)
        {
            return WritePrompt(query, constraints, Console.WriteLine);
        }
        //
        // Choice methods.
        //

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Returns an int of the user's response from the same line as the prompt.
        public static int ChoiceMenuPromptInline(string[] options, string query)
        {
            return WriteChoiceMenu(null, options, query, Console.Write);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Returns an int of the user's response from the same line as the prompt.
        public static int ChoiceMenuPromptInline(string title, string[] options, string query)
        {
            return WriteChoiceMenu(title, options, query, Console.Write);
        }

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Returns an int of the user's response from the next line after the prompt.
        public static int ChoiceMenuPromptNewline(string[] options, string query)
        {
            return WriteChoiceMenu(null, options, query, Console.WriteLine);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Returns an int of the user's response from the next line after the prompt.
        public static int ChoicesMenuPromptNewline(string title, string[] options, string query)
        {
            return WriteChoiceMenu(title, options, query, Console.WriteLine);
        }
    }
}