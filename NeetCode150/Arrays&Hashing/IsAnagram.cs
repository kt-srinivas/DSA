using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.NeetCode150.Arrays_Hashing
{
    public static class IsAnagram
    {

        // Given two strings s and t, return true if t is an anagram of s, and false otherwise.
        //Time Copmplexity: O(n) where n is the length of the strings. Space Complexity: O(1) since we are using a fixed size array of 26.
        public static bool Solution(string s, string t)
        {
            int n1 = s.Length;
            int n2 = t.Length;
            if (n1 != n2)
            {
                return false;
            }
            int[] count = new int[26];
            foreach (char c in s)
            {
                count[c - 'a'] += 1;
            }

            foreach (char c in t)
            {
                count[c - 'a'] -= 1;
            }

            for (int i = 0; i < 26; i++)
            {
                if (count[i] != 0)
                {
                    return false;
                }
            }

            return true;
        } 
    }
}
