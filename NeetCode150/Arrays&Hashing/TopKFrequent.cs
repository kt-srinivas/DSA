using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.NeetCode150.Arrays_Hashing
{
    public static  class TopKFrequent
    {

        // Using priority queue to get the top k frequent elements. The time complexity is O(nlogk) and space complexity is O(n).
        public static int[] SolutionUsinhHeaps(int[] nums, int k)
        {
            Dictionary<int, int> map = new();
            PriorityQueue<int, int> queue = new PriorityQueue<int, int>();
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                map[nums[i]] = map.GetValueOrDefault(nums[i]) + 1;
            }
            foreach (var key in map.Keys)
            {
                queue.Enqueue(key, map[key]);
                if (queue.Count > k)
                {
                    queue.Dequeue();
                }
            }
            int[] result = new int[k];
            int j = 0;
            while (queue.Count > 0)
            {
                int element = queue.Dequeue();
                result[j] = element;
                j++;
            }
            return result;
        }


        //Using bucket sort to get the top k frequent elements. The time complexity is O(n) and space complexity is O(n).
        public static int[] SolutionUsingBucketSort(int[] nums, int k)
        {
            Dictionary<int, int> map = new();
            int n = nums.Length;
            for (int i = 0; i < n; i++)
            {
                map[nums[i]] = map.GetValueOrDefault(nums[i]) + 1;
            }
            List<int>[] bucket = new List<int>[n + 1];
            foreach (var key in map.Keys)
            {
                int freq = map[key];
                if (bucket[freq] == null)
                {
                    bucket[freq] = new List<int>();
                }
                bucket[freq].Add(key);
            }
            List<int> result = new List<int>();
            for(int ti = n; ti >= 0; ti--)
            {
                if (bucket[ti] != null)
                {
                    foreach (var element in bucket[ti])
                    {
                        result.Add(element);
                        if (result.Count == k)
                        {
                            break;
                        }
                    }
                }
            }
            return result.ToArray();
        }
    }
}
