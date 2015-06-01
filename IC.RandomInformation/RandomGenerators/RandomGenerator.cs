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

        public static string GetRandomPassword(int min, int max)
        {
            var result = string.Empty;
            for (var i = min; i < max; i++)
            {
                switch (GetRandomValueFromMinToMax(0, 3))
                {
                    case 0:
                        result += Convert.ToChar(GetRandomValueFromMinToMax(97, 122));
                        break;
                    case 1:
                        result += Convert.ToChar(GetRandomValueFromMinToMax(65, 90));
                        break;
                    default:
                        result += Convert.ToChar(GetRandomValueFromMinToMax(48, 57));
                        break;
                }
            }
            return result;
        }

        public static int GetRandomValueFromMinToMax(int min, int max)
        {
            return Random.Next(min, max);
        }

        public static string GetRandomSpecialtyName()
        {
            return GetRandomValueFromList(SpecialtyData.SpecialtyNames);
        }

        public static string GetRandomFirstName()
        {
            return GetRandomValueFromList(StudentData.FirstNames);
        }

        public static string GetRandomMiddleName()
        {
            return GetRandomValueFromList(StudentData.Patronymics);
        }

        public static string GetRandomSecondName()
        {
            return GetRandomValueFromList(StudentData.SecondNames);
        }

        public static byte[] GetRandomIpAddress()
        {
            return new[] { GetRandomByte(), GetRandomByte(), GetRandomByte(), GetRandomByte() };
        }

        public static TResult GetRandomValueFromList<TResult>(IList<TResult> list)
        {
            var index = Random.Next(0, list.Count);
            return list[index];
        }
    }
}
