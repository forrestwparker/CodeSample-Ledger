using System;

namespace CodeSample_Ledger.ConsoleUI
{
    public class Constraint<T>
    {
        //
        // class properties
        //

        // Array of conditionals that a user response to a prompt
        // must satisfy to be considered valid.
        private Func<T, bool>[] _conditionals = new Func<T, bool>[0];
        public Func<T, bool>[] conditionals
        {
            get { return _conditionals; }
            set { _conditionals = value ?? new Func<T, bool>[0]; }
        }

        // Error message to be displayed if a conditional returns false.
        private string _constraintFailureErrorMessage;
        public string constraintFailureErrorMessage
        {
            get { return _constraintFailureErrorMessage; }
            set { _constraintFailureErrorMessage = value ?? defaultConstraintFailureErrorMessage; }
        }

        // Default constraint failure error message.
        public const string defaultConstraintFailureErrorMessage = "Response failed constraint.";

        // Class constructors

        public Constraint() { }

        public Constraint(int numberOfConditionals,
                          string errorMessage = defaultConstraintFailureErrorMessage)
        {
            this.conditionals = new Func<T, bool>[numberOfConditionals];
            this.constraintFailureErrorMessage = defaultConstraintFailureErrorMessage;
        }

        //
        // Class methods
        //

        // Evaluates each conditional against an input of type T.
        // If any conditionals exist and evaluate false, returns false.
        // Otherwise returns true.
        public bool Check(T value)
        {
            if (!(conditionals.Length == 0))
            {
                foreach (var condition in conditionals)
                {
                    if (!condition(value))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
