using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Queues
    {
        public static void PrintBinaryVersionForN(int n)
        {
            var queue = new Queue<string>();
            var count = 1;
            queue.Enqueue("1");
            while (count <= n)
            {
                var current = queue.Dequeue();
                Console.WriteLine(current);
                queue.Enqueue(current + "0");
                queue.Enqueue(current + "1");
                count++;
            }
        }

        public static char[] FirstNonRepaetingalphabetInStream(string s)
        {
            var freqArr = new int[26];
            var result = new char[s.Length];
            var queue = new Queue<char>();
            for (int i = 0;i<s.Length; i++)
            {
                freqArr[s[i] - 'a']++;
                queue.Enqueue(s[i]);
                while (queue.Count !=0 && freqArr[s[i] - 'a'] > 1)
                {
                    queue.Dequeue();
                }
                if (queue.Count == 0)
                {
                    result[i] = '-';
                }
                else
                {
                    result[i] = queue.Peek();
                }
            }
            return result;
        }

        public static int[] FirstNegativeNumberInSubArrayOfSizeK(int[] arr,int k)
        {
            var result = new int[arr.Length-k+1];
            var queue = new Queue<int>();
            int i = 0, j = 0;
            while (j < arr.Length)
            {
                if (arr[j]<0)
                {
                    queue.Enqueue(arr[j]);
                }
                if (j - i + 1 == k)
                {
                    result[i] = queue.Count == 0 ? 0 : queue.Peek();
                    if(queue.Count != 0 && queue.Peek() == arr[i])
                    {
                        queue.Dequeue();
                    }
                    i++;
                }
                j++;
            }
            return result;
        }
    }
}
