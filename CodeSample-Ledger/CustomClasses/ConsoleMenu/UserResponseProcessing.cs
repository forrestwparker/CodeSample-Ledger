using CodeSample_Ledger.Extensions;
using System;

namespace CodeSample_Ledger.ConsoleMenus
{
    public static partial class ConsoleMenu
    {
        //
        // User response processing methods.
        //

        // Used internally to pass generic TryParse functions to ProcessUserResponse method below
        private delegate bool TryParser<T>(string Input, out T Output);

        // Used internally to:
        // - Convert a user response to a prompt into the correct return type; and
        // - Ensure the user response meets all constraints.
        // Returns true if the user response is of correct type and meets all constraints.
        // Returns false otherwise.
        private static bool ProcessUserResponse<T>(
            string UserResponse, TryParser<T> TryParse, Func<T, bool>[] Constraints, out T TypedUserResponse
            )
        {
            if (!TryParse(UserResponse, out TypedUserResponse)) { return false; }
            else if (Constraints.IsNullOrEmpty()) { return true; }
            else
            {
                for (int i = 0; i < Constraints.Length; i++)
                {
                    if (!Constraints[i](TypedUserResponse)) { return false; }
                }
                return true;
            }
        }

        // Used internally to process bool type user responses.
        private static bool ReturnBool(string UserResponse, out bool TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Boolean.TryParse, null, out TypedUserResponse);
        }

        // Used internally to process int type user responses.
        private static bool ReturnInt(string UserResponse, out int TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Int32.TryParse, null, out TypedUserResponse);
        }

        // Used internally to process int type user responses subject to constraints.
        private static bool ReturnInt(string UserResponse, Func<int, bool>[] Constraints, out int TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Int32.TryParse, Constraints, out TypedUserResponse);
        }

        // Used internally to process double type user responses.
        private static bool ReturnDouble(string UserResponse, out double TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Double.TryParse, null, out TypedUserResponse);
        }

        // Used internally to process double type user responses subject to constraints.
        private static bool ReturnDouble(string UserResponse, Func<double, bool>[] Constraints, out double TypedUserResponse)
        {
            return ProcessUserResponse(UserResponse, Double.TryParse, Constraints, out TypedUserResponse);
        }
    }
}
