using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.NeetCode150.Arrays_Hashing
{
    public static class ContainsDuplicate
    {
        public static bool Solution(int[] nums)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (int num in nums)
            {
                if (set.Contains(num))
                {
                    return true;
                }
                set.Add(num);
            }
            return false;
        }
    }
}
