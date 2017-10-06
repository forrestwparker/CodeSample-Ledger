using System;
using System.Collections.Generic;

namespace CodeSample_Ledger.ConsoleUI
{
    // Provides simple menu UI.
    public class Menu
    {
        //
        // Class constructors
        //

        public Menu()
        {
            prompt.text = defaultPromptText;
            prompt.tryParser = Int32.TryParse;
        }

        //
        // Class properties
        //

        // Menu title.
        public string title = null;

        // Menu options.
        private readonly List<MenuOption> options = new List<MenuOption>();
        public int numberOfOptions { get { return options.Count; } }

        // Used to determine if the constraint in prompt needs updating.
        private int previousNumberOfOptions = default(int);

        // User prompt.
        private readonly Prompt<int> prompt = new Prompt<int>();
        public string promptText
        {
            get { return prompt.text; }
            set { prompt.text = value ?? defaultPromptText; }
        }
        public Action<string> promptDisplayAction
        {
            get { return prompt.displayAction; }
            set { prompt.displayAction = value; }
        }

        // Default promptText
        private const string defaultPromptText = "Choose an option: ";

        // Constraint failure error message.
        private const string defaultConstraintFailureErrorMessage = "Must be an integer between 1 and {0}.";

        //
        // Class Methods
        //

        // Add an option to menu.
        // Prevents empty or null strings in options.
        public void AddMenuOption(string text, Action action)
        {
            if (!String.IsNullOrEmpty(text))
            {
                options.Add(new MenuOption(text, action));
            }
        }

        // Attempts to retrieves option text and action from menu.
        public bool GetMenuOption(int index, out string text, out Action action)
        {
            if (0 <= index && index < options.Count)
            {
                text = options[index].text;
                action = options[index].action;
                return true;
            }
            else
            {
                text = default(string);
                action = default(Action);
                return false;
            }
        }

        // Attempts to remove an option from menu.
        public bool RemoveMenuOption(int index)
        {
            if (0 <= index && index < options.Count)
            {
                options.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Remove all options from menu.
        public void RemoveAllMenuOptions()
        {
            options.Clear();
        }

        // If options exist...
        // - Sets correct constraint on prompt;
        // - Shows the title (if not null);
        // - Shows the options; 
        // - Prompts the user for a response;
        // - Performs the selected action based on user response;
        // - Returns the user selected value.
        // 
        // Otherwise no options exist, so displays nothing and returns 0.
        public int Show()
        {
            if (options.Count > 0)
            {
                if (options.Count != previousNumberOfOptions)
                {
                    previousNumberOfOptions = options.Count;
                    SetPromptConstraint();
                }
                ShowTitle();
                ShowOptions();
                int selection = prompt.Show();
                Console.WriteLine();
                options[selection-1].action?.Invoke();
                return selection;
            }
            else
            {
                return default(int);
            }
        }

        // Shows a menu title with a row of
        // dashes underneath with the same width.
        //
        // E.g.
        // Title Of Menu
        // -------------
        private void ShowTitle()
        {
            if (!String.IsNullOrEmpty(title))
            {
                Console.WriteLine("{0}\n{1}",
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
        private void ShowOptions()
        {
            int maxLeadingSpaces = (int)Math.Log10(options.Count);
            for (int i = 0; i < options.Count; i++)
            {
                // Mathematically, leadingSpaces will always be non-negative.
                int leadingSpaces = maxLeadingSpaces - (int)Math.Log10(i + 1);
                Console.WriteLine("{0}{1}) {2}",
                                  new String(' ', leadingSpaces),
                                  i + 1,
                                  options[i].text);
            }
            Console.WriteLine();
        }

        private void SetPromptConstraint()
        {
            var constraint = new Constraint<int>();
            constraint.constraintFailureErrorMessage = String.Format(
                defaultConstraintFailureErrorMessage,
                options.Count);
            constraint.AddConditional(x => x >= 1);
            constraint.AddConditional(x => x <= options.Count);
            prompt.RemoveAllConstraints();
            prompt.AddConstraint(constraint);
        }

        //
        // Nested classes, structs, enums
        //

        // Used to associate options in menu with functions.
        private struct MenuOption
        {
            //
            // Struct Properties
            //

            // Text to show as an option in menu.
            public readonly string text;

            // Function to run if option is selected.
            public readonly Action action;

            //
            // Struct Constructors
            //

            public MenuOption(string text, Action action)
            {
                this.text = text;
                this.action = action;
            }
        }
    }
}
