using System;

namespace CodeSample_Ledger.ConsoleMenus
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

        // Creates a prompt and outs a typed user response from the same line.
        public static void PromptInline<T>(string Query, out T TypedUserResponse)
        { 
            WritePrompt(Query, Console.Write, null, out TypedUserResponse);
        }

        // Creates a prompt and outs a typed user response
        // from the same line subject to the given constraints.
        public static void PromptInline<T>(string Query, Func<T, bool>[] Constraints, out T TypedUserResponse)
        {
            WritePrompt(Query, Console.Write, Constraints, out TypedUserResponse);
        }

        // Creates a prompt and outs a typed user response from the next line.
        public static void PromptNewline<T>(string Query, out T TypedUserResponse)
        {
            WritePrompt(Query, Console.WriteLine, null, out TypedUserResponse);
        }

        // Creates a prompt and outs a typed user response
        // from the next line subject to the given constraints.
        public static void PromptNewline<T>(string Query, Func<T,bool>[] Constraints, out T TypedUserResponse)
        {
            WritePrompt(Query, Console.WriteLine, Constraints, out TypedUserResponse);
        }

        //
        // Choice methods.
        //

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Outs an int of the user's response from the same line as the prompt.
        public static void ChoiceMenuPromptInline(string[] Choices, string Query, out int TypedUserResponse)
        {
            WriteChoiceMenu(null, Choices, Query, Console.Write, out TypedUserResponse);
        }

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Outs an int of the user's response from the next line after the prompt.
        public static void ChoiceMenuPromptNewline(string[] Choices, string Query, out int TypedUserResponse)
        {
            WriteChoiceMenu(null, Choices, Query, Console.WriteLine, out TypedUserResponse);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Outs an int of the user's response from the same line as the prompt.
        public static void ChoiceMenuPromptInline(string Title, string[] Choices, string Query, out int TypedUserResponse)
        {
            WriteChoiceMenu(Title, Choices, Query, Console.Write, out TypedUserResponse);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Outs an int of the user's response from the next line after the prompt.
        public static void ChoicesMenuPromptNewline(string Title, string[] Choices, string Query, out int TypedUserResponse)
        {
            WriteChoiceMenu(Title, Choices, Query, Console.WriteLine, out TypedUserResponse);
        }
    }
}