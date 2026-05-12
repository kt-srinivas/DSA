using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class DP
    {
        public static int Fibonacci(int n)
        {
            int[] dp = new int[n+1];
            Array.Fill(dp, -1);
            dp[0] = 0;
            dp[1] = 1;
            int FibHelper(int n, int[] dp)
            {
                if (n <= 1) return dp[n];
                if (dp[n] != 0) return dp[n];
                dp[n] = FibHelper(n - 1, dp) + FibHelper(n - 2, dp);
                return dp[n];
            }
            int ans = FibHelper(n, dp);
        }

        public static int ClimbingStairs(int n)
        {
            int[] dp = new int[n + 1];
            dp[0] = 1;
            dp[1] = 1;
            int Helper(int n, int[] dp)
            {
                if (n <= 1) return dp[n];
                if (dp[n] != 0) return dp[n];
                dp[n] = Helper(n - 1, dp) + Helper(n - 2, dp);
                return dp[n];
            }
            int result = Helper(n, dp);
            return result;
        }

        public static int HouseRobber(int[] arr)
        {
            int[] dp = new int[arr.Length];

            Array.Fill(dp, -1);

            int Helper(int[] arr, int i, int[] dp)
            {
                // Base case
                if (i < 0)
                    return 0;

                // Memoized
                if (dp[i] != -1)
                    return dp[i];

                // Pick current house
                int pick = Helper(arr, i - 2, dp) + arr[i];

                // Skip current house
                int skip = Helper(arr, i - 1, dp);

                dp[i] = Math.Max(pick, skip);

                return dp[i];
            }

            return Helper(arr, arr.Length - 1, dp);
        }
    }
}
