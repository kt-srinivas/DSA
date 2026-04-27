using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.LeetCode.Arrays
{
    public static class Easy
    {
        // LeetCode 1. Two Sum
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> pairs = new Dictionary<int, int> { { nums[0], 0 } };


            for (int j = 1; j < nums.Length; j++)
            {
                int diff = target - nums[j];
                if (pairs.ContainsKey(diff))
                {
                    return [j, pairs[diff]];
                }
                pairs[nums[j]] = j;
            }
            return [];


        }

        // LeetCode 66. Plus OneInput: digits = [1,2,3]
        //Output: [1,2,4]
        //Explanation: The array represents the integer 123.Incrementing by one gives 123 + 1 = 124.Thus, the result should be[1, 2, 4].
        public static int[] PlusOne(int[] digits)
        {
            int n = digits.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                digits[i] = 0;
            }
            int[] result = new int[n + 1];
            result[0] = 1;
            return result;
        }

        // LeetCode 88. Merge Sorted Array
        //Input: nums1 = [1,2,3,0,0,0], m = 3, nums2 = [2,5,6], n = 3
        //Output: [1,2,2,3,5,6]
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            if (m == 0)
            {
                for (int i = 0; i < n; i++)
                {
                    nums1[i] = nums2[i];
                }
            }
            else if (n != 0)
            {
                int i = m - 1;
                int j = n - 1;
                int k = m + n - 1;
                while (j > -1)
                {
                    if (i > -1)
                    {
                        if (nums1[i] >= nums2[j])
                        {
                            nums1[k] = nums1[i];
                            i--;
                        }
                        else
                        {
                            nums1[k] = nums2[j];
                            j--;
                        }
                    }
                    else
                    {
                        nums1[k] = nums2[j];
                        j--;
                    }
                    k--;
                }
            }
            
        }

        // LeetCode 219. Contains Duplicate II
        public static bool ContainsNearbyDuplicate(int[] nums, int k)
        {
            if (nums.Length < 2)
            {
                return false;
            }
            var d = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (d.ContainsKey(nums[i]))
                {
                    if (i - d[nums[i]] <= k)
                    {
                        return true;
                    }
                }
                d[nums[i]] = i;

            }
            return false;
        }
    }
}
