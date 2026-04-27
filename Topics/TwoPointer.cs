using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class TwoPointer
    {
        public static int[] SumPair(int[] arr, int sum)
        {

            int i = 0; int j = arr.Length - 1;
            //Sum = 6,[1,2,3,4,6] Ans:[2,4]
            while (i < j)
            {
                if (arr[i] + arr[j] > sum)
                {
                    j--;
                }
                else if (arr[i] + arr[j] < sum)
                {
                    i++;
                }
                else
                {
                    return [arr[i], arr[j]];
                }
            }
            return [];
        }

        public static int RemoveDuplicateAndReturnlength(int[] arr)
        {
            int i = 0;
            if (arr.Length <= 1) { return arr.Length; }
            //moved the unique values to left. Ex: [1,1,1,2,2,2,3,4] => [1,2,3,4,1,1,2,2]
            for (int j = 1; j < arr.Length; j++)
            {
                if (arr[j] != arr[j - 1])
                {
                    i++;
                    arr[i] = arr[j];
                }
            }

            // replace duplicates with zero => [1,2,3,4,0,0,0,0]
            for (int k = i + 1; k < arr.Length; k++)
            {
                arr[k] = 0;
            }

            //length of array
            return i + 1;
        }

        public static int[] ReturnNewArrayWithSquares(int[] arr)
        {
            //[-4,-3,-2,-1,0,1,2,5] => [0,1,1,4,4,9,16,25]
            int[] ansArray = new int[arr.Length];
            int i = 0; int j = arr.Length - 1; int k = ansArray.Length - 1;
            while (i <= j)
            {
                int leftSquare = arr[i] * arr[i];
                int rightSquare = arr[j] * arr[j];
                if (leftSquare > rightSquare)
                {
                    ansArray[k] = leftSquare;
                    i++;
                }
                else
                {
                    ansArray[k] = rightSquare;
                    j--;
                }
                k--;
            }
            return ansArray;
        }

        public static int FindMaxWaterBetweenTwoLines(int[] arr)
        {
            int maxArea = 0;
            if (arr.Length <= 1) { return maxArea; }
            int i = 0; int j = arr.Length - 1;
            while (i < j)
            {
                int length = Math.Min(arr[i], arr[j]);
                int breadth = j - i;
                int area = length * breadth;
                maxArea = Math.Max(maxArea, area);
                if (arr[i] < arr[j])
                {
                    i++;
                }
                else
                {
                    j--;
                }
            }
            return maxArea;
        }

        public static List<int[]> FindTriples(int[] arr, int sum)
        {
            var result = new List<int[]>();
            Array.Sort(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                if (i > 0 && arr[i] == arr[i - 1])
                {
                    continue;
                }
                int left = i + 1; int right = arr.Length - 1;
                while (left < right)
                {
                    if (arr[left] + arr[right] + arr[i] > sum)
                    {
                        right--;
                    }
                    else if (arr[left] + arr[right] + arr[i] < sum)
                    {
                        left++;
                    }
                    else
                    {
                        result.Add([arr[i], arr[left], arr[right]]);
                        left++;
                        right--;

                    }
                }
            }
            return result;

        }

        //Arn arrays of bar heights are gives distance between bars is 1 unit. Find max water stored between two bars.
        //Ex:[1,8,6,2,5,4,3,7] => 49 (between bar height 8 and 7)
        public static int FindMaxWaterStoredBetweenTwoBars(int[] arr)
        {
            int maxWater = int.MinValue;
            int i = 0; int j = arr.Length - 1;
            while (i < j)
            {
                int len = Math.Min(arr[i], arr[j]);
                int breadth = j - i;
                int WaterStored = len * breadth;
                maxWater = Math.Max(maxWater, WaterStored);
                if (arr[i] <= arr[j])
                {
                    i++;
                }
                else
                {
                    j--;
                }
            }
            return maxWater == int.MinValue ? 0 : maxWater;
        }


        public static int FindRainDropsStored(int[] arr)
        {
            int i = 0; int j = arr.Length - 1;
            int leftMax = arr[i]; int rightMax = arr[j];
            int totalWater = 0;
            while (i < j)
            {
                if (leftMax <= rightMax)
                {
                    i++;
                    leftMax = Math.Max(leftMax, arr[i]);
                    totalWater += leftMax - arr[i];
                }
                else
                {
                    j--;
                    rightMax = Math.Max(rightMax, arr[j]);
                    totalWater += rightMax - arr[j];
                }
            }
            return totalWater;
        }

        //Maximum Index
        //Given an array arr[], find the maximum j - i such that arr[j] >= arr[i]
        //Ex: [34,8,10,3,2,80,70,60,50] => 6 (j=7,i=1)

        public static int MaximumIndex(int[] nums)
        {
            int maxIndex = int.MinValue;
            int[] maxArray = new int[nums.Length];
            maxArray[nums.Length - 1] = nums[nums.Length - 1];
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                maxArray[i] = Math.Max(nums[i], maxArray[i + 1]);
            }
            int[] minArray = new int[nums.Length];
            minArray[0] = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                minArray[i] = Math.Min(nums[i], minArray[i - 1]);
            }
            int left = 0; int right = 0;
            while (left < nums.Length && right < nums.Length)
            {
                if (minArray[left] <= maxArray[right])
                {
                    maxIndex = Math.Max(maxIndex, right - left);
                    right++;
                }
                else
                {
                    left++;
                }
            }
            return maxIndex == int.MinValue ? -1 : maxIndex;
        }

        //Pair Sum Closest to 0
        public static int PairSumClosestToZero(int[] arr)
        {
            int left = 0; int right = arr.Length - 1;
            int minSum = int.MaxValue;
            Array.Sort(arr);
            while(left < right)
            {
                minSum = Math.Min(minSum, Math.Abs(arr[left] + arr[right]));
                if (arr[left] + arr[right] < 0)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
            return minSum;

        }

        //Maximum Product Subarray
        public static int MaxProduct(int[] nums)
        {
            var prefixProduct = 1;
            var suffixProduct = 1;
            var result = int.MinValue;
            int i = 0, j = nums.Length - 1;
            while (i < nums.Length && j >= 0)
            {
                if (prefixProduct == 0)
                {
                    prefixProduct = 1;
                }
                if (suffixProduct == 0)
                {
                    suffixProduct = 1;
                }
                prefixProduct *= nums[i];
                suffixProduct *= nums[j];

                result = Math.Max(result, Math.Max(prefixProduct, suffixProduct));
                i++; j--;
            }
            return result;
        }
    }
}
