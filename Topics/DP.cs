using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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
            return ans;
        }

        // Climbing Stairs using Memoization (Top-Down DP)
        // Time Complexity  : O(n)
        // Space Complexity : O(n) recursion stack + O(n) dp array
        public static int ClimbingStairsMemoization(int n)
        {
            // DP array stores already computed results
            int[] dp = new int[n + 1];

            // Base case
            // One way to stay at ground
            dp[0] = 1;

            // One way to reach first stair
            dp[1] = 1;

            // Recursive helper function
            int Helper(int n, int[] dp)
            {
                // If already at 0 or 1 return stored answer
                if (n <= 1)
                    return dp[n];

                // If already computed return memoized answer
                if (dp[n] != 0)
                    return dp[n];

                // Current stair ways =
                // ways from previous stair +
                // ways from two stairs behind
                dp[n] = Helper(n - 1, dp) + Helper(n - 2, dp);

                // Return computed answer
                return dp[n];
            }

            // Compute final answer
            int result = Helper(n, dp);

            // Return total number of ways
            return result;
        }

        // Climbing Stairs using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n)
        // Space Complexity : O(n)
        public static int ClimbingStairsTabulation(int n)
        {
            // DP array where dp[i] stores
            // number of ways to reach stair i
            int[] dp = new int[n + 1];

            // One way to remain at ground
            dp[0] = 1;

            // One way to reach first stair
            dp[1] = 1;

            // Build answer from smaller subproblems
            for (int i = 2; i <= n; i++)
            {
                // Ways to reach current stair =
                // previous stair ways +
                // two stairs behind ways
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            // Return answer for nth stair
            return dp[n];
        }

        public static int HouseRobberMemoization(int[] arr)
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

        public static int HouseRobberTabulation(int[] arr)
        {
            int[] dp = new int[arr.Length];
            dp[0] = 0;
            dp[1] = Math.Max(arr[0], arr[1]);
            for(int i = 2; i < arr.Length; i++)
            {
                int pick = dp[i - 2] + arr[i];
                int skip = dp[i - 1];
                dp[i] = Math.Max(pick, skip);
            }
            return dp[arr.Length - 1];

        }

        // Min Cost Climbing Stairs using Memoization (Top-Down DP)
        // Time Complexity  : O(n)
        // Space Complexity : O(n) recursion stack + O(n) dp array
        public static int MinCostClimbingStairsMemoization(int[] cost)
        {
                // DP array stores minimum cost from each index
                int[] dp = new int[cost.Length];

                // Initialize with -1 indicating uncomputed state
                Array.Fill(dp, -1);

                // Recursive helper function
                int Helper(int index)
                {
                    // If crossed stair range no extra cost needed
                    if (index >= cost.Length)
                    {
                        return 0;
                    }

                    // If already computed return memoized result
                    if (dp[index] != -1)
                    {
                        return dp[index];
                    }

                    // Cost when taking one step forward
                    int oneStep = Helper(index + 1) + cost[index];

                    // Cost when taking two steps forward
                    int twoStep = Helper(index + 2) + cost[index];

                    // Choose minimum cost path
                    int ans = Math.Min(oneStep, twoStep);

                    // Store computed answer
                    dp[index] = ans;

                    // Return minimum cost from current index
                    return ans;
                }

                // Compute from stair 0
                Helper(0);

                // Return minimum of starting from stair 0 or stair 1
                return Math.Min(dp[0], dp[1]);
            
        }

        // Min Cost Climbing Stairs using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n)
        // Space Complexity : O(n)
        public static int MinCostClimbingStairsTabulation(int[] cost)
        {
            int n = cost.Length;
            int[] dp = new int[n + 1];

            // Base cases: cost to reach the top from the last two stairs
            dp[n] = 0; // No cost to stand on the top
            dp[n - 1] = cost[n - 1]; // Cost to step on the last stair

            // Fill the dp array from the second last stair down to the first
            for (int i = n - 2; i >= 0; i--)
            {
                dp[i] = cost[i] + Math.Min(dp[i + 1], dp[i + 2]);
            }

            // The minimum cost to reach the top can start from either of the first two stairs
            return Math.Min(dp[0], dp[1]);
        }

        public static int FrogJumpMemoization(int[] heights)
        {
            int n = heights.Length;
            int[] dp = new int[n];
            Array.Fill(dp, -1);

            int Helper(int i)
            {
                if (i == 0) return 0;
                if (dp[i] != -1) return dp[i];

                int left = Helper(i - 1) + Math.Abs(heights[i] - heights[i - 1]);
                int right = int.MaxValue;
                if (i > 1)
                    right = Helper(i - 2) + Math.Abs(heights[i] - heights[i - 2]);

                dp[i] = Math.Min(left, right);
                return dp[i];
            }

            return Helper(n - 1);
        }

        public static int FrogJumpTabulation(int[] heights)
        {
            int n = heights.Length;
            int[] dp = new int[n];
            dp[0] = 0;

            for (int i = 1; i < n; i++)
            {
                int left = dp[i - 1] + Math.Abs(heights[i] - heights[i - 1]);
                int right = int.MaxValue;
                if (i > 1)
                    right = dp[i - 2] + Math.Abs(heights[i] - heights[i - 2]);

                dp[i] = Math.Min(left, right);
            }

            return dp[n - 1];
        }

        public static int FrogJumpWithKDistancesMemoization(int[] heights, int k)
        {
            int n = heights.Length;
            int[] dp = new int[n];
            Array.Fill(dp, -1);

            int Helper(int i)
            {
                if (i == 0) return 0;
                if (dp[i] != -1) return dp[i];

                int minCost = int.MaxValue;
                for (int j = 1; j <= k; j++)
                {
                    if (i - j >= 0)
                    {
                        int cost = Helper(i - j) + Math.Abs(heights[i] - heights[i - j]);
                        minCost = Math.Min(minCost, cost);
                    }
                }

                dp[i] = minCost;
                return dp[i];
            }

            return Helper(n - 1);
        }

        public static int FrogJumpWithKDistancesTabulation(int[] heights, int k)
        {
            int n = heights.Length;
            int[] dp = new int[n];
            dp[0] = 0;
            for (int i = 1; i < n; i++)
            {
                int minCost = int.MaxValue;
                for (int j = 1; j <= k; j++)
                {
                    if (i - j >= 0)
                    {
                        int cost = dp[i - j] + Math.Abs(heights[i] - heights[i - j]);
                        minCost = Math.Min(minCost, cost);
                    }
                }
                dp[i] = minCost;
            }
            return dp[n - 1];
        }

        /*      --------------------------------------
                       2-D Array (0-1  KnapSack)
                -------------------------------------*/

        // 0/1 Knapsack using Memoization (Top-Down DP)
        // Time Complexity  : O(n * W)
        // Space Complexity : O(n * W) dp array + O(n) recursion stack
        public static int MaxProfitWithLimitedWeightMemoization(int[] profit, int[] weights, int W)
        {
            // DP array where:
            // dp[index,currentWeight]
            // stores maximum profit possible
            int[,] dp =
                new int[profit.Length + 1, W + 1];

            // Initialize DP array
            for (int i = 0; i <= profit.Length; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    // Base cases
                    // No items or zero capacity means zero profit
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Mark remaining states as uncomputed
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Recursive helper function
            int Helper(int currentWeight, int index)
            {
                // If no items left or bag capacity exhausted
                if (index == 0 || currentWeight == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, currentWeight] != -1)
                {
                    return dp[index, currentWeight];
                }

                // Option 1:
                // Pick current item if weight allows
                int pick = int.MinValue;

                // Check if current item can fit inside bag
                if (weights[index - 1] <= currentWeight)
                {
                    pick = Helper(currentWeight - weights[index - 1],index - 1) + profit[index - 1];
                }

                // Option 2:
                // Skip current item
                int notPick =
                    Helper(currentWeight, index - 1);

                // Store maximum profit possible
                dp[index, currentWeight] =
                    Math.Max(pick, notPick);

                // Return computed answer
                return dp[index, currentWeight];
            }

            // Compute answer for full capacity and all items
            return Helper(W, profit.Length);
        }

        // 0/1 Knapsack using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * W)
        // Space Complexity : O(n * W)
        public static int MaxProfitWithLimitedWeightTabulation(int[] profit, int[] weights, int W)
        {
            int n = profit.Length;
            int[,] dp = new int[n + 1, W + 1];

            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else if (weights[i - 1] <= j)
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i - 1, j - weights[i - 1]] + profit[i - 1]);
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            return dp[n, W];
        }

        // Subset Sum Equal To Target using Memoization (Top-Down DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target) dp array + O(n) recursion stack
        public static bool CheckSubSetSumEqualToTargetMemoization(int[] arr, int target)
        {
            // Store number of elements
            int n = arr.Length;

            // DP array where:
            // 1  -> subset possible
            // 0  -> subset not possible
            // -1 -> uncomputed
            int[,] dp = new int[n + 1, target + 1];

            // Initialize DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 is always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 elements
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Remaining states initially uncomputed
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Recursive helper function
            int Helper(int index, int balance)
            {
                // If target becomes 0 subset found
                if (balance == 0)
                {
                    return 1;
                }

                // If no elements left subset impossible
                if (index == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, balance] != -1)
                {
                    return dp[index, balance];
                }

                // Stores pick result
                int pick = 0;

                // Pick current element if possible
                if (arr[index - 1] <= balance)
                {
                    pick = Helper(index - 1, balance - arr[index - 1]);
                }

                // Skip current element
                int notPick = Helper(index - 1, balance);

                // If either path gives valid subset answer is true
                dp[index, balance] = (pick == 1 || notPick == 1) ? 1 : 0;

                // Return computed answer
                return dp[index, balance];
            }

            // Return whether subset exists or not
            return Helper(n, target) == 1;
        }



        // Subset Sum Equal To Target using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target)
        public static bool CheckSubSetSumEqualToTargetTabulation(int[] arr, int target)
        {
            // Step 1:
            // Create DP table where:
            // 1 -> subset possible
            // 0 -> subset not possible
            int[,] dp = new int[arr.Length + 1, target + 1];

            // Step 2:
            // Initialize base cases
            for (int i = 0; i <= arr.Length; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 elements
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                }
            }

            // Step 3:
            // Fill remaining DP states
            for (int i = 1; i <= arr.Length; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    // If current element can be picked
                    if (arr[i - 1] <= j)
                    {
                        // Either pick or skip current element
                        int pick = dp[i - 1, j - arr[i - 1]];
                        int notPick = dp[i - 1, j];
                        dp[i, j] = (pick == 1 || notPick == 1) ? 1 : 0;
                    }

                    // Current element too large cannot pick
                    else
                    {
                        // Carry previous row answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 4:
            // Final answer stored in last cell
            return dp[arr.Length, target] == 1;
        }

        public static bool EqualSumPartition(int[] arr)
        {
            int totalSum = arr.Sum();

            // If total sum is odd, we cannot partition it into two equal subsets
            if (totalSum % 2 != 0)
            {
                return false;
            }

            // Check if there's a subset with sum equal to half of total sum
            return CheckSubSetSumEqualToTargetTabulation(arr, totalSum / 2);
        }


        // Count Number Of Subsets Equal To Target using Memoization (Top-Down DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target) dp array + O(n) recursion stack
        public static int NumberOfSubsetsEqualToTargetMemoization(int[] arr, int target)
        {
            // Step 1:
            // Create DP table where:
            // dp[index,balance]
            // stores number of subsets possible
            int[,] dp = new int[arr.Length + 1, target + 1];

            // Step 2:
            // Initialize DP table
            for (int i = 0; i <= arr.Length; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 elements
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Remaining states initially uncomputed
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Step 3:
            // Recursive helper function
            int Helper(int index, int balance)
            {
                // If target becomes 0 one valid subset found
                if (balance == 0)
                {
                    return 1;
                }

                // If no elements left subset impossible
                if (index == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, balance] != -1)
                {
                    return dp[index, balance];
                }

                // Stores count after picking current element
                int pick = 0;

                // Pick current element if possible
                if (arr[index - 1] <= balance)
                {
                    pick =
                    Helper(
                    index - 1,
                    balance - arr[index - 1]);
                }

                // Skip current element
                int notPick =
                Helper(index - 1, balance);

                // Total subsets =
                // pick subsets + notPick subsets
                dp[index, balance] = pick + notPick;

                // Return computed answer
                return dp[index, balance];
            }

            // Step 4:
            // Return total subset count
            return Helper(arr.Length, target);
        }

        // Count Number Of Subsets Equal To Target using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target)
        public static int NumberOfSubsetsEqualToTargetTabulation(int[] arr, int target)
        {
            // Step 1:
            // Create DP table where:
            // dp[index,balance]
            // stores number of subsets possible
            int[,] dp = new int[arr.Length + 1, target + 1];

            // Step 2:
            // Initialize DP table
            for (int i = 0; i <= arr.Length; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 elements
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                }
            }

            // Step 3:
            // Fill remaining DP states
            for (int i = 1; i <= arr.Length; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    // If current element can be picked
                    if (arr[i - 1] <= j)
                    {
                        // Total subsets =
                        // pick subsets + notPick subsets
                        dp[i, j] = dp[i - 1, j - arr[i - 1]] + dp[i - 1, j];
                    }

                    // Current element too large cannot pick
                    else
                    {
                        // Carry previous row answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 4:
            // Return total subset count from last cell
            return dp[arr.Length, target];
        }

        public static int PartitionArrayWithMinSubsetDifference(int[] arr) 
        {
            int target = 0;
            for(int i=0;i<arr.Length;i++)
            {
                target += arr[i];
            }

            // Step 1:
            // Create DP table where:
            // 1 -> subset possible
            // 0 -> subset not possible
            int[,] dp = new int[arr.Length + 1, target + 1];

            // Step 2:
            // Initialize base cases
            for (int i = 0; i <= arr.Length; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 elements
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                }
            }

            // Step 3:
            // Fill remaining DP states
            for (int i = 1; i <= arr.Length; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    // If current element can be picked
                    if (arr[i - 1] <= j)
                    {
                        // Either pick or skip current element
                        int pick = dp[i - 1, j - arr[i - 1]];
                        int notPick = dp[i - 1, j];
                        dp[i, j] = (pick == 1 || notPick == 1) ? 1 : 0;
                    }

                    // Current element too large cannot pick
                    else
                    {
                        // Carry previous row answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            List<int> possibleSubsetSum = new List<int>();
            for(int j=0;j<= target; j++)
            {
                if (dp[arr.Length, j] == 1)
                {
                    possibleSubsetSum.Add(j);
                }
            }

            int result = int.MaxValue;
            for (int i = 0;i< possibleSubsetSum.Count/2; i++)
            {
                if (possibleSubsetSum[i] > target / 2)
                {
                    break;
                }
                /*S1 + S2 = target => s1 = target-s2 
                 * we need Min(s1-s2) = > min(target-s2-s2) =. min(target-2s2)
                 */
                result = Math.Min(result, Math.Abs(target - 2 * possibleSubsetSum[i]));
            }
            return result;
        }



    }

}
