using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSA.Models;

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
            PriorityQueue<(int tId, int index, int uId),int> queue = new PriorityQueue<(int tId, int index, int uId), int >(Comparer<int>.Create((a, b) => b.CompareTo(a)));
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


        // Definition for singly-linked list.
        public static Node MergeKLists(Node[] lists)
        {

            // Step 1: Handle edge case
            if (lists.Length == 0)
            {
                return null;
            }

            // Step 2: Create min-heap (priority queue)
            // WHY: Always extract smallest node among K lists
            var pq = new PriorityQueue<Node, Node>(
                Comparer<Node>.Create((a, b) => a.data - b.data)
            );

            // Step 3: Add head of each list to heap
            // WHY: These are initial candidates
            foreach (var node in lists)
            {
                if (node != null)
                {
                    pq.Enqueue(node, node);
                }
            }

            // Step 4: Dummy node to simplify list construction
            Node dummy = new Node(0);
            Node tail = dummy;

            // Step 5: Process heap
            while (pq.Count > 0)
            {

                // Extract smallest node
                var tempNode = pq.Dequeue();

                // Add it to result list
                tail.next = tempNode;
                tail = tail.next;

                // Step 6: If extracted node has next → add to heap
                // WHY: Maintain candidates from same list
                if (tempNode.next != null)
                {
                    pq.Enqueue(tempNode.next, tempNode.next);
                }
            }

            // Step 7: Return merged list
            return dummy.next;
        }

        public static List<int> MaxCombinations(int[] nums1, int[] nums2, int k)
        {
            // Step 1: Sort both arrays (ascending)
            Array.Sort(nums1);
            Array.Sort(nums2);

            int n = nums1.Length;

            // Step 2: Max heap (priority queue)
            // Store: (sum, i, j)
            var maxHeap = new PriorityQueue<(int sum, int i, int j), int>();

            // Step 3: Visited set to avoid duplicate index pairs
            var visited = new HashSet<(int, int)>();

            // Step 4: Start from largest possible sum
            int i = n - 1, j = n - 1;
            maxHeap.Enqueue((nums1[i] + nums2[j], i, j), -(nums1[i] + nums2[j]));
            visited.Add((i, j));

            var result = new List<int>();

            // Step 5: Extract top k combinations
            while (k-- > 0 && maxHeap.Count > 0)
            {
                var current = maxHeap.Dequeue();

                int sum = current.sum;
                int x = current.i;
                int y = current.j;

                result.Add(sum);

                // Step 6: Try (i-1, j)
                if (x - 1 >= 0 && !visited.Contains((x - 1, y)))
                {
                    int newSum = nums1[x - 1] + nums2[y];
                    maxHeap.Enqueue((newSum, x - 1, y), -newSum);
                    visited.Add((x - 1, y));
                }

                // Step 7: Try (i, j-1)
                if (y - 1 >= 0 && !visited.Contains((x, y - 1)))
                {
                    int newSum = nums1[x] + nums2[y - 1];
                    maxHeap.Enqueue((newSum, x, y - 1), -newSum);
                    visited.Add((x, y - 1));
                }
            }

            return result;
        }

    }
}
