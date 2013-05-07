using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.Managed
{
    public static class Converter
    {
        public static int Hex2Int32(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length == 0)
            {
                throw new ArgumentNullException();
            }
            if (hex.Length > 8)
            {
                throw new ArgumentException();
            }
            char[] charArray = hex.ToCharArray();
            return InfiniteHex(charArray, 0, charArray.Length);
        }

        public static long Hex2Int64(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length == 0)
            {
                throw new ArgumentNullException();
            }
            char[] charArray = hex.ToCharArray();
            if (hex.Length <= 8)
            {
                return InfiniteHex(charArray, 0, charArray.Length);
            }
            else if (hex.Length <= 16)
            {
                long lower = InfiniteHex(charArray, 0, 8);
                long higher = InfiniteHex(charArray, 8, charArray.Length - 8);
                return (higher << 32) | lower;
            }
            else
            {
                throw new ArgumentException();
            }   
        }

        private static int InfiniteHex(char[] charArray, int start, int count)
        {
            int result = 0, length = start + count;
            for (int i = start; i < length; i++)
            {
                result <<= 4;
                char temp = charArray[i];
                if (temp >= '0' && temp <= '9')
                {
                    result |= temp - '0';
                }
                else if (temp >= 'a' && temp <= 'f')
                {
                    result |= temp - 'a' + 10;
                }
                else if (temp >= 'A' && temp <= 'F')
                {
                    result |= temp - 'A' + 10;
                }
                else
                {
                    throw new FormatException();
                }
            }
            return result;
        }
    }
}
