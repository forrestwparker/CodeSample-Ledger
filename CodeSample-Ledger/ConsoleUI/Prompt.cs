using System;
using System.ComponentModel;

namespace CodeSample_Ledger.ConsoleUI
{
    public class Prompt<T>
    {
        //
        // Class properties
        //

        // Text to show when prompting a user for a response.
        private string _text = defaultText;
        public string text
        {
            get { return _text; }
            set { _text = value ?? defaultText; }
        }

        // Default text.
        public const string defaultText = "Type something: ";

        // Function that determines how a prompt will appear.
        private Action<string> _displayAction = defaultDisplayActionInline;
        public Action<string> displayAction
        {
            get { return _displayAction; }
            set { _displayAction = value ?? defaultDisplayActionInline; }
        }

        // Default displayAction functions.
        public static readonly Action<string> defaultDisplayActionInline = Console.Write;
        public static readonly Action<string> defaultDisplayActionNewline = Console.WriteLine;

        // Function used to convert user response to type T.
        public delegate bool TryParser(string stringValue, out T typedValue);
        private TryParser _tryParser = TryParse;
        public TryParser tryParser
        {
            get { return _tryParser; }
            set { _tryParser = value ?? TryParse; }
        }

        // Error message to display if a user response cannot be converted
        // into type T by tryParser.
        private string _invalidUserResponseTypeErrorMessage = defaultInvalidUserResponseTypeErrorMessage;
        public string invalidUserResponseTypeErrorMessage
        {
            get { return _invalidUserResponseTypeErrorMessage; }
            set
            {
                _invalidUserResponseTypeErrorMessage = value ??
                                                       defaultInvalidUserResponseTypeErrorMessage;
            }
        }

        // Default type conversion error message.
        public const string defaultInvalidUserResponseTypeErrorMessage = "Invalid response type.";

        // List of Constraints that a user response must satisfy to be valid.
        // Each Constraint contains its own settable error message.
        private Constraint<T>[] _constraints = new Constraint<T>[0];
        public Constraint<T>[] constraints
        {
            get { return _constraints; }
            set { _constraints = value ?? new Constraint<T>[0]; }
        }


        //
        // Class methods
        //

        // Displays the prompt and returns user response converted to type T.
        public T Show()
        {
            T typedUserResponse;
            // While user response is invalid, display
            // corresponding errorMessage and query again.
            do { displayAction(text); } while (!GetValidTypedUserResponse(out typedUserResponse));
            return typedUserResponse;
        }

        // Returns true and outs typedUserResponse if the user response:
        // - Converts correctly; and
        // - Satisfies all constraints.
        // Returns false otherwise and outs appropriate errorMessage.
        private bool GetValidTypedUserResponse(out T typedUserResponse)
        {
            // If userResponse cannot be parsed to correct type...
            if (!tryParser(Console.ReadLine(), out typedUserResponse))
            {
                Console.WriteLine("Error: {0}\n", invalidUserResponseTypeErrorMessage);
                return false;
            }
            // Otherwise, check all constraints (in order).
            // If one fails, return false and out the errorMessage.
            // Else, all have passed so return true.
            else
            {
                for (int i = 0; i < constraints.Length; i++)
                {
                    if (!constraints[i].Check(typedUserResponse))
                    {
                        Console.WriteLine("Error: {0}\n", constraints[i].constraintFailureErrorMessage);
                        return false;
                    }
                }
                return true;
            }
        }

        // Default TryParser when no others are supplied at time of instantiation.
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

        //
        // Class constructors
        //

        public Prompt() { }

        public Prompt(string text, int numberOfConstraints = 0)
        {
            this.text = text;
            this.constraints = new Constraint<T>[numberOfConstraints];
        }
    }
}
