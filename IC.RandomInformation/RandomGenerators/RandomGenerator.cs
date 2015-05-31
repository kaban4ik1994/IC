using System;
using System.Collections.Generic;
using IC.RandomInformation.RandomData;

namespace IC.RandomInformation.RandomGenerators
{
    public static class RandomGenerator
    {
        private static readonly Random Random = new Random();

        public static byte GetRandomByte()
        {
            return (byte)Random.Next(0, 255);
        }

        public static string GetRandomStringFromMinCountToMaxCount(int min, int max)
        {
            var result = string.Empty;
            for (var i = min; i < max; i++)
            {
                result += Convert.ToChar(GetRandomValueFromMinToMax(97, 122));
            }
            return result;
        }

        public static int GetRandomValueFromMinToMax(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static byte[] GetRandomIpAddress()
        {
            return new[] { GetRandomByte(), GetRandomByte(), GetRandomByte(), GetRandomByte() };
        }

        public static TResult GetRandomValueFromList<TResult>(IList<TResult> list)
        {
            var index = Random.Next(list.Count);
            return list[index];
        }
    }
}
