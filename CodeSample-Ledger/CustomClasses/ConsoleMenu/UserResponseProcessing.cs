using CodeSample_Ledger.Extensions;

namespace CodeSample_Ledger.ConsoleMenu
{
    public static partial class ConsoleMenu
    {
        // Used internally to pass TryParse functions
        // (eventually) to ProcessUserResponse method.
        private delegate bool TryParser<T>(string stringValue, out T typedValue);

        // Used as a placeholder for tryParser
        // when a prompt should return a string.
        private static bool DummyStringTryParse(string userResponse, out string typedUserResponse)
        {
            typedUserResponse = userResponse;
            return true;
        }

        // Used internally to:
        // - Convert a user response to a prompt into the correct return type; and
        // - Ensure the user response meets all constraints.
        // Returns true if the user response is of correct type and meets all constraints.
        // Returns false otherwise.
        private static bool ProcessUserResponse<T>(
            string userResponse,
            TryParser<T> tryParser,
            Constraint<T>[] constraints,
            out T typedUserResponse,
            out string errorMessage
            )
        {
            // If userResponse cannot be parsed to correct type...
            if (!tryParser(userResponse, out typedUserResponse))
            {
                errorMessage = invalidUserResponseTypeErrorMessage;
                return false;
            }

            // If there are no constraints...
            else if (constraints.IsNullOrEmpty())
            {
                errorMessage = null;
                return true;
            }

            // Otherwise, check all constraints (in order).
            // If one fails, return false and out the errorMessage.
            // Else, all have passed so return true.
            else
            {
                for (int i = 0; i < constraints.Length; i++)
                {
                    if (!constraints[i].CheckConstraint(typedUserResponse, out errorMessage)) { return false; }
                }
                errorMessage = null;
                return true;
            }
        }
    }
}
