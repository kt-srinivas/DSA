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

        public static int GetCharValue(char c)
        {
            return c - 'A' + 1;
        }
        
        public static int GetHashCodeValueForString(string s)
        {
            int m = 1000000007;
            int p = 31;
            long hashValue = 0;
            for(int i = 0; i < s.Length; i++)
            {
                hashValue = (hashValue * p + GetCharValue(s[i])) % m;
            }
            return (int)hashValue;
        }

        public static int RabinsKarp(string text, string pattern)
        {
            int m = 1000000007;
            int p = 31;
            int patternHash = GetHashCodeValueForString(pattern);
            long textHash = 0;
            int i = 0, j = 0;
            while(j<text.Length)
            {
                textHash = (textHash * p + GetCharValue(text[j])) % m;
                if (j - i + 1 == pattern.Length)
                {
                    if (textHash == patternHash && text.Substring(i, pattern.Length) == pattern)
                    {
                        return i;
                    }
                    textHash = (textHash - GetCharValue(text[i]) * Power(p, pattern.Length - 1, m) % m + m) % m;
                    i++;
                }
                j++;
            }
            return -1;
        }

        public static long Power(int x, int y, int m)
        {
            long result = 1;
            long baseValue = x % m;
            while (y > 0)
            {
                if ((y & 1) == 1)
                {
                    result = (result * baseValue) % m;
                }
                baseValue = (baseValue * baseValue) % m;
                y >>= 1;
            }
            return result;
        }

        static int KMPSearch(string text, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
                return 0;

            int[] lps = BuildLps(pattern);

            int i = 0; // text index
            int j = 0; // pattern index

            while (i < text.Length)
            {
                if (text[i] == pattern[j])
                {
                    i++;
                    j++;

                    if (j == pattern.Length)
                    {
                        return i - j; // match found
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return -1; // not found
        }

        static int[] BuildLps(string pattern)
        {
            int n = pattern.Length;
            int[] lps = new int[n];

            int len = 0; // length of previous longest prefix suffix
            int i = 1;

            while (i < n)
            {
                if (pattern[i] == pattern[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }

    }
}
