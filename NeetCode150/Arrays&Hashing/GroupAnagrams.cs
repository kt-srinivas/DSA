using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.NeetCode150.Arrays_Hashing
{
    public static class GroupAnagrams
    {
        // Initially I solved it thorugh two loops and checking if the two strings are anagrams or not. But that was not efficient. So I used a dictionary to store the anagrams.
        public static List<List<string>> Solution(string[] strs)
        {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            foreach (string str in strs)
            {
                int[] count = new int[26];
                char[] chars = str.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    count[chars[i] - 'a'] += 1;
                }
                string key = String.Join(",",count);
                if (!map.ContainsKey(key))
                {
                    map[key] = new List<string>();
                }
                map[key].Add(str);
            }
            return map.Values.Select(x => x.ToList()).ToList();
        }
    }
}
