using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Boyer_Moore
{
    public class Prog
    {
        static void Main()
        {
            string _keyWord = "hey";
            byte[] _byteKeyWord = Encoding.ASCII.GetBytes(_keyWord);
            string _context = "Lo hey hey rem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et varius nisi";
            byte[] _byteContext = Encoding.ASCII.GetBytes(_context);
            BoyerMoore boyerMoore = new BoyerMoore()
                                        .SetPattern(_byteKeyWord)
                                        .SetSearchArray(_byteContext);
            List<int> _findList = boyerMoore.SearchAll();
            Console.WriteLine("Points of entitites:");
            foreach (var item in _findList)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Number of find words: " + boyerMoore.count());
            Console.WriteLine("Long random prime: "+ longRandomPrime());
        }




        public class BoyerMoore
        {
            private int[] _jumpTable;
            private byte[] _pattern;
            private int _patternLength;
            public byte[] _searchArray;


            public BoyerMoore()
            {
            }
            public BoyerMoore SetPattern(byte[] pattern)
            {
                _pattern = pattern;
                _jumpTable = new int[256];
                _patternLength = _pattern.Length;
                for (var index = 0; index < 256; index++)
                    _jumpTable[index] = _patternLength;
                for (var index = 0; index < _patternLength - 1; index++)
                    _jumpTable[_pattern[index]] = _patternLength - index - 1;
                return this;
            }
            public BoyerMoore SetSearchArray(byte[] array)
            {
                _searchArray = array;
                return this;
            }
            public unsafe int Search(byte[] searchArray, int startIndex = 0)
            {
                if (_pattern == null)
                    throw new Exception("Pattern has not been set.");
                if (_patternLength > searchArray.Length)
                    throw new Exception("Search Pattern length exceeds search array length.");
                var index = startIndex;
                var limit = searchArray.Length - _patternLength;
                var patternLengthMinusOne = _patternLength - 1;
                fixed (byte* pointerToByteArray = searchArray)
                {
                    var pointerToByteArrayStartingIndex = pointerToByteArray + startIndex;
                    fixed (byte* pointerToPattern = _pattern)
                    {
                        while (index <= limit)
                        {
                            var j = patternLengthMinusOne;
                            while (j >= 0 && pointerToPattern[j] == pointerToByteArrayStartingIndex[index + j])
                                j--;
                            if (j < 0)
                                return index;
                            index += Math.Max(_jumpTable[pointerToByteArrayStartingIndex[index + j]] - _patternLength + 1 + j, 1);
                        }
                    }
                }
                return -1;
            }
            public unsafe List<int> SearchAll()
            {
                var index = 0;
                var limit = _searchArray.Length - _patternLength;
                var patternLengthMinusOne = _patternLength - 1;
                var list = new List<int>();
                fixed (byte* pointerToByteArray = _searchArray)
                {
                    var pointerToByteArrayStartingIndex = pointerToByteArray + index;
                    fixed (byte* pointerToPattern = _pattern)
                    {
                        while (index <= limit)
                        {
                            var j = patternLengthMinusOne;
                            while (j >= 0 && pointerToPattern[j] == pointerToByteArrayStartingIndex[index + j])
                                j--;
                            if (j < 0)
                                list.Add(index);
                            index += Math.Max(_jumpTable[pointerToByteArrayStartingIndex[index + j]] - _patternLength + 1 + j, 1);
                        }
                    }
                }
                return list;
            }
            public unsafe int count()
            {
                int count = 0;
                var index = 0;
                var limit = _searchArray.Length - _patternLength;
                var patternLengthMinusOne = _patternLength - 1;
                fixed (byte* pointerToByteArray = _searchArray)
                {
                    var pointerToByteArrayStartingIndex = pointerToByteArray + index;
                    fixed (byte* pointerToPattern = _pattern)
                    {
                        while (index <= limit)
                        {
                            var j = patternLengthMinusOne;
                            while (j >= 0 && pointerToPattern[j] == pointerToByteArrayStartingIndex[index + j])
                                j--;
                            if (j < 0)
                                count++;
                            index += Math.Max(_jumpTable[pointerToByteArrayStartingIndex[index + j]] - _patternLength + 1 + j, 1);
                        }
                    }
                }
                return count;

            }
            public int SuperSearch(byte[] searchArray, int nth, int start = 0)
            {
                var e = start;
                var c = 0;
                do
                {
                    e = Search(searchArray, e);
                    if (e == -1)
                        return -1;
                    c++;
                    e++;
                } while (c < nth);
                return e - 1;
            }
        }

        public static long longRandomPrime() 
        {
            int n = 6;
            for (int i = 0; i < long.MaxValue; i++)
            {
                long startGen = Convert.ToInt64(Math.Pow(10, n - 1));
                long endGen = Convert.ToInt64(Math.Pow(10, n));
                Random rand = new Random();
                long number = next(startGen, endGen);
                if (millerRabinTest(number, 10))
                {
                    return number;
                }
            }
            return 0;
        }
        public static bool millerRabinTest(long n, int k)
        {
            if (n < 2)
                return false;
            if (n == 2)
                return true;
            if (n % 2 == 0)
                return false;
            long r = 0, d = n - 1;
            while (d % 2 == 0)
            {
                d /= 2;
                r++;
            }

            var rand = new Random();

            for (long i = 0; i < k; i++)
            {
                long a = next(2, n - 1);
                BigInteger x = BigInteger.ModPow(a, d, n);
                if (x == 1 || x == n - 1)
                    continue;
                for (int j = 0; j < r - 1; j++)
                {
                    x = BigInteger.ModPow(x, 2, n);
                    if (x == 1)
                        return false;
                    if (x == n - 1)
                        break;
                }
                if (x != n - 1)
                    return false;
            }
            return true;
        }
        public static long next(long min, long max)
        {
            System.Random rd = new System.Random();

            if (max <= min)
                throw new ArgumentOutOfRangeException("max", "max must be > min!");

            ulong uRange = (ulong)(max - min);
            ulong ulongRand;
            do
            {
                byte[] buf = new byte[8];
                rd.NextBytes(buf);
                ulongRand = (ulong)BitConverter.ToInt64(buf, 0);
            } while (ulongRand > ulong.MaxValue - ((ulong.MaxValue % uRange) + 1) % uRange);

            return (long)(ulongRand % uRange) + min;

        }
    }
}
