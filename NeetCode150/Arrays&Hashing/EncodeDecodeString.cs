using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.NeetCode150.Arrays_Hashing
{
    public static class EncodeDecodeString
    {
        public static string Encode(List<string> strs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in strs)
            {
                sb.Append(str.Length);
                sb.Append('#');
                sb.Append(str);
            }
            return sb.ToString();
        }

        public static List<string> Decode(string s)
        {
            List<string> result = new List<string>();
            int i = 0;
            while (i < s.Length)
            {
                int j = i;
                while (s[j] != '#')
                {
                    j++;
                }
                int length = int.Parse(s.Substring(i, j - i));
                result.Add(s.Substring(j + 1, length));
                i = j + 1 + length;
            }
            return result;
        }
    }
}
