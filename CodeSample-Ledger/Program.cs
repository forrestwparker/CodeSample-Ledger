using CodeSample_Ledger.Controllers;

namespace CodeSample_Ledger
{
    class Program
    {
        static void Main(string[] args)
        {
            // LoginController handles all functionality of
            // creating accounts and logging in.
            new LoginController().Run();
        }
    }
}
