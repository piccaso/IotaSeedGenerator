using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IotaSeedGenerator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var validCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ9".ToList();
            const int len = 81;
            var cnt = 10;
            using (var rng = new RNGCryptoServiceProvider())
            {
                while (cnt > 0)
                {
                    var sb = new StringBuilder(len);
                    while (sb.Length < len)
                    {
                        var nextUint = rng.GetNextUint();
                        var nextIndex = (int) (nextUint % validCharacters.Count);
                        var nextCharacter = validCharacters[nextIndex];

                        if (validCharacters.Contains(nextCharacter)) sb.Append(nextCharacter);
                    }

                    var seed = sb.ToString();
                    var diversity = validCharacters.Count(character => seed.Contains(character));

                    if (diversity < validCharacters.Count || seed.Length != len) continue;

                    Console.WriteLine(seed);
                    cnt--;
                }
            }
        }
    }

    public static class RngExt
    {
        public static uint GetNextUint(this RandomNumberGenerator r)
        {
            var buffer = new byte[sizeof(uint)];
            r.GetBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }
    }
}
