using System;
using CodeSample_Ledger.Extensions;
using System.Text;

namespace CodeSample_Ledger.Views
{

    // This class provides all the menu functionality used by the ledger.
    // There are three categories of methods:
    // - Menu methods
    // - (private) menu formatting methods
    // - User response processing and return type methods
    public static class ConsoleMenu
    {
        //
        // Menu methods
        //

        // Creates a prompt and returns user input from same line.
        public static string PromptInline(string Query)
        {
            return WritePrompt(Query, Console.Write);
        }

        // Creates a prompt and returns user input from next line.
        public static string PromptNewline(string Query)
        {
            return WritePrompt(Query, Console.WriteLine);
        }

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Returns user input from same line as prompt.
        public static string ChoiceMenuPromptInline(string[] Choices, string Query)
        {
            return WriteChoiceMenu(null, Choices, Query, Console.Write);
        }

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Returns user input from next line after prompt.
        public static string ChoiceMenuPromptNewline(string[] Choices, string Query)
        {
            return WriteChoiceMenu(null, Choices, Query, Console.WriteLine);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Returns user input from same line as prompt.
        public static string ChoiceMenuPromptInline(string Title, string[] Choices, string Query)
        {
            return WriteChoiceMenu(Title, Choices, Query, Console.Write);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Returns user input from next line after prompt.
        public static string ChoicesMenuPromptNewline(string Title, string[] Choices, string Query)
        {
            return WriteChoiceMenu(Title, Choices, Query, Console.WriteLine);
        }

        //
        // Menu formatting methods
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
        private static string WritePrompt(string Query, Action<string> WriteAction)
        {
            WriteAction(Query);
            return Console.ReadLine();
        }

        // Writes a choice menu and returns user input.
        // UI varies by function passed to WriteAction.
        private static string WriteChoiceMenu(string Title, string[] Choices, string Query, Action<string> WriteAction)
        {
            WriteTitle(Title);
            WriteChoices(Choices);
            return WritePrompt(Query, WriteAction);
        }

        //
        // User response processing and return type methods.
        //

        // Used to indicate that a prompt should return a bool type.
        public static bool ReturnBool(string UserResponse, out bool TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Boolean.TryParse, null, out TypedUserResponse);
        }

        // Used to indicate that a prompt should return an int type.
        public static bool ReturnInt(string UserResponse, out int TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Int32.TryParse, null, out TypedUserResponse);
        }

        // Used to indicate that a prompt should return an int type subject to some of constraints.
        public static bool ReturnInt(string UserResponse, Func<int,bool>[] Constraints, out int TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Int32.TryParse, Constraints, out TypedUserResponse);
        }

        // Used to indicate that a prompt should return a double type.
        public static bool ReturnDouble(string UserResponse, out double TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Double.TryParse, null, out TypedUserResponse);
        }

        // Used to indicate that a prompt should return a double type subject to some constraints.
        public static bool ReturnDouble(string UserResponse, Func<double,bool>[] Constraints, out double TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Double.TryParse, Constraints, out TypedUserResponse);
        }

        // Used internally to pass generic TryParse functions to ProcessUserResponse method below
        private delegate bool TryParser<T>(string Input, out T Output);

        // Used internally to:
        // - Convert a user response to a prompt into the correct return type; and
        // - Ensure the response meets all constraints.
        private static bool ProcessUserResponse<T>(string UserResponse, TryParser<T> TryParse, Func<T,bool>[] Constraints, out T TypedUserResponse)
        {
            if (!TryParse(UserResponse, out TypedUserResponse)) { return false; }
            else if (Constraints.IsNullOrEmpty()) { return true; }
            else
            {
                for (int i = 0; i < Constraints.Length; i++)
                {
                    if (!Constraints[i](TypedUserResponse)) { return false; }
                }
                return true;
            }
        }
    }
}