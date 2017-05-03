using System;
using System.Collections.Generic;
using System.Linq;

namespace DataReaderWriter
{
    static class Randomizer
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomSentense()
        {
            var words = new List<string> { "quick ", "red ", "grey ", "brown ", "fox ", "rabbit ", "jumps ", "over ", "the ", "lazy ", "spotted ", "dog ", "penguin " };
            string s = String.Empty;
            var count = 1 + random.Next(words.Count);
            for (var i = 0; i < count; i++)
                s += words[random.Next(words.Count)];
            return s.TrimEnd();
        }

        public static string RandomString()
        {
            return RandomString(1 + random.Next(255));
        }
        public static int RandomInteger(int max)
        {
            return random.Next(max);
        }
    }
}
