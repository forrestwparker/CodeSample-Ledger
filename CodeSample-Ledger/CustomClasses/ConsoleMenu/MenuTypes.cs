﻿using System;
using static System.Console;

namespace CodeSample_Ledger.Menus
{
    // This class provides all the menu functionality used by the ledger.
    // There are three categories of methods:
    // - Menu methods
    // - (private) menu formatting methods
    // - User response processing

    public static partial class ConsoleMenu
    {       
        //
        // Prompt overloads.
        //

        // Creates a prompt and returns a double
        // from the same line subject to the given constraints.
        public static double PromptDouble(string query, Constraint<double>[] constraints)
        {
            return DisplayPrompt(query, constraints, Write, Double.TryParse);
        }

        // Creates a prompt and returns an int
        // from the same line subject to the given constraints.
        public static int PromptInt(string query, Constraint<int>[] constraints)
        {
            return DisplayPrompt(query, constraints, Write, Int32.TryParse);
        }

        // Creates a prompt and returns a string
        // from the same line subject to the given constraints.
        public static string PromptString(string query, Constraint<string>[] constraints)
        {
            return DisplayPrompt(query, constraints, Write, DummyStringTryParse);
        }

        //
        // PromptLine overloads.
        //

        // Creates a prompt and returns a double
        // from the same line subject to the given constraints.
        public static double PromptLineDouble(string query, Constraint<double>[] constraints)
        {
            return DisplayPrompt(query, constraints, WriteLine, Double.TryParse);
        }

        // Creates a prompt and returns an int
        // from the same line subject to the given constraints.
        public static int PromptLineInt(string query, Constraint<int>[] constraints)
        {
            return DisplayPrompt(query, constraints, WriteLine, Int32.TryParse);
        }

        // Creates a prompt and returns a string
        // from the same line subject to the given constraints.
        public static string PromptLineString(string query, Constraint<string>[] constraints)
        {
            return DisplayPrompt(query, constraints, WriteLine, DummyStringTryParse);
        }

        //
        // ChoicePrompt overloads.
        //

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Returns an int of the user's response from the same line as the prompt.
        public static int ChoicePrompt(string[] options, string query)
        {
            return DisplayChoicePrompt(null, options, query, Write);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Returns an int of the user's response from the same line as the prompt.
        public static int ChoicePrompt(string title, string[] options, string query)
        {
            return DisplayChoicePrompt(title, options, query, Write);
        }

        //
        // ChoicePromptLine overloads.
        //

        // Creates a menu of options that:
        // - Does not have a title; and
        // - Returns an int of the user's response from the next line after the prompt.
        public static int ChoicePromptLine(string[] options, string query)
        {
            return DisplayChoicePrompt(null, options, query, WriteLine);
        }

        // Creates a menu of options that:
        // - Has a title; and
        // - Returns an int of the user's response from the next line after the prompt.
        public static int ChoicePromptLine(string title, string[] options, string query)
        {
            return DisplayChoicePrompt(title, options, query, WriteLine);
        }
    }
}