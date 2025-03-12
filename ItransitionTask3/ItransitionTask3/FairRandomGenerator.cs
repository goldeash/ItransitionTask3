using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NonTransitiveDiceGame
{
    public class FairRandomGenerator
    {
        private const int KeySize = 32;

        public (int Result, string Hmac, byte[] Key) GenerateFairRandom(int min, int max)
        {
            byte[] key = new byte[KeySize];
            RandomNumberGenerator.Fill(key);
            int computerNumber = RandomNumberGenerator.GetInt32(min, max);

            byte[] message = BitConverter.GetBytes(computerNumber);
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(message);
                string hmacHex = BitConverter.ToString(hash).Replace("-", "");

                return (computerNumber, hmacHex, key);
            }
        }

        public int CalculateResult(int computerNumber, int userNumber, int modulus)
        {
            return (computerNumber + userNumber) % modulus;
        }
    }
}
