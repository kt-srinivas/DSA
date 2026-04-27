using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Strings
    {
        //Reverse the String
        public static string ReverseString(string s)
        {
            int start = 0;
            int end = s.Length - 1;
            var sb = new StringBuilder();
            for(int i= end; i >= start; i--)
            {
                sb.Append(s[i]);
            }
            return sb.ToString();
        }

        //palindrome string
        public static bool CheckPalindrome(string s)
        {
            int i = 0;
            int j = s.Length - 1;
            while (i <= j)
            {
                if (s[i] != s[j])
                {
                    return false;
                }
                i++;j--;
            }
            return true;
        }

        //Count of each charater in a string
        public static void CountofEachChar(string s)
        {
            var freqArr = new int[26];
            for (int i= 0; i < s.Length;i ++)
            {
                freqArr[s[i] - 'a'] += 1;
                
            }
            for (int i = 0; i < freqArr.Length; i++)
            {
                if (freqArr[i] > 0)
                {
                    Console.WriteLine((char)(i + 'a') + " " + freqArr[i]);
                }
            }
        }

        //Reverse the the order of words
        public static string ReverseWords(String s)
        {
            var sb = new StringBuilder();
            for(int i = s.Length - 1; i >= 0; i--)
            {
                while (i >= 0 && s[i] == ' ')
                    i--;
                int j = i;
                while (i>=0 && s[i]!=' ')
                {
                    i--;
                }
                sb.Append(s.Substring(i + 1, j - i) + " ");
            }
            return sb.ToString().Trim();
        }

        //longest subString without repeating characters
        public static int LongestSubStringWithUniqueCharacters(string s)
        {
            int i = 0;
            int j = 0;
            var freqArray = new int[26];
            int maxLength = int.MinValue;
            while (j < s.Length)
            {
                freqArray[s[j] - 'a'] += 1;
                while (freqArray[s[j] - 'a'] > 1)
                {
                    freqArray[s[i] - 'a'] -= 1;
                    i++;
                }
                maxLength = Math.Max(maxLength, j - i + 1);
                j++;
            }
            return maxLength;
        }

        public static int LongestSubStringByExchangingAtMostKCharacters(string s, int k)
        {
            int i = 0;
            int j = 0;
            var freqArray = new int[26];
            int maxLength = int.MinValue;
            while(j < s.Length)
            {
                freqArray[s[j] - 'a'] += 1;
                while ((j - i + 1) - freqArray.Max() > k)
                {
                    freqArray[s[i] - 'a'] -= 1;
                    i++;
                }
                maxLength = Math.Max(maxLength, j - i + 1);
                j++;
            }
            return maxLength;
        }

        public static string LongestPalindrome(string s)
        {
            string result = "";
            int maxLength = result.Length;
            for (int i = 0; i < s.Length; i++)
            {
                int l = i, r = i;
                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (r - l + 1 > maxLength)
                    {
                        result = s.Substring(l, r - l + 1);
                        maxLength = r - l + 1;
                    }
                    l--;
                    r++;
                }

                l = i; r = i + 1;
                while (l >= 0 && r < s.Length && s[l] == s[r])
                {
                    if (r - l + 1 > maxLength)
                    {
                        result = s.Substring(l, r - l + 1);
                        maxLength = r - l + 1;
                    }
                    l--;
                    r++;
                }
            }
            return result;
        }
        
    }
}
