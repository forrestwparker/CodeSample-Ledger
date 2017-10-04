using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodeSample_Ledger.ConsoleUI
{
    public class Prompt<T>
    {
        //
        // Class constructors
        //

        // Default constructor.
        public Prompt() { }

        // Useful when a simple user prompt without constraints is needed.
        public Prompt(string text, bool allowBlankResponse = false)
        {
            this.promptText = text;
            this.allowBlankResponse = allowBlankResponse;
        }

        //
        // Class properties
        //

        // Text to show when prompting a user for a response.
        // Cannot be null or empty.
        private string _promptText = defaultText;
        public string promptText
        {
            get { return _promptText; }
            set { _promptText = !String.IsNullOrEmpty(value) ? value : defaultText; }
        }

        // Default prompt text.
        private const string defaultText = "Type something: ";

        // Function to control formatting of the prompt text.
        // Cannot be null.
        private Action<string> _displayAction = defaultDisplayAction;
        public Action<string> displayAction
        {
            get { return _displayAction; }
            set { _displayAction = value ?? defaultDisplayAction; }
        }

        // Default displayAction.
        private static readonly Action<string> defaultDisplayAction = Console.Write;

        // Determines if a blank response is allowed.
        public bool allowBlankResponse = false;

        // Blank response error message.
        private const string blankResponseErrorMessage = "A blank response is not allowed.";

        // Function used to convert user response to type T.
        // By default, uses Prompt<T>.TryParse method.
        public delegate bool TryParser(string stringValue, out T typedValue);
        private TryParser _tryParser = TryParse;
        public TryParser tryParser
        {
            get { return _tryParser; }
            set { _tryParser = value ?? TryParse; }
        }

        // User response type conversion error message.
        // Cannot be null or empty.
        private string _invalidUserResponseTypeErrorMessage = defaultInvalidUserResponseTypeErrorMessage;
        public string invalidUserResponseTypeErrorMessage
        {
            get { return _invalidUserResponseTypeErrorMessage; }
            set
            {
                _invalidUserResponseTypeErrorMessage = String.IsNullOrEmpty(value) ? value :
                                                       defaultInvalidUserResponseTypeErrorMessage;
            }
        }

        // Default type conversion error message.
        public const string defaultInvalidUserResponseTypeErrorMessage = "Invalid response type.";

        // List of constraints that the (correctly typed) user response must satisfy to be valid.
        private readonly List<Constraint<T>> constraints = new List<Constraint<T>>();

        //
        // Class methods
        //

        // Add a constraint.
        // Prevents null constraints.
        public void AddConstraint(Constraint<T> constraint)
        {
            if (constraint != null)
            {
                constraints.Add(constraint);
            }
        }

        // Attempts to retrieve a constraint.
        public bool GetConstraint(int index, out Constraint<T> constraint)
        {
            if (0 <= index && index < constraints.Count)
            {
                constraint = constraints[index];
                return true;
            }
            else
            {
                constraint = default(Constraint<T>);
                return false;
            }
        }

        // Attempts to remove a constraint.
        public bool RemoveConstraint(int index)
        {
            if (0 <= index && index < constraints.Count)
            {
                constraints.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Remove all constraints.
        public void RemoveAllConstraints()
        {
            constraints.Clear();
        }

        // Displays the prompt and returns a user response that is:
        // - Converted to type T; and
        // - Satisfies all constraints.
        // Or returns default(T) and outs true if user response was blank
        // and was allowed to be so.
        public T Show(out bool blankUserResponse)
        {
            T typedUserResponse;
            // Repeat prompt until a valid response has been given.
            do
            {
                displayAction(promptText);
            }
            while (!GetValidTypedUserResponse(out typedUserResponse, out blankUserResponse));
            return typedUserResponse;
        }

        // Overload for use when blank responses aren't of concern,
        // primarily when they aren't allowed.
        public T Show()
        {
            return Show(out _);

        }

        // If user response is blank:
        // - Outs default(T);
        // - Outs true; and
        // - Returns allowBlankResponse.
        //
        // Else, if user response doesn't converts to type T using tryParser function, returns false.
        //
        // Else, checks contraints. If any fail, returns false.
        // Otherwise, all constraints pass and returns true.
        private bool GetValidTypedUserResponse(out T typedUserResponse, out bool blankUserResponse)
        {
            string userResponse = Console.ReadLine();
            // If userResponse is blank...
            if (String.IsNullOrEmpty(userResponse))
            {
                blankUserResponse = true;
                typedUserResponse = default(T);
                if (!allowBlankResponse)
                {
                    ShowErrorMessage(blankResponseErrorMessage);
                }
                return allowBlankResponse;
            }
            // Otherwise user response is not blank.
            blankUserResponse = false;
            // If userResponse cannot be parsed to correct type...
            // Note that typedUserResponse is set here for use
            // outside the if statement.
            if (!tryParser(userResponse, out typedUserResponse))
            {
                ShowErrorMessage(invalidUserResponseTypeErrorMessage);
                return false;
            }
            // Otherwise, user input converted correctly, so check all constraints (in order).
            // If any fails, return false and out the errorMessage.
            for (var i = 0; i < constraints.Count; i++)
            {
                if (!constraints[i].Check(typedUserResponse))
                {
                    ShowErrorMessage(constraints[i].constraintFailureErrorMessage);
                    return false;
                }
            }
            // Otherwise, everything worked correctly, so return true.
            return true;
        }

        // Shows a formatted error message.
        private void ShowErrorMessage(string errorMessage)
        {
            Console.WriteLine("Error: {0}", errorMessage);
            Console.WriteLine();
        }

        // Default TryParser.
        // Used by instantiated prompts if not replaced.
        private static bool TryParse(string stringValue, out T typedValue)
        {
            try
            {
                typedValue = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(stringValue);
                return true;
            }
            catch
            {
                typedValue = default(T);
                return false;
            }
        }
    }
}
