using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSA.Models;

namespace DSA.Topics
{
    public static class Recursions
    {
        public static void PrintinAscendingOrder(int n)
        {
            if (n == 0)
            {
                return;
            }
            PrintinAscendingOrder(n - 1);
            Console.WriteLine(n);
        }

        public static void PrintinDescendingOrder(int n)
        {
            if (n == 0)
            {
                return;
            }
            Console.WriteLine(n);
            PrintinDescendingOrder(n - 1);
        }

        public static int SumOfNNaturalNumbers(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            return n + SumOfNNaturalNumbers(n - 1);
        }

        public static int Factorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            return n * Factorial(n - 1);
        }

        public static int Power(int x, int n)
        {
            if (n == 0)
            {
                return 1;
            }
            return x * Power(x, n - 1);
        }

        public static int SumOfElementsInArray(int[] arr, int n)
        {
            if (n == 0)
            {
                return 0;
            }
            return arr[n - 1] + SumOfElementsInArray(arr, n - 1);
        }

        public static int BinarySearchUsingRecursion(int[] arr, int target, int low, int high)
        {
            if (low > high)
            {
                return -1; // Target not found
            }
            int mid = low + (high - low) / 2;
            if (arr[mid] == target)
            {
                return mid; // Target found at index mid
            }
            else if (arr[mid] > target)
            {
                return BinarySearchUsingRecursion(arr, target, low, mid - 1); // Search in the left half
            }
            else
            {
                return BinarySearchUsingRecursion(arr, target, mid + 1, high); // Search in the right half
            }
        }

        public static Node ReverseLinkedList(Node head)
        {
            if (head == null || head.next == null)
            {
                return head; // Base case: empty list or single node
            }
            Node l = head;
            Node r = ReverseLinkedList(head.next); // Recursive call on the rest of the list
            l.next.next = l; // Reverse the link
            l.next = null; // Set the next of current node to null
            return r; // Return the new head of the reversed list
        }

        public static string RemoveMsFromString(string s)
        {
            var sb = new StringBuilder();
            RemoveMs(s, sb, 0);
            return sb.ToString();
        }

        public static void RemoveMs(string s, StringBuilder sb, int index)
        {
            if (index == s.Length)
            {
                return;
            }
            if (s[index] != 'm')
            {
                sb.Append(s[index]);
            }
            RemoveMs(s, sb, index + 1);
        }

        public static int MaxOfArray(int[] arr, int index)
        {
            if (index == arr.Length)
            {
                return int.MinValue;
            }
            int currentMax = MaxOfArray(arr, index + 1);
            return Math.Max(arr[index], currentMax);
        }

        public static string ReverseString(string s)
        {
            var sb = new StringBuilder();
            ReverseStringHelper(s, sb, 0);
            return sb.ToString();
        }

        public static void ReverseStringHelper(string s, StringBuilder sb, int index)
        {
            if (index == s.Length)
            {
                return;
            }
            ReverseStringHelper(s, sb, index + 1);
            sb.Append(s[index]);
        }

        public static string MoveAllXToEnd(string s)
        {
            var sb = new StringBuilder();
            MoveAllXToEndHelper(s, sb, 0);
            return sb.ToString();
        }

        public static void MoveAllXToEndHelper(string s, StringBuilder sb, int index)
        {
            if (index == s.Length)
            {
                return;
            }
            if (s[index] != 'x')
            {
                sb.Append(s[index]);
            }
            MoveAllXToEndHelper(s, sb, index + 1);
            if (s[index] == 'x')
            {
                sb.Append('x');
            }
        }

        public static bool Ispalindrome(string s,int i,int j)
        {
            if (i == j)
            {
                return true;
            }
            if (s[i] != s[j])
            {
                return false;
            }
            return Ispalindrome(s, i + 1, j - 1);
        }


        public static string RemoveDuplicate(string s)
        {
            var sb = new StringBuilder();
            var freqArray = new int[26];
            RemoveDuplicateHelper(s, sb, 0,freqArray);
            return sb.ToString();
        }

        public static void RemoveDuplicateHelper(string s, StringBuilder sb, int index, int[] freqArray)
        {
            if (index == s.Length)
            {
                return;
            }
            char currentChar = s[index];
            if (freqArray[currentChar - 'a'] == 0)
            {
                sb.Append(currentChar);
                freqArray[currentChar - 'a']++;
            }
            RemoveDuplicateHelper(s, sb, index + 1, freqArray);
        }

        public static int PlaceTiles(int n, int m)
        {
           if(n == m)
            {
                return 2; // Two ways: all vertical or all horizontal
            }
            if (n < m)
            {
                return 1; // Only one way: all vertical
            }
            return PlaceTiles(n - 1, m) + PlaceTiles(n - m, m); // Place a tile vertically or horizontally
        }

    }
}
