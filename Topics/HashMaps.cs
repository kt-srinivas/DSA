using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class HashMaps
    {
        //Find Target Pair in an Array
        //Given an array of integers and a target integer, return the indices of the two numbers that add up to the target. You can assume that there is exactly one solution, and you cannot use the same element twice.
        //Example:
        //Input: arr = [2, 7, 11, 15], target = 9
        public static int[] FindPair(int[] arr, int target)
        {
            var dict = new Dictionary<int, int>();
            for(int i=0;i< arr.Length; i++)
            {
                if(dict.ContainsKey(target - arr[i]))
                {
                    return new int[] { dict[target - arr[i]], i };
                }
                else
                {
                    dict[arr[i]] = i;
                }
            }
            return [];
        }

        //Longest Subarray with Sum Zero
        //Given an array of integers, find the length of the longest subarray that sums to zero.
        //Example:
        //Input: arr = [1, -1, 3, 2, -2, -3]
        //Output: 5
        public static int LongestSubArrayWithSumZero(int[] arr)
        {
            var dict = new Dictionary<int, int>();
            dict[0] = -1;
            int mL = int.MinValue;
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
                if (dict.TryGetValue(sum, out int value))
                {
                    mL = Math.Max(mL, i - value);
                }
                else
                {
                    dict[sum] = i;
                }
            }
            return mL == int.MinValue ? 0 : mL;
        }

        //Number of Unique Elements in K Sized Window
        //Given an array of integers and a window size k, return an array of the number of unique elements in each window of size k as it slides through the array.
        //Example:
        //Input: arr = [1, 2, 3, 2, 1], k = 3
        //Output: [3, 2, 2]
        public static int[] NumOfUniqueElementsinKSizedWindow(int[] arr, int k)
        {
            var dict = new Dictionary<int, int>();
            var result = new List<int>();
            int i = 0, j = 0;
            while (j < arr.Length)
            {
                dict[arr[j]] = dict.GetValueOrDefault(arr[j]) + 1;
                if (j - i + 1 == k)
                {
                    result.Add(dict.Count);
                    dict[arr[i]]--;
                    if (dict[arr[i]] == 0)
                    {
                        dict.Remove(arr[i]);
                    }
                    i++;
                }
                j++;
            }
            return [.. result];
        }

        //Longest Substring with At Most K Unique Characters
        //Given a string and an integer k, find the length of the longest substring that contains at most k unique characters.
        //Example:
        //Input: arr = "eceba", k = 2
        //Output: 3
        public static int LongestSubStringWithAtmostkUniqueChar(string arr, int k)
        {
            var dict = new Dictionary<int, int>();
            var result = int.MinValue;
            int i = 0, j = 0;
            while (j < arr.Length)
            {
                dict[arr[j]] = dict.GetValueOrDefault(arr[j]) + 1;
                while (dict.Count > k)
                {
                    dict[arr[i]]--;
                    if (dict[arr[i]] == 0)
                    {
                        dict.Remove(arr[i]);
                    }
                    i++;
                }
                result = Math.Max(result, j - i + 1);
                j++;
            }
            return result;
        }

        public static int FindNoOfSubArrayWithSumEqualToK(int[] nums, int k)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            map[0] = 1;

            int sum = 0;
            int count = 0;

            foreach (int num in nums)
            {
                sum += num;

                if (map.ContainsKey(sum - k))
                {
                    count += map[sum - k];
                }

                if (map.ContainsKey(sum))
                    map[sum]++;
                else
                    map[sum] = 1;
            }

            return count;
        }

        //Given two strings s and t, find the length of the shortest substring of s that contains all the characters of t (including duplicates). If there is no such substring, return 0.
        public static string FindMinWindowSubString(string s, string t)
        {
            if(string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length)
            {
                return "";
            }
            var dict = new Dictionary<char, int>();
            for(int k=0;k<t.Length;k++)
            {
                dict[t[k]] = dict.GetValueOrDefault(t[k]) + 1;
            }
            int count = dict.Count;
            int i = 0, j = 0;
            int minLength = int.MaxValue;
            int startIndex = 0;
            while (j < s.Length)
            {
                char c = s[j];
                if (dict.ContainsKey(s[j]))
                {
                    dict[c]--;
                    if (dict[c]==0)
                    {
                        count--;
                    }
                }
                while (count == 0)
                {
                    if(j-i+ 1 < minLength)
                    {
                        minLength = j - i + 1;
                        startIndex = i;
                    }
                    char ch = s[i];
                    if (dict.ContainsKey(ch))
                    {
                        dict[ch]++;
                        if (dict[ch] > 0)
                        {
                            count++;
                        }
                    }
                    i++;
                }
                j++;
            }
            return minLength == int.MaxValue ? "" : s.Substring(startIndex, minLength);
        }


    }
}
