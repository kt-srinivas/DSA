using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class BasicArrayProblems
    {
        public static int[] DutchNationalFlagProblem(int[] arr)
        {
            int i = 0;
            int j = 0;
            int k = arr.Length - 1;
            while (j <= k)
            {
                if (arr[j] == 1)
                {
                    j++;
                }
                if (arr[j] == 0)
                {
                    int temp;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                    i++;
                    j++;
                }
                if (arr[j] == 2)
                {
                    int temp;
                    temp = arr[k];
                    arr[k] = arr[j];
                    arr[j] = temp;
                    k--;
                }
            }
            return arr;
        }

        public static int FindLeastMissingPositiveNumber(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] <= 0 || arr[i] > arr.Length)
                    arr[i] = arr.Length + 1;
            }

            //marking 
            for (int i = 0; i < arr.Length; i++)
            {
                int index = Math.Abs(arr[i]);
                if (index <= arr.Length)
                {
                    if (arr[index - 1] > 0)
                    {
                        arr[index - 1] = -arr[index - 1];
                    }

                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] > 0)
                    return i + 1;
            }
            return arr.Length + 1;
        }

        public static int FindMajorityElement(int[] arr)
        {
            int index = 0;
            int count = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == arr[index])
                    count++;
                else
                    count--;
                if (count == 0)
                {
                    count = 1;
                    index = i;
                }
            }
            int majorityElementCount = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == arr[index])
                    majorityElementCount++;
            }
            if (majorityElementCount > arr.Length / 2)
                return arr[index];
            else
                return -1; //no majority element
        }
        public static int FindMaxProfit(int[] arr)
        {
            int maxProfit = int.MinValue;
            int minStockPrice = int.MaxValue;
            int maxProfitIndex = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                minStockPrice = Math.Min(minStockPrice, arr[i]);
                int proft = arr[i] - minStockPrice;
                if (proft > maxProfit)
                {
                    maxProfit = proft;
                    maxProfitIndex = i;
                }

            }
            Console.WriteLine($"But stock on index {minStockPrice} and sell at {maxProfitIndex}");
            return maxProfit;
        }


        public static int sumOfArray(int[] arr)
        {
            int sum = 0;
            foreach (var item in arr)
            {
                sum += item;
            }
            return sum;
        }

        //time complexity O(n)
        public static int[] prefixSum(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] += arr[i - 1];
            }
            return arr;
        }


        public static int findMax(int[] arr)
        {
            int max = int.MinValue;
            foreach (var item in arr)
            {
                max = Math.Max(max, item);
            }
            return max;
        }

        //not the optimized way. to find nth max in an araay we use Heaps
        public static int findSecondMax(int[] arr)
        {
            int max = int.MinValue;
            int secondMax = int.MinValue;
            int i = 0;
            for (int j = 0; j < arr.Length; j++)
            {
                if (arr[j] > max)
                {
                    secondMax = max;
                    max = arr[j];
                }
                else if (arr[j] > secondMax && arr[j] != max) // duplicate values
                {
                    secondMax = arr[j];
                }
            }
            return secondMax;
        }

        public static int[] MoveZeroesToRightEnd(int[] arr)
        {
            int i = 0;
            int j = 0;
            for (j = 0; j < arr.Length;)
            {
                if (arr[j] == 0)
                {
                    j++;
                }
                else
                {
                    int temp;
                    temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                    i++;
                    j++;
                }
            }
            return arr;
        }

        public static int[] MoveZeroesToLeftEnd(int[] arr)
        {
            int i = arr.Length - 1;
            int j;
            for (j = arr.Length - 1; j >= 0;)
            {
                if (arr[j] == 0)
                {
                    j--;
                }
                else
                {
                    int temp;
                    temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                    i--;
                    j--;
                }
            }
            return arr;
        }

        public static int maxSumSubArray(int[] arr)
        {
            int maxSum = arr[0];
            int currentSum = arr[0];
            int start = 0;
            int end = 0;
            int bestStart = 0;
            for (int i = 1; i < arr.Length; i++)
            {
                if (currentSum < 0)
                {
                    currentSum = arr[i];
                    start = i;
                }
                else
                {
                    currentSum += arr[i];
                }
                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    bestStart = start;
                    end = i;
                }
            }
            Console.WriteLine($"Max Sum SubArray is from index {bestStart} to {end}");

            return maxSum;
        }
    }
}
