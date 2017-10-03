using System;

namespace CodeSample_Ledger.ConsoleUI
{
    public class Menu
    {
        //
        // Class properties
        //

        // Title
        public string title = null;

        // Options
        private string[] _options = defaultOptions;
        public string[] options
        {
            get { return _options; }
            set
            {
                if (value == null || value.Length == 0) { _options = defaultOptions; }
                else { _options = value; }
                prompt.constraints[0].constraintFailureErrorMessage = String.Format(defaultConstraintFailureErrorMessage,
                                                                                    _options.Length);
            }
        }

        public static readonly string[] defaultOptions = new string[] { "Default option 1", "Default option 2" };

        // Prompt
        private Prompt<int> prompt = new Prompt<int>();
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
        public const string defaultPromptText = "Choose an option: ";

        // Default constraint failure error message.
        private const string defaultConstraintFailureErrorMessage = "Must be an integer between 1 and {0}.";

        //
        // Class Methods
        //

        // Show.
        public int Show()
        {
            ShowTitle();
            ShowOptions();
            return prompt.Show();
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
            int maxLeadingSpaces = (int)Math.Log10(options.Length);
            for (int i = 0; i < options.Length; i++)
            {
                // Mathematically, leadingSpaces will always be non-negative.
                int leadingSpaces = maxLeadingSpaces - (int)Math.Log10(i + 1);
                Console.WriteLine("{0}{1}) {2}",
                                  new String(' ', leadingSpaces),
                                  i + 1,
                                  options[i]);
            }
            Console.WriteLine();
        }

        //
        // Class constructors
        //

        public Menu()
        {
            // Initialize Prompt
            prompt.constraints = new Constraint<int>[1];
            prompt.constraints[0] = new Constraint<int>(2);
            prompt.constraints[0].conditionals[0] = x => x >= 1;
            prompt.constraints[0].conditionals[1] = x => x <= options.Length;
            prompt.constraints[0].constraintFailureErrorMessage = String.Format(defaultConstraintFailureErrorMessage,
                                                                                options.Length);
            promptText = defaultPromptText;
        }

        public Menu(string title, string[] options = null, string promptText = null) : this()
        {
            this.title = title;
            this.options = options;
            this.prompt.text = promptText;
        }
    }
}
