using System;

namespace CodeSample_Ledger
{
    public static class Menu
    {
        // This class provides all menu functionality for the ledger.

        // Menu types

        public int Choice(String Title, String[] Choices, String Prompt)
        {
            if (!String.IsNullOrEmpty(Title))
            {
                Console.WriteLine(Title);
                Console.WriteLine();
            }
            // Include code for better formatting
            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine("{0}) {1}", i, Choices[i]);
            }
            string Input;
            do
            {
                Console.WriteLine();
                Console.Write(Prompt);
                Input = Console.ReadLine();
            } while (ExpectInt(Input));
            return 1;
        }

        public int Choice(String[] Choices, String Prompt)
        {
            return Choice(null, Choices, Prompt);
        }

        public bool Prompt()

        // Return type parsing

        public bool ExpectInt(string Input)
        {
            TryParse(Input,)
        }

        public bool ExpectBool(string Input)
        {

        }
    }
}