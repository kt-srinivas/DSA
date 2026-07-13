using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class BitManipulation
    {
        public static bool CheckEvenOrOdd(int n)
        {
            return (n & 1) == 0;
        }

        public static (int,int) SwapNumbers(int a, int b)
        {
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
            return (a, b);
        }

        public static bool IsithBitSet(int n, int i)
        {
            return (n & (1 << i)) != 0;
        }

        public static bool IsithBitSetUsingRightShift(int n, int i)
        {
            return ((n >> i) & 1) == 1;
        }

        public static int SetithBit(int n, int i)
        {
            return n | (1 << i);
        }

        public static int ClearithBit(int n, int i)
        {
            return n & ~(1 << i);
        }

        public static int ToggleithBit(int n, int i)
        {
            return n ^ (1 << i);
        }

        public static int RemoveLastSetBit(int n)
        {
            return n & (n - 1);
        }

        public static int SetTheLastClearBit(int n)
        {
            return n | (n + 1);
        }

        public static int CountSetBits(int n)
        {
            int count = 0;
            while (n > 0)
            {
                n = n & (n - 1);
                count++;
            }
            return count;
        }

        public static int Divide(int dividend, int divisor)
        {
            bool sign = true;
            if (dividend < 0 && divisor > 0)
            {
                sign = false;
            }
            if (dividend > 0 && divisor < 0)
            {
                sign = false;
            }
            long n = Math.Abs((long)dividend);
            long d = Math.Abs((long)divisor);
            long ans = 0;
            while (n >= d)
            {
                int count = 0;
                while (n >= (d << (count + 1)))
                {
                    count++;
                }
                n = n - (d << count);
                ans += 1L << count;
            }

            if (ans == (1L << 31) && sign)
            {
                return int.MaxValue;
            }
            if (ans == (1L << 31) && !sign)
            {
                return int.MinValue;
            }
            return sign ? (int)ans : -(int)ans;
        }


        public static List<List<int>> PrimeFactorizationOfArray(int[] arr)
        {
            var result = new List<List<int>>();
            for(int i=2;i<arr.Length; i++)
            {
                result.Add(PrimeFactorizationofNumber(arr[i]));
            }
            return result;
        }

        public static List<int> PrimeFactorizationofNumber(int n)
        {
            var result = new List<int>();
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if(n % i == 0)
                {
                    while(n % i == 0)
                    {
                        result.Add(i);
                        n /= i;
                    }
                }
            }
            if (n > 1)
            {
                result.Add(n);
            }
            return result;
        }

        public static List<int> PrintAllDivisors(int n)
        {
            var result = new List<int>();
            for(int i = 1; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    result.Add(i);
                    if (i != n / i)
                    {
                        result.Add(n / i);
                    }
                }
            }
            result.Sort();
            return result;
        }
    }
}
