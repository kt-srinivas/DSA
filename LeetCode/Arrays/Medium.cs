using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DSA.LeetCode.Arrays
{
    public static class Medium
    {
        //Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k] == 0. Notice that the solution set must not contain duplicate triplets.
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums.Length <2) return result;

            Array.Sort(nums);
            for(int s = 0; s < nums.Length - 2; s++)
            {
                if (s>0 && nums[s] == nums[s - 1]) continue;
                int l = s + 1;
                int r = nums.Length - 1;
                while (l < r)
                {
                    if (nums[s] + nums[l] + nums[r] < 0) l++;
                    else if (nums[s] + nums[l] + nums[r] > 0) r--;
                    else
                    {
                        result.Add(new List<int> { nums[s], nums[l], nums[r] });
                        while (l < r && nums[l] == nums[l + 1]) l++;
                        while (l < r && nums[r] == nums[r - 1]) r--;
                        l++;r--;
                    }

                }
            }

            return result;
        }
        //Given an integer array nums of length n and an integer target, find three integers at distinct indices in nums such that the sum is closest to target.
        public static int ThreeSumClosest(int[] nums, int target)
        {
            Array.Sort(nums);
            int closestSum = nums[0] + nums[1] + nums[2];
            for (int s = 0; s < nums.Length - 2; s++)
            {
                int l = s + 1;
                int r = nums.Length - 1;
                while (l < r)
                {
                    int currentSum = nums[s] + nums[l] + nums[r];
                    if (Math.Abs(currentSum - target) < Math.Abs(closestSum - target))
                    {
                        closestSum = currentSum;
                    }
                    if (currentSum < target) l++;
                    else r--;
                }
            }
            return closestSum;

        }

        //Implement next permutation, which rearranges numbers into the lexicographically next greater permutation of numbers.Important. Did not get it initally spend some time if needed.
        // 
        public static int[] NextPemutation(int[] nums)
        {
            int i = nums.Length - 2;

            // Find the Pivot 
            while (i >= 0 && nums[i] >= nums[i + 1]) i--;
            if (i >= 0)
            {
                int j = i;

                //Find the element to swap with pivot that its the just larger than pivot
                while (j < nums.Length - 1 && nums[j + 1] > nums[i]) j++;
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;
            }
            void Reverse(int[] arr, int start, int end)
            {
                while (start < end)
                {
                    int temp = arr[start];
                    arr[start] = arr[end];
                    arr[end] = temp;
                    start++;
                    end--;
                }
            }
            Reverse(nums, i + 1, nums.Length - 1);
            return nums;
        }

        //80. Remove Duplicates from Sorted Array II
        //Given an integer array nums sorted in non-decreasing order, remove some duplicates in-place such that each unique element appears at most twice. The relative order of the elements should be kept the same.
        public static int RemoveDuplicates(int[] nums)
        {
            bool twoflag = false;
            int i = 0; int j = 1;
            while (j < nums.Length)
            {
                if (nums[j] == nums[j - 1])
                {
                    if (!twoflag)
                    {
                        i++;
                        nums[i] = nums[j];
                        twoflag = true;
                    }
                }
                else
                {
                    i++;
                    nums[i] = nums[j];
                    twoflag = false;
                }
                j++;
            }
            return i + 1;
        }

        //122. Best Time to Buy and Sell Stock II
        //You are given an array prices where prices[i] is the price of a given stock on the ith day.
        // Find the maximum profit you can achieve. You may complete as many transactions as you like (i.e., buy one and sell one share of the stock multiple times).
        // Note: You cannot engage in multiple transactions simultaneously (i.e., you must sell the stock before you buy again).
        // Input: prices = [7,1,5,3,6,4]
        // Output: 7
        public static int MaxProfit(int[] prices)
        {
            int maxProfit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] > prices[i - 1])
                {
                    maxProfit += prices[i] - prices[i - 1];
                }
            }
            return maxProfit;
        }

        //134. Gas Station
        // There are n gas stations along a circular route, where the amount of gas at the ith station is gas[i].
        // You have a car with an unlimited gas tank and it costs cost[i] of gas to travel from the ith station to its next (i + 1)th station. You begin the journey with an empty tank at one of the gas stations.
        // Given two integer arrays gas and cost, return the starting gas station's index if you can travel around the circuit once in the clockwise direction, otherwise return -1. If there exists a solution, it is guaranteed to be unique
        // Input: gas = [1,2,3,4,5], cost = [3,4,5,1,2]
        // Output: 3
        public static int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int cs = 0;
            int total = 0;
            int lowestIndex = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                cs += gas[i] - cost[i];
                total += gas[i] - cost[i];
                if (cs < 0)
                {
                    lowestIndex = i + 1;
                    cs = 0;
                }
            }
            return total < 0 ? -1 : lowestIndex;

        }

        //18. 4Sum
        // Given an array nums of n integers, return an array of all the unique quadruplets [nums[a], nums[b], nums[c], nums[d]] such that:
        // 0 <= a, b, c, d < n
        // a, b, c, and d are distinct.
        // nums[a] + nums[b] + nums[c] + nums[d] == target
        //intermediate summations can exceed int range so using long
        public static IList<IList<int>> FourSum(int[] nums, int target)
        {
            List<IList<int>> result = new List<IList<int>>();
            if (nums.Length < 4)
            {
                return result;
            }
            Array.Sort(nums);
            int n = nums.Length;
            for (int t1 = 0; t1 <= n - 4; t1++)
            {
                if (t1 > 0 && nums[t1] == nums[t1 - 1]) continue;
                for (int t2 = t1 + 1; t2 <= n - 3; t2++)
                {
                    if (t2 > t1 + 1 && nums[t2] == nums[t2 - 1]) continue;
                    int i = t2 + 1;
                    int j = n - 1;
                    while (i < j)
                    {

                        long currentSum = (long)nums[t1] + nums[t2] + nums[i] + nums[j];
                        if (currentSum == target)
                        {
                            result.Add([nums[t1], nums[t2], nums[i], nums[j]]);
                            i++; j--;
                            while (i < j && i > t2 + 1 && nums[i] == nums[i - 1]) i++;
                            while (i < j && j < n - 1 && nums[j] == nums[j + 1]) j--;
                        }
                        else if (currentSum < target) i++;
                        else j--;
                    }

                }
            }
            return result;
        }

        //713. Subarray Product Less Than K
        // Given an array of integers nums and an integer k, return the number of contiguous subarrays where the product of all the elements in the subarray is strictly less than k.
        public static int NumSubarrayProductLessThanK(int[] nums, int k)
        {
            if (k <= 1) return 0;
            int i = 0, j = 0;
            int result = 0;
            long tempResult = 1;
            while (j < nums.Length)
            {
                tempResult *= nums[j];
                while (tempResult >= k && i < nums.Length)
                {
                    tempResult = tempResult / nums[i];
                    i++;
                }
                result += j - i + 1;
                j++;
            }
            return result;
            Stack<int> stack = new Stack<int>();
            
        }
    }
}
