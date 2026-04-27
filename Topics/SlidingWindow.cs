using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class SlidingWindow
    {
        public static int MaxSumSubArrayOfSizeK(int[] arr, int k)
        {
            //Ex: [2,1,5,1,3,2], k=3 => 9
            int windowSum = 0;
            int maxSum = 0;
            int windowStart = 0;
            for (int windowEnd = 0; windowEnd < arr.Length; windowEnd++)
            {
                windowSum += arr[windowEnd]; // add the next element

                // slide the window, we don't need to slide if we've not hit the required window size of 'k'
                if (windowEnd - windowStart + 1 == k)
                {
                    maxSum = Math.Max(maxSum, windowSum);
                    windowSum -= arr[windowStart]; // subtract the element going out
                    windowStart++; // slide the window ahead
                }
            }
            return maxSum;
        }

        public static int MaxVowelsinSubStringofSizeK(string arr, int k)
        {
            int windowStart = 0;
            int MaxVowelsCount = 0;
            for (int windowEnd = 0; windowEnd < arr.Length; windowEnd++)
            {
                if (IsVowel(arr[windowEnd]))
                {
                    MaxVowelsCount++;
                }
                if (windowEnd - windowStart + 1 == k)
                {
                    //update max count
                    MaxVowelsCount = Math.Max(MaxVowelsCount, MaxVowelsCount);
                    //remove the first char from window
                    if (IsVowel(arr[windowStart]))
                    {
                        MaxVowelsCount--;
                    }
                    windowStart++;
                }
            }
            return MaxVowelsCount;

        }

        public static bool IsVowel(char c)
        {
            if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
            {
                return true;
            }
            return false;
        }

        public static int LongestSubsArrayWithSumLessThanEqualToK(int[] arr, int k)
        {
            int maxLength = 0;
            int windowSum = 0;
            int j = 0; int i = 0;
            while (j < arr.Length)
            {
                windowSum += arr[j];
                while (windowSum > k)
                {
                    windowSum -= arr[i];
                    i++;
                }
                maxLength = Math.Max(maxLength, j - i + 1);
                j++;
            }
            return maxLength;
        }

        public static int MinLengthSubArrayWithSUmGreaterThanEqualToK(int[] arr, int k)
        {
            int minLength = int.MaxValue;
            int windowSum = 0;
            int windowStart = 0;
            for (int windowEnd = 0; windowEnd < arr.Length; windowEnd++)
            {
                windowSum += arr[windowEnd];
                while (windowSum >= k)
                {
                    minLength = Math.Min(minLength, windowEnd - windowStart + 1);
                    windowSum -= arr[windowStart];
                    windowStart++;
                }
            }
            return minLength == int.MaxValue ? 0 : minLength;
        }

        public static int MaxLengthWithContinousOnesAfterFlippingKZeros(int[] arr, int k)
        {
            int zeroesCount = 0;
            int i = 0; int j = 0;
            int maxLength = 0;
            while (j < arr.Length)
            {
                if (arr[j] == 0)
                {
                    zeroesCount++;
                }
                while (zeroesCount > k)
                {
                    if (arr[i] == 0)
                    {
                        zeroesCount--;
                    }
                    i++;
                }
                maxLength = Math.Max(maxLength, j - i + 1);
                j++;

            }
            return maxLength;
        }

        public static int[] SubArrayWithSumEqualToK(int[] arr, int k)
        {
            int windowSum = 0;
            int windowStart = 0;
            for (int windowEnd = 0; windowEnd < arr.Length; windowEnd++)
            {
                windowSum += arr[windowEnd];
                while (windowSum > k)
                {
                    windowSum -= arr[windowStart];
                    windowStart++;
                }
                if (windowSum == k)
                {
                    return [windowStart,windowEnd];
                }
            }
            return [-1];
        }

    }
}
