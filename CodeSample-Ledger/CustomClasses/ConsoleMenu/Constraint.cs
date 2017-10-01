using CodeSample_Ledger.Extensions;
using System;

namespace CodeSample_Ledger.Menus
{
    public static partial class ConsoleMenu
    {
        // Used for associating a specific error message with
        // a collection of conditionals when calling a prompt. 
        public struct Constraint<T> {
            public readonly Func<T, bool>[] conditionals;
            public readonly string errorMessage;

            public Constraint(Func<T, bool>[] conditionals, string errorMessage)
            {
                this.conditionals = conditionals;
                this.errorMessage = errorMessage;
            }

            // Evaluates each conditional against an input value.
            // If this.conditionals is not null and not empty,
            // and any conditional evaluates false,
            // outs this.errorMessage and returns false.
            // Otherwise, outs a null string and returns true.
            public bool CheckConstraint(T value, out string errorMessage)
            {
                if (!conditionals.IsNullOrEmpty()) {
                    foreach (var condition in conditionals)
                    {
                        if (!condition(value))
                        {
                            errorMessage = this.errorMessage ?? defaultNullOrEmptyConstraintErrorMessage;
                            return false;
                        }
                    }
                }
                errorMessage = null;
                return true;
            }
        }
    }
}
