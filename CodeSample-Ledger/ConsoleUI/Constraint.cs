using System;
using System.Collections.Generic;

namespace CodeSample_Ledger.ConsoleUI
{
    public class Constraint<T>
    {
        //
        // Class constructors
        //

        public Constraint() { }

        //
        // Class properties
        //

        // Conditionals.
        private readonly List<Func<T, bool>> conditionals = new List<Func<T, bool>>();
        public int numberOfConditionals { get { return conditionals.Count; } }

        // Error message.
        // Cannot be null or empty.
        private string _constraintFailureErrorMessage;
        public string constraintFailureErrorMessage
        {
            get { return _constraintFailureErrorMessage; }
            set
            {
                _constraintFailureErrorMessage = !String.IsNullOrEmpty(value) ? value :
                                                 defaultConstraintFailureErrorMessage;
            }
        }

        // Default constraint failure error message (if instantiated by non-default constructor).
        private const string defaultConstraintFailureErrorMessage = "Response failed constraint.";

        //
        // Class methods
        //

        // Add a conditional.
        // Prevents null conditionals.
        public void AddConditional(Func<T, bool> conditional)
        {
            if (conditional != null)
            {
                conditionals.Add(conditional);
            }
        }

        // Attempts to retrieve a conditional.
        public bool GetConditional(int index, out Func<T, bool> conditional)
        {
            if (0 <= index && index < conditionals.Count)
            {
                conditional = conditionals[index];
                return true;
            }
            else
            {
                conditional = default(Func<T, bool>);
                return false;
            }
        }

        // Attempts to remove a conditional.
        public bool RemoveConditional(int index)
        {
            if (0 <= index && index < conditionals.Count)
            {
                conditionals.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Remove all constraints.
        public void RemoveAllMenuOptions()
        {
            conditionals.Clear();
        }

        // Evaluates each conditional against an input of type T.
        // If any conditionals exist and evaluate false, returns false.
        // Otherwise returns true.
        public bool Check(T value)
        {
            if (conditionals.Count != 0)
            {
                foreach (var condition in conditionals)
                {
                    if (!condition.Invoke(value))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
