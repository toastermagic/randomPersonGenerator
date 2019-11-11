using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace easygoingsoftware.People
{
    public static class RNG
    {
        private static Random _rand = new Random((int)DateTime.Now.Ticks);
        private static MD5 hasher = MD5.Create();

        public static Random R => _rand;

        /// <summary>
        /// Gets a random element from an array of strings
        /// </summary>
        /// <param name="elements">The array of strings</param>
        /// <returns>Single random element from the array</returns>
        public static string GetRandom(this string[] elements)
        {
            return elements[_rand.Next(elements.Length)];
        }

        /// <summary>
        /// Gets a random element from a list of strings
        /// </summary>
        /// <param name="elements">The list of strings</param>
        /// <returns>Single random element from the list</returns>
        public static string GetRandom(this List<string> options)
        {
            return options[_rand.Next(options.Count)];
        }

        /// <summary>
        /// Gets random upper case letter
        /// </summary>
        /// <returns>Random upper case letter</returns>
        public static string GetRandomLetter()
        {
            return GetRandomLetter('Z');
        }

        /// <summary>
        /// Gets random upper case letter from A to the provided character
        /// </summary>
        /// <param name="notAfter">Last possible character</param>
        /// <returns>Random upper case letter</returns>
        public static string GetRandomLetter(char notAfter)
        {
            var limit = (int)notAfter - 65;

            var letter = (char)('A' + _rand.Next(0, limit));
            return letter.ToString();
        }

        /// <summary>
        /// Gets a random string of numbers, can contain leading zeros
        /// </summary>
        /// <param name="length">Length of string to return</param>
        /// <returns>String of random numbers</returns>
        public static string GetRandomNumberString(int length)
        {
            var seq = new StringBuilder();

            while (seq.Length < length)
                seq.Append(_rand.Next(0, 10).ToString());

            return seq.ToString();
        }

        /// <summary>
        /// Sets the seed for the randomizer using a byte array
        /// </summary>
        /// <param name="seedBytes">Seed</param>
        public static void SetSeed(byte[] seedBytes)
        {
            var number = new System.Numerics.BigInteger(seedBytes);
            var seed = (int)(number % int.MaxValue);
            _rand = new Random(seed);
        }

        /// <summary>
        /// Sets the seed for the randomizer using a Guid
        /// </summary>
        /// <param name="seedBytes">Seed</param>
        public static void SetSeed(Guid id)
        {
            var hex = id.ToByteArray();
            SetSeed(hex);
        }

        /// <summary>
        /// Sets the seed for the randomizer using a string
        /// </summary>
        /// <param name="seedBytes">Seed</param>
        public static void SetSeed(string seed)
        {
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(seed));
            SetSeed(hash);
        }

        /// <summary>
        /// Sets the seed for the randomizer using an integer
        /// </summary>
        /// <param name="seedBytes">Seed</param>
        public static void SetSeed(int seed) {
            _rand = new Random(seed);
        }
    }
}