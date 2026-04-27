using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Heaps
    {
        public static int FindKthLargestElement(int[] nums, int k)
        {
            var pq = new PriorityQueue<int, int>();
            for(int i=0;i<nums.Length; i++)
            {
                pq.Enqueue(nums[i], nums[i]);
                if(pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            return pq.Peek();
        }

        public static int FindKthSmallestElement(int[] nums, int k)
        {
            var pq = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(nums[i], nums[i]);
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            return pq.Peek();
        }

        public static int[] RetunKClosestElementsToX(int[] nums, int k, int x)
        {
            var pq = new PriorityQueue<int, (int val, int dis)>(Comparer<(int val, int dis)>.Create((a, b) =>
            {
                var comp = a.dis.CompareTo(b.dis);
                if (comp == 0)
                {
                    return a.val.CompareTo(b.val);
                }
                return comp;
            }));
            for(int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(nums[i], (nums[i], Math.Abs(nums[i] - x)));
                if(pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            int[] result = new int[k];
            for (int i = 0; i < k; i++)
            {
                result[i] = pq.Dequeue();
            }
            return result;
        }

        public static int[] FixAndReturnKSoretedList(int[] nums, int k)
        {
            var result = new int[nums.Length];
            var pq = new PriorityQueue<int, int>();
            int p = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                pq.Enqueue(nums[i], nums[i]);
                if (pq.Count > k)
                {
                    result[p++] = pq.Dequeue();
                }
            }
            return result;
        }

        public static int[] TopKFrequentElements(int[] nums, int k)
        {
            var result = new int[k];
            var map = new Dictionary<int, int>();
            var pq = new PriorityQueue<int,(int val, int freq)>(Comparer<(int val,int freq)>.Create((a, b) => {
                var comp = a.freq.CompareTo(b.freq);
                if (comp == 0)
                {
                    return a.val.CompareTo(b.val);
                }
                return comp;
            }));
            foreach(var element in nums)
            {
                map[element] = map.GetValueOrDefault(element, 0) + 1;
            }
            foreach (var kvp in map)
            {
                pq.Enqueue(kvp.Key, (kvp.Key, kvp.Value));
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            for (int i = 0; i < k; i++)
            {
                result[i] = pq.Dequeue();
            }
            return result;
        }

        public static int[] SortArrayByFrequency(int[] nums)
        {
            var result = new int[nums.Length];
            var map = new Dictionary<int, int>();
            var pq = new PriorityQueue<int, (int val, int freq)>(Comparer<(int val, int freq)>.Create((a, b) =>
            {
                var comp = a.freq.CompareTo(b.freq);
                if (comp == 0)
                {
                    return b.val.CompareTo(a.val);
                }
                return comp;
            }));

            foreach (var element in nums)
            {
                map[element] = map.GetValueOrDefault(element, 0) + 1;
            }
            foreach (var element in nums)
            {
                pq.Enqueue(element, (element, map[element]));
            }
            int p = 0;
            while (pq.Count > 0)
            {
                result[p++] = pq.Dequeue();
            }
            return result;

        }

        public static List<List<int>> ReturnKClosestPointsToOrigin(int[][] points, int k)
        {
            var result = new List<List<int>>();
            var pq = new PriorityQueue<int[], (int[] point, int dis)>(Comparer<(int[] point, int dis)>.Create((a, b) =>
            {
                var comp = a.dis.CompareTo(b.dis);
                if (comp == 0)
                {
                    return a.point[0].CompareTo(b.point[0]);
                }
                return comp;
            }));
            foreach (var point in points)
            {
                pq.Enqueue(point,(point, point[0] * point[0] + point[1] * point[1]));
                if (pq.Count > k)
                {
                    pq.Dequeue();
                }
            }
            for (int i = 0; i < k; i++)
            {
                var point = pq.Dequeue();
                result.Add(new List<int> { point[0], point[1] });
            }
            return result;
        }
    }
}
