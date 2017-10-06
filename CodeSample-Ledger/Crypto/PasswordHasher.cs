using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample_Ledger.Crypto
{
    // Provides all the functions used to create salt and passwords.
    // The storage for hashkey and the hashing functionality should be
    // provided by dedicated hardware, but that isn't an option in this
    // code sample.
    public sealed class PasswordHasher
    {
        //
        // Class constructors
        //

        // Default constructor
        public PasswordHasher() { }

        // Constructor for use with 
        public PasswordHasher(byte[] salt)
        {
            this.salt = salt;
        }

        //
        // Class properties
        //

        // Secret key used for hashing passwords.
        // In practice, should be longer, random
        // and hashing should be performed on dedicated
        // hardware on which the hashkey is not directly
        // accessible by the computer.
        private static readonly byte[] hashkey = new byte[]
            { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        // Salt used for hashing.
        public byte[] salt = MakeSalt();

        //
        // Class methods
        //

        // Makes NaCl.
        private static byte[] MakeSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var salt = new byte[32];
            rng.GetBytes(salt);
            return salt;
        }

        // Creates hash from password (as string) using random salt.
        // Uses HMACSHA256.
        // A slower hashing function would be more suitable in practice.
        public byte[] MakeHash(string password)
        {
            byte[] passwordbytes = Encoding.ASCII.GetBytes(password);
            var saltedPassword = new byte[salt.Length + passwordbytes.Length];
            for (var i = 0; i < salt.Length; i++)
            {
                saltedPassword[i] = salt[i];
            }
            for (var i = 0; i < passwordbytes.Length; i++)
            {
                saltedPassword[salt.Length+i] = passwordbytes[i];
            }
            var hmac = new HMACSHA256(hashkey);
            return hmac.ComputeHash(saltedPassword);
        }
    }
}
