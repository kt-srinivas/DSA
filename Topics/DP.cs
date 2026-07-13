using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
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


        /*      --------------------------------------
                       2-D Array (Unbound  KnapSack)
                -------------------------------------*/

        // Unbounded Knapsack using Memoization (Top-Down DP)
        // Time Complexity  : O(n * W)
        // Space Complexity : O(n * W) dp array + O(W) recursion stack
        public static int MaxProfitWithLimitedWeightMemoizationUnbound(int[] profit, int[] weight, int W)
        {
            // Step 1:
            // Store total number of items
            int n = weight.Length;

            // Step 2:
            // Create DP table where:
            // dp[index,balance]
            // stores maximum profit possible
            int[,] dp = new int[n + 1, W + 1];

            // Step 3:
            // Initialize DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    // No items or no capacity means zero profit
                    if (i == 0 || j == 0)
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

            // Step 4:
            // Recursive helper function
            int Helper(int index, int balance)
            {
                // If no items left or capacity exhausted
                if (index == 0 || balance == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, balance] != -1)
                {
                    return dp[index, balance];
                }

                // Stores profit after picking current item
                int pick = 0;

                // Pick current item if weight allows
                if (weight[index - 1] <= balance)
                {
                    // Important:
                    // Stay at same index because
                    // item can be picked multiple times
                    pick = profit[index - 1] + Helper(index,balance - weight[index - 1]);
                }

                // Skip current item
                int notPick =Helper(index - 1, balance);
                // Store maximum profit possible
                dp[index, balance] = Math.Max(pick, notPick);
                // Return computed answer
                return dp[index, balance];
            }

            // Step 5:
            // Return maximum possible profit
            return Helper(n, W);
        }

        // Unbounded Knapsack using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * W)
        // Space Complexity : O(n * W)
        // Unbounded Knapsack using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * W)
        // Space Complexity : O(n * W)
        public static int MaxProfitWithLimitedWeightTabulationUnbound(int[] profit, int[] weight, int W)
        {
            // Step 1:
            // Store total number of items
            int n = profit.Length;

            // Step 2:
            // Create DP table where:
            // dp[i,j]
            // stores maximum profit possible
            // using first i items and capacity j
            int[,] dp = new int[n + 1, W + 1];

            // Step 3:
            // Fill DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    // No items or zero capacity means zero profit
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // If current item can be picked
                    else if (weight[i - 1] <= j)
                    {
                        // Option 1:
                        // Skip current item
                        int notPick =dp[i - 1, j];

                        // Option 2:
                        // Pick current item
                        // Stay at same row because
                        // item can be reused multiple times
                        int pick =profit[i - 1] +dp[i, j - weight[i - 1]];

                        // Store maximum profit
                        dp[i, j] = Math.Max(pick, notPick);
                    }

                    // Current item too heavy cannot pick
                    else
                    {
                        // Carry previous answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 4:
            // Final cell stores maximum profit
            return dp[n, W];
        }

        // Rod Cutting Problem using Memoization (Top-Down DP)
        // Time Complexity  : O(N * N)
        // Space Complexity : O(N * N) dp array + O(N) recursion stack
        public static int RodCuttingProblemMemoization(int N, int[] prices)
        {
            // Step 1:
            // Create DP table where:
            // dp[index,balance] stores maximum obtainable profit
            int[,] dp = new int[N + 1, N + 1];

            // Step 2:
            // Initialize DP table
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    // No pieces or no remaining rod length means zero profit
                    if (i == 0 || j == 0)
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
            // Create rod lengths array
            int[] lengths = new int[N];

            for (int i = 0; i < N; i++)
            {
                lengths[i] = i + 1;
            }

            // Step 4:
            // Recursive helper function
            int Helper(int index, int balance)
            {
                // No pieces left or rod fully used
                if (index == 0 || balance == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, balance] != -1)
                {
                    return dp[index, balance];
                }

                // Stores profit after picking current rod piece
                int pick = 0;

                // Pick current rod piece if possible
                if (lengths[index - 1] <= balance)
                {
                    // Stay at same index because same rod length can be reused
                    pick = prices[index - 1] + Helper(index, balance - lengths[index - 1]);
                }

                // Skip current rod piece
                int notPick = Helper(index - 1, balance);

                // Store maximum obtainable profit
                dp[index, balance] = Math.Max(pick, notPick);

                // Return computed answer
                return dp[index, balance];
            }

            // Step 5:
            // Return maximum obtainable profit
            return Helper(N, N);
        }

        // Rod Cutting Problem using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(N * N)
        // Space Complexity : O(N * N)
        public static int RodCuttingProblemTabulation(int N, int[] prices)
        {
            // Step 1:
            // Create DP table where:
            // dp[i,j] stores maximum obtainable profit
            // using first i rod lengths and remaining rod length j
            int[,] dp = new int[N + 1, N + 1];

            // Step 2:
            // Fill DP table
            for (int i = 0; i <= N; i++)
            {
                for (int j = 0; j <= N; j++)
                {
                    // No pieces or no remaining rod length means zero profit
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // If current rod piece can be cut
                    else if (i <= j)
                    {
                        // Option 1:
                        // Skip current rod piece
                        int notPick = dp[i - 1, j];

                        // Option 2:
                        // Cut current rod piece
                        // Stay at same row because same rod length can be reused
                        int pick = prices[i - 1] + dp[i, j - i];

                        // Store maximum profit
                        dp[i, j] = Math.Max(pick, notPick);
                    }

                    // Current rod piece too long cannot cut
                    else
                    {
                        // Carry previous answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 3:
            // Final cell stores maximum obtainable profit
            return dp[N, N];
        }

        // Coin Change 2 (Count Total Ways) using Memoization (Top-Down DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target) dp array + O(target) recursion stack
        public static int NoOfWaysForCoinsToGetSumMemoization(int[] arr, int target)
        {
            // Step 1:
            // Store total number of coins
            int n = arr.Length;

            // Step 2:
            // Create DP table where:
            // dp[index,balance]
            // stores total number of ways
            int[,] dp = new int[n + 1, target + 1];

            // Step 3:
            // Initialize DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 coins
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

            // Step 4:
            // Recursive helper function
            int Helper(int index, int balance)
            {
                // If target becomes 0 one valid way found
                if (balance == 0)
                {
                    return 1;
                }

                // No coins left but target still remains
                if (index == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, balance] != -1)
                {
                    return dp[index, balance];
                }

                // Stores ways after picking current coin
                int pick = 0;

                // Pick current coin if possible
                if (arr[index - 1] <= balance)
                {
                    // Stay at same index because
                    // coin can be reused unlimited times
                    pick =
                    Helper(
                    index,
                    balance - arr[index - 1]);
                }

                // Skip current coin
                int notPick =
                Helper(index - 1, balance);

                // Total ways =
                // pick ways + notPick ways
                dp[index, balance] =
                pick + notPick;

                // Return computed answer
                return dp[index, balance];
            }

            // Step 5:
            // Return total number of ways
            return Helper(n, target);
        }


        // Coin Change 2 (Count Total Ways) using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target)
        public static int NoOfWaysForCoinsToGetSumTabulation(int[] arr, int target)
        {
            // Step 1:
            // Store total number of coins
            int n = arr.Length;

            // Step 2:
            // Create DP table where:
            // dp[i,j] stores total number of ways
            // to make sum j using first i coins
            int[,] dp = new int[n + 1, target + 1];

            // Step 3:
            // Initialize DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 always possible using empty subset
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // Positive target impossible with 0 coins
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                }
            }

            // Step 4:
            // Fill remaining DP states
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    // If current coin can be picked
                    if (arr[i - 1] <= j)
                    {
                        // Total ways =
                        // pick ways + notPick ways
                        dp[i, j] = dp[i, j - arr[i - 1]] + dp[i - 1, j];
                    }

                    // Current coin too large cannot pick
                    else
                    {
                        // Carry previous row answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 5:
            // Return total number of ways from last cell
            return dp[n, target];
        }

        // Coin Change (Minimum Coins) using Memoization (Top-Down DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target) dp array + O(target) recursion stack
        public static int MinNoOfCoinsToGetSum(int[] arr, int target)
        {
            // Step 1:
            // Store total number of coins
            int n = arr.Length;

            // Step 2:
            // Use large value to represent impossible states
            int max = int.MaxValue - 1;

            // Step 3:
            // Create DP table where:
            // dp[index,balance] stores minimum coins required
            int[,] dp = new int[n + 1, target + 1];

            // Step 4:
            // Initialize DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 requires 0 coins
                    if (j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Positive target impossible with 0 coins
                    else if (i == 0)
                    {
                        dp[i, j] = max;
                    }

                    // Remaining states initially uncomputed
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Step 5:
            // Recursive helper function
            int Helper(int index, int balance)
            {
                // If target achieved return 0 coins
                if (balance == 0)
                {
                    return 0;
                }

                // If no coins left return impossible
                if (index == 0)
                {
                    return max;
                }

                // Return memoized answer if already computed
                if (dp[index, balance] != -1)
                {
                    return dp[index, balance];
                }

                // Stores minimum coins after picking current coin
                int pick = max;

                // Pick current coin if possible
                if (arr[index - 1] <= balance)
                {
                    // Stay at same index because same coin can be reused
                    pick = 1 + Helper(index, balance - arr[index - 1]);
                }

                // Skip current coin
                int notPick = Helper(index - 1, balance);

                // Store minimum coins required
                dp[index, balance] = Math.Min(pick, notPick);

                // Return computed answer
                return dp[index, balance];
            }

            // Step 6:
            // Compute final answer
            int ans = Helper(n, target);

            // Return -1 if impossible else return answer
            return ans >= max ? -1 : ans;
        }

        // Coin Change (Minimum Coins) using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * target)
        // Space Complexity : O(n * target)
        public static int MinNoOfCoinsToGetSumTabulation(int[] arr, int target)
        {
            // Step 1:
            // Store total number of coins
            int n = arr.Length;

            // Step 2:
            // Use large value to represent impossible states
            int max = int.MaxValue - 1;

            // Step 3:
            // Create DP table where:
            // dp[i,j] stores minimum coins required
            // to make sum j using first i coins
            int[,] dp = new int[n + 1, target + 1];

            // Step 4:
            // Initialize DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= target; j++)
                {
                    // Target 0 requires 0 coins
                    if (j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Positive target impossible with 0 coins
                    else if (i == 0)
                    {
                        dp[i, j] = max;
                    }
                }
            }

            // Step 5:
            // Fill remaining DP states
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= target; j++)
                {
                    // If current coin can be picked
                    if (arr[i - 1] <= j)
                    {
                        // Option 1:
                        // Pick current coin
                        int pick = 1 + dp[i, j - arr[i - 1]];

                        // Option 2:
                        // Skip current coin
                        int notPick = dp[i - 1, j];

                        // Store minimum coins required
                        dp[i, j] = Math.Min(pick, notPick);
                    }

                    // Current coin too large cannot pick
                    else
                    {
                        // Carry previous row answer
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 6:
            // Final cell has minimum coins required
            int ans = dp[n, target];

            // Return -1 if impossible else return answer
            return ans >= max ? -1 : ans;
        }

        // Longest Common Subsequence using Memoization (Top-Down DP)
        // Time Complexity  : O(n1 * n2)
        // Space Complexity : O(n1 * n2) dp array + O(n1 + n2) recursion stack
        public static (int, string) LongestCommonSubsequenceMemoization(string s1, string s2)
        {
            // Step 1: Store lengths of both strings
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2: Create DP table where dp[i,j] stores LCS length
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3: Initialize DP table
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // If any string length becomes 0 then LCS length is 0
                    if (i == 0 || j == 0)
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

            // Step 4: Recursive helper function
            int Helper(int index1, int index2)
            {
                // If any string exhausted return 0
                if (index1 == 0 || index2 == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index1, index2] != -1)
                {
                    return dp[index1, index2];
                }

                // If characters match include current character in subsequence
                if (s1[index1 - 1] == s2[index2 - 1])
                {
                    dp[index1, index2] = 1 + Helper(index1 - 1, index2 - 1);
                }

                // If characters do not match explore both possibilities
                else
                {
                    int opt1 = Helper(index1 - 1, index2);
                    int opt2 = Helper(index1, index2 - 1);
                    dp[index1, index2] = Math.Max(opt1, opt2);
                }

                // Return computed answer
                return dp[index1, index2];
            }
            var sb = new StringBuilder();
            int x = n1, y = n2;
            while(x>0 && y > 0)
            {
                if (s1[x] == s2[y])
                {
                    sb.Append(s1[x]);
                    x--;
                    y--;
                }
                else if (dp[x - 1, y] > dp[x,y-1])
                {
                    x--;
                }
                else
                {
                    y--;
                }
            }
            string subSequence = new string(sb.ToString().Reverse().ToArray());
            // Step 5: Return longest common subsequence length
            return (Helper(n1, n2),subSequence);
        }

        // Longest Common Subsequence using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n1 * n2)
        // Space Complexity : O(n1 * n2)
        public static (int, string) LongestCommonSubsequenceTabulation(string s1, string s2)
        {
            // Step 1: Store lengths of both strings
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2: Create DP table where dp[i,j] stores LCS length
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3: Fill DP table
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // If any string length becomes 0 then LCS length is 0
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // If characters match include current character in subsequence
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = 1 + dp[i - 1, j - 1];
                    }

                    // If characters do not match explore both possibilities
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            var sb = new StringBuilder();
            int x = n1, y = n2;
            while (x > 0 && y > 0)
            {
                if (s1[x] == s2[y])
                {
                    sb.Append(s1[x]);
                    x--;
                    y--;
                }
                else if (dp[x - 1, y] > dp[x, y - 1])
                {
                    x--;
                }
                else
                {
                    y--;
                }
            }
            string subSequence = new string(sb.ToString().Reverse().ToArray());

            // Step 4: Return longest common subsequence length from last cell
            return (dp[n1, n2], subSequence);
        }

        // Longest Common Substring using Memoization (Top-Down DP)
        // Time Complexity  : O(n1 * n2)
        // Space Complexity : O(n1 * n2)
        public static (int, string) LongestCommonSubstringMemoization(string s1, string s2)
        {
            // Step 1: Store lengths of both strings
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2: Create DP table where dp[i,j] stores longest common substring ending exactly at i and j
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3: Initialize DP table
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // Base case when any string length becomes 0
                    if (i == 0 || j == 0)
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

            // Step 4: Store maximum substring length
            int maxLength = 0;

            // Step 5: Store ending index of substring in first string
            int lastIndex = 0;

            // Step 6: Recursive helper function
            int Helper(int index1, int index2)
            {
                // If any string exhausted return 0
                if (index1 == 0 || index2 == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index1, index2] != -1)
                {
                    return dp[index1, index2];
                }

                // If characters match continue substring diagonally
                if (s1[index1 - 1] == s2[index2 - 1])
                {
                    int ans = 1 + Helper(index1 - 1, index2 - 1);

                    // Update global maximum substring length
                    if (ans > maxLength)
                    {
                        maxLength = ans;
                        lastIndex = index1 - 1;
                    }

                    // Store computed answer
                    dp[index1, index2] = ans;
                }

                // If characters mismatch substring breaks immediately
                else
                {
                    dp[index1, index2] = 0;
                }

                // Explore remaining DP states
                Helper(index1 - 1, index2);
                Helper(index1, index2 - 1);

                // Return computed answer
                return dp[index1, index2];
            }

            // Step 7: Start recursion
            Helper(n1, n2);

            // Step 8: Extract longest common substring
            string substring =maxLength == 0 ? "": s1.Substring(lastIndex - maxLength + 1, maxLength);

            // Step 9: Return length and substring
            return (maxLength, substring);
        }

        // Longest Common Substring using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n1 * n2)
        // Space Complexity : O(n1 * n2)
        public static (int, string) LongestCommonSubstringTabulation(string s1, string s2)
        {
            // Step 1: Store lengths of both strings
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2: Create DP table where dp[i,j] stores longest common substring ending exactly at i and j
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3: Fill DP table
            int maxLength = 0;
            int lastIndex = 0;
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // Base case when any string length becomes 0
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // If characters match continue substring diagonally
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = 1 + dp[i - 1, j - 1];

                        // Update global maximum substring length
                        if (dp[i, j] > maxLength)
                        {
                            maxLength = dp[i, j];
                            lastIndex = i - 1;
                        }
                    }

                    // If characters mismatch substring breaks immediately
                    else
                    {
                        dp[i, j] = 0;
                    }
                }
            }

            // Step 4: Extract longest common substring
            string substring = maxLength == 0 ? "" : s1.Substring(lastIndex - maxLength + 1, maxLength);

            // Step 5: Return length and substring
            return (maxLength, substring);
        }


        public static int ShortestCommonSupersequence(string s1, string s2)
        {
            // Step 1: Get longest common subsequence length
            int lcsLength = LongestCommonSubsequenceTabulation(s1, s2).Item1;

            // Step 2: Shortest common supersequence length =
            // sum of both string lengths - longest common subsequence length
            return s1.Length + s2.Length - lcsLength;
        }

        public static int LongestPalindromeSubSequence(string s)
        {
            // Step 1: Get longest common subsequence length of string and its reverse
            int lcsLength = LongestCommonSubsequenceTabulation(s, new string(s.Reverse().ToArray())).Item1;

            // Step 2: Longest palindromic subsequence length is same as longest common subsequence length
            return lcsLength;
        }


        // Ninja Training using Memoization (Top-Down DP)
        // Time Complexity  : O(n * 4)
        // Space Complexity : O(n * 4) dp array + O(n) recursion stack
        public static int NinjaTrainingMemoization(int[][] points)
        {
            // Step 1:
            // Store total number of days
            int n = points.Length;

            // Step 2:
            // Create DP table where:
            // dp[index,last] stores maximum points on day index
            // if last activity done on previous day is last
            int[,] dp = new int[n+1, 4];

            // Step 3:
            // Initialize DP table with -1 for uncomputed states
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 4:
            // Recursive helper function
            int Helper(int index, int last)
            {
                // Base case when all days are covered
                if (index == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[index, last] != -1)
                {
                    return dp[index, last];
                }

                int maxPoints = 0;

                // Explore all activities for current day
                for (int activity = 0; activity < 3; activity++)
                {
                    // Skip same activity as previous day
                    if (activity != last)
                    {
                        int pointsEarned = points[index-1][activity] + Helper(index - 1, activity);
                        maxPoints = Math.Max(maxPoints, pointsEarned);
                    }
                }

                // Store computed answer
                dp[index, last] = maxPoints;

                // Return maximum points for current state
                return maxPoints;
            }

            // Step 5:
            // Start recursion from day 0 with no previous activity (last = 3)
            return Helper(n, 3);
        }

        // Ninja Training using Tabulation (Bottom-Up DP)
        // Time Complexity  : O(n * 4 * 3)
        // Space Complexity : O(n * 4)
        public static int NinjaTrainingTabulation(int[][] points)
        {
            // Step 1:
            // Store total number of days
            int n = points.Length;

            // Step 2:
            // Create DP table where:
            // dp[day,last] stores maximum points till given day
            // if previous day's activity was 'last'
            int[,] dp = new int[n + 1, 4];

            // Step 3:
            // Initialize base case for day 1
            // If previous activity was 0 choose max between 1 and 2
            dp[1, 0] = Math.Max(points[0][1], points[0][2]);

            // If previous activity was 1 choose max between 0 and 2
            dp[1, 1] = Math.Max(points[0][0], points[0][2]);

            // If previous activity was 2 choose max between 0 and 1
            dp[1, 2] = Math.Max(points[0][0], points[0][1]);

            // If no previous activity choose maximum of all three
            dp[1, 3] =
            Math.Max(
            points[0][0],
            Math.Max(points[0][1], points[0][2]));

            // Step 4:
            // Fill remaining DP states
            for (int day = 2; day <= n; day++)
            {
                // Try every possible previous activity
                for (int last = 0; last < 4; last++)
                {
                    // Store maximum points for current state
                    int maxPoints = 0;

                    // Try every activity for current day
                    for (int activity = 0; activity < 3; activity++)
                    {
                        // Cannot perform same activity consecutively
                        if (activity != last)
                        {
                            // Calculate current total points
                            int currentPoints =
                            points[day - 1][activity] +
                            dp[day - 1, activity];

                            // Update maximum points
                            maxPoints =
                            Math.Max(maxPoints, currentPoints);
                        }
                    }

                    // Store computed answer
                    dp[day, last] = maxPoints;
                }
            }

            // Step 5:
            // Return maximum points after all days
            return dp[n, 3];
        }

        // Unique Paths using Memoization
        // Time Complexity  : O(m * n)
        // Space Complexity : O(m * n) + O(m + n) (Recursion Stack)
        public static int UniquePathsMemoization(int m, int n)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the number of unique paths
            // to reach cell (i,j)
            int[,] dp = new int[m + 1, n + 1];

            // Step 2:
            // Initialize DP table with -1 indicating uncomputed states
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 3:
            // Recursive helper function
            int Helper(int i, int j)
            {
                // Out of bounds
                if (i == 0 || j == 0)
                {
                    return 0;
                }

                // Reached starting cell
                if (i == 1 && j == 1)
                {
                    return 1;
                }

                // Return memoized answer if already computed
                if (dp[i, j] != -1)
                {
                    return dp[i, j];
                }

                // Compute paths from top and left cells
                int top = Helper(i - 1, j);
                int left = Helper(i, j - 1);

                // Total paths to current cell
                int ans = top + left;

                // Store computed answer
                dp[i, j] = ans;

                return ans;
            }

            // Step 4:
            // Compute answer starting from destination cell
            Helper(m, n);

            // Step 5:
            // Return total unique paths
            return dp[m, n];
        }


        // Unique Paths using Tabulation
        // Time Complexity  : O(m * n)
        // Space Complexity : O(m * n)
        public static int UniquePathsTabulation(int m, int n)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the number of unique paths
            // to reach cell (i,j)
            int[,] dp = new int[m + 1, n + 1];

            // Step 2:
            // Fill the DP table from top-left to bottom-right
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    // Cells outside the grid contribute 0 paths
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Starting cell has exactly one path
                    else if (i == 1 && j == 1)
                    {
                        dp[i, j] = 1;
                    }

                    // Number of paths to current cell is the sum of
                    // paths from the top and left cells
                    else
                    {
                        dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }

            // Step 3:
            // Return the total unique paths to reach the destination
            return dp[m, n];
        }

        // Unique Paths II (With Obstacles) using Memoization
        // Time Complexity  : O(m * n)
        // Space Complexity : O(m * n) + O(m + n) (Recursion Stack)
        public static int UniquePathsWithObstaclesMemoization(int[][] grid)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the number of unique paths
            // to reach cell (i,j)
            int[,] dp = new int[grid.Length + 1, grid[0].Length + 1];

            // Step 2:
            // Initialize DP table
            // 0 -> Boundary cells and obstacle cells
            // -1 -> Uncomputed valid cells
            for (int i = 0; i <= grid.Length; i++)
            {
                for (int j = 0; j <= grid[0].Length; j++)
                {
                    // Cells outside the grid contribute 0 paths
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Obstacle cells also contribute 0 paths
                    else if ((i > 0 && j > 0) && (grid[i - 1][j - 1] == 1))
                    {
                        dp[i, j] = 0;
                    }

                    // Valid cells are initialized as uncomputed
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Step 3:
            // Recursive helper function
            int Helper(int i, int j)
            {
                // Outside the grid
                if (i == 0 || j == 0)
                {
                    return 0;
                }

                // Starting cell
                if (i == 1 && j == 1)
                {
                    return grid[i - 1][j - 1] == 1 ? 0 : 1;
                }

                // Return memoized answer if already computed
                if (dp[i, j] != -1)
                {
                    return dp[i, j];
                }

                // Compute paths from left and top cells
                int left = Helper(i, j - 1);
                int top = Helper(i - 1, j);

                // Total paths to current cell
                int ans = left + top;

                // Store computed answer
                dp[i, j] = ans;

                return ans;
            }

            // Step 4:
            // Compute answer from destination cell
            Helper(grid.Length, grid[0].Length);

            // Step 5:
            // Return total unique paths
            return dp[grid.Length, grid[0].Length];
        }

        // Unique Paths II (With Obstacles) using Tabulation
        // Time Complexity  : O(m * n)
        // Space Complexity : O(m * n)
        public static int UniquePathsWithObstaclesTabulation(int[][] grid)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the number of unique paths
            // to reach cell (i,j)
            int[,] dp = new int[grid.Length + 1, grid[0].Length + 1];

            // Step 2:
            // Initialize DP table
            // 0 -> Boundary cells and obstacle cells
            // -1 -> Valid cells that are yet to be computed
            for (int i = 0; i <= grid.Length; i++)
            {
                for (int j = 0; j <= grid[0].Length; j++)
                {
                    // Cells outside the grid contribute 0 paths
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // Obstacle cells contribute 0 paths
                    else if ((i > 0 && j > 0) && (grid[i - 1][j - 1] == 1))
                    {
                        dp[i, j] = 0;
                    }

                    // Valid cells are marked as uncomputed
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Step 3:
            // Initialize the starting cell if it is not blocked
            if (grid[0][0] == 0)
            {
                dp[1, 1] = 1;
            }

            // Step 4:
            // Fill the DP table from top-left to bottom-right
            for (int i = 1; i <= grid.Length; i++)
            {
                for (int j = 1; j <= grid[0].Length; j++)
                {
                    // Compute only valid cells
                    if (dp[i, j] == -1)
                    {
                        // Number of paths is the sum of
                        // paths from the top and left cells
                        dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
                    }
                }
            }

            // Step 5:
            // Return the total unique paths to reach the destination
            return dp[grid.Length, grid[0].Length];
        }

        // Minimum Path Sum using Memoization
        // Time Complexity  : O(m * n)
        // Space Complexity : O(m * n) + O(m + n) (Recursion Stack)
        public static int MinSumpathMemoization(int[][] grid)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the minimum path sum
            // to reach cell (i,j)
            int[,] dp = new int[grid.Length + 1, grid[0].Length + 1];

            // Step 2:
            // Initialize DP table
            // int.MaxValue -> Boundary cells (invalid paths)
            // -1 -> Uncomputed valid cells
            for (int i = 0; i <= grid.Length; i++)
            {
                for (int j = 0; j <= grid[0].Length; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = int.MaxValue;
                    }
                    else
                    {
                        dp[i, j] = -1;
                    }
                }
            }

            // Step 3:
            // Recursive helper function
            int Helper(int i, int j)
            {
                // Outside the grid represents an invalid path
                if (i == 0 || j == 0)
                {
                    return int.MaxValue;
                }

                // Base case: starting cell
                if (i == 1 && j == 1)
                {
                    dp[i, j] = grid[i - 1][j - 1];
                    return dp[i, j];
                }

                // Return memoized answer if already computed
                if (dp[i, j] != -1)
                {
                    return dp[i, j];
                }

                // Compute minimum path sum from left and top cells
                int left = Helper(i, j - 1);
                int top = Helper(i - 1, j);

                // Current cell value + minimum of both possible paths
                int ans = grid[i - 1][j - 1] + Math.Min(left, top);

                // Store computed answer
                dp[i, j] = ans;

                return dp[i, j];
            }

            // Step 4:
            // Compute answer starting from the destination cell
            Helper(grid.Length, grid[0].Length);

            // Step 5:
            // Return the minimum path sum
            return dp[grid.Length, grid[0].Length];
        }

        // Minimum Path Sum using Tabulation
        // Time Complexity  : O(m * n)
        // Space Complexity : O(m * n)
        public static int MinSumpathTabulation(int[][] grid)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the minimum path sum
            // to reach cell (i,j)
            int[,] dp = new int[grid.Length + 1, grid[0].Length + 1];

            // Step 2:
            // Initialize boundary cells with int.MaxValue
            // so they are never chosen as the minimum path
            for (int i = 0; i <= grid.Length; i++)
            {
                for (int j = 0; j <= grid[0].Length; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = int.MaxValue;
                    }
                }
            }

            // Step 3:
            // Initialize the starting cell
            dp[1, 1] = grid[0][0];

            // Step 4:
            // Fill the DP table from top-left to bottom-right
            for (int i = 1; i <= grid.Length; i++)
            {
                for (int j = 1; j <= grid[0].Length; j++)
                {
                    // Skip the already initialized starting cell
                    if (i == 1 && j == 1)
                    {
                        continue;
                    }

                    // Minimum path to current cell is the current cell value
                    // plus the minimum of the top and left paths
                    dp[i, j] = grid[i - 1][j - 1] + Math.Min(dp[i - 1, j], dp[i, j - 1]);
                }
            }

            // Step 5:
            // Return the minimum path sum to reach the destination
            return dp[grid.Length, grid[0].Length];
        }


        // Triangle Minimum Path Sum using Memoization
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²) + O(n) (Recursion Stack)
        public static int? TriangleMinPathSumMemoization(List<List<int>> grid)
        {
            // Step 1:
            // Store the total number of rows
            int n = grid.Count;

            // Step 2:
            // Create a jagged DP array where dp[row][col] stores
            // the minimum path sum to reach (row, col)
            int?[][] dp = new int?[n + 1][];

            // Step 3:
            // Allocate memory for each row (1-based indexing)
            for (int i = 0; i <= n; i++)
            {
                dp[i] = new int?[i + 2];
            }

            // Step 4:
            // Recursive helper function
            int Helper(int row, int col)
            {
                // Invalid position
                if (col < 1 || col > row)
                {
                    return int.MaxValue;
                }

                // Base case: starting cell
                if (row == 1 && col == 1)
                {
                    return grid[0][0];
                }

                // Return memoized answer if already computed
                if (dp[row][col] != null)
                {
                    return dp[row][col].Value;
                }

                // Compute minimum path sum from both possible parent cells
                int leftParent = Helper(row - 1, col - 1);
                int rightParent = Helper(row - 1, col);

                // Choose the minimum parent path
                int parentMin = Math.Min(leftParent, rightParent);

                // Include the current cell value
                dp[row][col] = grid[row - 1][col - 1] + parentMin;

                return dp[row][col].Value;
            }

            // Step 5:
            // Minimum path can end at any cell in the last row
            int ans = int.MaxValue;

            for (int col = 1; col <= n; col++)
            {
                ans = Math.Min(ans, Helper(n, col));
            }

            // Step 6:
            // Return the minimum path sum
            return ans;
        }

        // Triangle Minimum Path Sum using Tabulation
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²)
        public static int? TriangleMinPathSumTabulation(List<List<int>> grid)
        {
            // Step 1:
            // Store the total number of rows
            int n = grid.Count;

            // Step 2:
            // Create a jagged DP array where dp[row][col] stores
            // the minimum path sum to reach (row, col)
            int?[][] dp = new int?[n + 1][];

            // Step 3:
            // Allocate memory for each row (1-based indexing)
            for (int i = 0; i <= n; i++)
            {
                dp[i] = new int?[i + 2];
            }

            // Step 4:
            // Initialize the starting cell at the top of the triangle
            dp[1][1] = grid[0][0];

            // Step 5:
            // Fill the DP table from top to bottom
            for (int row = 2; row <= n; row++)
            {
                for (int col = 1; col <= row; col++)
                {
                    // Get the path sums from both possible parent cells
                    // If a parent doesn't exist, treat it as an invalid path
                    int leftParent = dp[row - 1][col - 1] ?? int.MaxValue;
                    int rightParent = dp[row - 1][col] ?? int.MaxValue;

                    // Choose the minimum parent path
                    int parentMin = Math.Min(leftParent, rightParent);

                    // Include the current cell value
                    dp[row][col] = grid[row - 1][col - 1] + parentMin;
                }
            }

            // Step 6:
            // The minimum path can end at any cell in the last row
            int ans = int.MaxValue;

            for (int col = 1; col <= n; col++)
            {
                ans = Math.Min(ans, dp[n][col] ?? int.MaxValue);
            }

            // Step 7:
            // Return the minimum path sum
            return ans;
        }


        // Ninja And His Friends (Cherry Pickup II) using Memoization
        // Time Complexity  : O(n * m² * 9) = O(n * m²)
        // Space Complexity : O(n * m²) + O(n) (Recursion Stack)
        public static int NinjaAndHisFriendsCollectChoclatesMemoization(int[][] grid)
        {
            // Step 1:
            // Store number of rows and columns
            int n = grid.Length;
            int m = grid[0].Length;

            // Step 2:
            // dp[i,j1,j2] stores the maximum chocolates collected
            // starting from row i when Robot 1 is at column j1
            // and Robot 2 is at column j2
            int[,,] dp = new int[n, m, m];

            // Step 3:
            // Initialize DP table with -1
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    for (int k = 0; k < m; k++)
                    {
                        dp[i, j, k] = -1;
                    }
                }
            }

            // Step 4:
            // Recursive helper function
            int Helper(int i, int j1, int j2)
            {
                // Invalid column positions
                if (j1 < 0 || j1 >= m || j2 < 0 || j2 >= m)
                    return int.MinValue;

                // Base case: last row
                if (i == n - 1)
                {
                    // If both robots are on same cell collect once
                    return (j1 == j2)
                        ? grid[i][j1]
                        : grid[i][j1] + grid[i][j2];
                }

                // Return memoized answer
                if (dp[i, j1, j2] != -1)
                    return dp[i, j1, j2];

                // Store maximum chocolates for current state
                int max = int.MinValue;

                // Possible movements for both robots
                int[] dy = { -1, 0, 1 };

                // Try all three moves for Robot 1
                for (int d1 = 0; d1 < 3; d1++)
                {
                    // Try all three moves for Robot 2
                    for (int d2 = 0; d2 < 3; d2++)
                    {
                        // Compute answer for next row
                        int next = Helper(i + 1, j1 + dy[d1], j2 + dy[d2]);

                        // Skip invalid paths
                        if (next == int.MinValue)
                            continue;

                        // Collect chocolates from current row
                        int value = (j1 == j2)
                            ? grid[i][j1]
                            : grid[i][j1] + grid[i][j2];

                        // Add chocolates collected from remaining rows
                        value += next;

                        // Update maximum chocolates
                        max = Math.Max(max, value);
                    }
                }

                // Store and return answer
                return dp[i, j1, j2] = max;
            }

            // Step 5:
            // Robots start at (0,0) and (0,m-1)
            return Helper(0, 0, m - 1);
        }

        // Ninja And His Friends (Cherry Pickup II) using Tabulation
        // Time Complexity  : O(n * m * m * 9) = O(n * m²)
        // Space Complexity : O(n * m²)
        public static int NinjaAndHisFriendsCollectChoclatesTabulation(int[][] grid)
        {
            // Step 1:
            // Store number of rows and columns
            int n = grid.Length;
            int m = grid[0].Length;

            // Step 2:
            // dp[i,j1,j2] stores the maximum chocolates collected
            // starting from row i when Robot 1 is at column j1
            // and Robot 2 is at column j2
            int[,,] dp = new int[n, m, m];

            // Step 3:
            // Possible column movements for both robots
            int[] dy = { -1, 0, 1 };

            // Step 4:
            // Initialize base case for the last row
            for (int j1 = 0; j1 < m; j1++)
            {
                for (int j2 = 0; j2 < m; j2++)
                {
                    // If both robots are on same cell collect once
                    dp[n - 1, j1, j2] =
                        (j1 == j2)
                        ? grid[n - 1][j1]
                        : grid[n - 1][j1] + grid[n - 1][j2];
                }
            }

            // Step 5:
            // Fill DP table from second last row to first row
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j1 = m - 1; j1 >= 0; j1--)
                {
                    for (int j2 = 0; j2 < m; j2++)
                    {
                        // Store maximum chocolates for current state
                        int max = int.MinValue;

                        // Try all three moves for Robot 1
                        for (int d1 = 0; d1 < 3; d1++)
                        {
                            // Try all three moves for Robot 2
                            for (int d2 = 0; d2 < 3; d2++)
                            {
                                // Calculate next columns
                                int dj1 = j1 + dy[d1];
                                int dj2 = j2 + dy[d2];

                                // Skip invalid positions
                                if (dj1 < 0 || dj1 >= m || dj2 < 0 || dj2 >= m)
                                {
                                    continue;
                                }

                                // Collect chocolates from current row
                                int value = (j1 == j2)
                                    ? grid[i][j1]
                                    : grid[i][j1] + grid[i][j2];

                                // Add best answer from next row
                                int next = dp[i + 1, dj1, dj2];
                                value += next;

                                // Update maximum chocolates
                                max = Math.Max(max, value);
                            }
                        }

                        // Store answer for current state
                        dp[i, j1, j2] = max;
                    }
                }
            }

            // Step 6:
            // Robots start at (0,0) and (0,m-1)
            return dp[0, 0, m - 1];
        }

        // Ninja and His Friends (Cherry Pickup II) using Space Optimization
        // Time Complexity  : O(n * m * m * 9) ≈ O(n * m²)
        // Space Complexity : O(m²)
        public static int NinjaAndHisFriendsCollectChoclatesSpaceOptimized(int[][] grid)
        {
            // Step 1:
            // Store the number of rows and columns
            int n = grid.Length;
            int m = grid[0].Length;

            // Step 2:
            // front[j1,j2] stores the answer for the next row (i + 1)
            // curr[j1,j2] stores the answer for the current row (i)
            int[,] front = new int[m, m];
            int[,] curr = new int[m, m];

            // Step 3:
            // Initialize the base case for the last row
            // If both friends are on the same cell, count chocolates once,
            // otherwise count chocolates from both cells
            for (int j1 = 0; j1 < m; j1++)
            {
                for (int j2 = 0; j2 < m; j2++)
                {
                    front[j1, j2] =
                        (j1 == j2)
                        ? grid[n - 1][j1]
                        : grid[n - 1][j1] + grid[n - 1][j2];
                }
            }

            // Possible column movements for both friends
            int[] dy = { -1, 0, 1 };

            // Step 4:
            // Process rows from bottom to top
            for (int i = n - 2; i >= 0; i--)
            {
                // Try every possible position of Friend 1
                for (int j1 = m - 1; j1 >= 0; j1--)
                {
                    // Try every possible position of Friend 2
                    for (int j2 = 0; j2 < m; j2++)
                    {
                        int max = int.MinValue;

                        // Explore all 9 possible move combinations
                        for (int d1 = 0; d1 < 3; d1++)
                        {
                            for (int d2 = 0; d2 < 3; d2++)
                            {
                                int dj1 = j1 + dy[d1];
                                int dj2 = j2 + dy[d2];

                                // Ignore invalid positions
                                if (dj1 < 0 || dj1 >= m || dj2 < 0 || dj2 >= m)
                                {
                                    continue;
                                }

                                // Collect chocolates from the current row
                                // Count only once if both friends are on the same cell
                                int value = (j1 == j2)
                                    ? grid[i][j1]
                                    : grid[i][j1] + grid[i][j2];

                                // Add the best answer from the next row
                                value += front[dj1, dj2];

                                // Keep the maximum chocolates possible
                                max = Math.Max(max, value);
                            }
                        }

                        // Store the best answer for the current state
                        curr[j1, j2] = max;
                    }
                }

                // Step 5:
                // Move the current row to front for the next iteration
                var temp = front;
                front = curr;
                curr = temp;
            }

            // Step 6:
            // Friend 1 starts at column 0 and Friend 2 starts at column m-1
            return front[0, m - 1];
        }
        // Target Sum - Count Number of Ways using Memoization
        // Time Complexity  : O(n * (2 * totalSum + 1))
        // Space Complexity : O(n * (2 * totalSum + 1)) + O(n) (Recursion Stack)
        public static int NoOfPathsForTargetSumMemoization(int[] arr, int target)
        {
            // Step 1:
            // Compute the total sum of all elements
            int arrSum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arrSum += arr[i];
            }

            // Step 2:
            // If the target is outside the possible range [-arrSum, arrSum],
            // it is impossible to form the target
            if (Math.Abs(target) > arrSum)
            {
                return -1;
            }

            // Step 3:
            // Create DP table where:
            // dp[index, shiftedBalance] stores the number of ways
            // to achieve the required balance using the first 'index' elements.
            // Since balance can be negative, shift it by arrSum.
            int[,] dp = new int[arr.Length + 1, 2 * arrSum + 1];

            // Step 4:
            // Initialize DP table with int.MinValue to indicate uncomputed states
            for (int i = 0; i <= arr.Length; i++)
            {
                for (int j = 0; j <= 2 * arrSum; j++)
                {
                    dp[i, j] = int.MinValue;
                }
            }

            // Step 5:
            // Recursive helper function
            int Helper(int index, int balance)
            {
                // Base case:
                // If no elements are left, check whether the required balance is zero
                if (index == 0)
                {
                    return balance == 0 ? 1 : 0;
                }

                // Shift balance to map negative values into valid DP indices
                int shifted = arrSum + balance;

                // Return memoized answer if already computed
                if (dp[index, shifted] != int.MinValue)
                {
                    return dp[index, shifted];
                }

                // Assign '-' sign to the current element
                int negSide = Helper(index - 1, balance + arr[index - 1]);

                // Assign '+' sign to the current element
                int posSide = Helper(index - 1, balance - arr[index - 1]);

                // Total ways from both choices
                dp[index, shifted] = negSide + posSide;

                return dp[index, shifted];
            }

            // Step 6:
            // Start recursion with the required target balance
            Helper(arr.Length, target);

            // Step 7:
            // Return the total number of ways
            return dp[arr.Length, arrSum + target];
        }

        // Target Sum - Count Number of Ways using Tabulation
        // Time Complexity  : O(n * (2 * totalSum + 1))
        // Space Complexity : O(n * (2 * totalSum + 1))
        public static int NoOfPathsForTargetSumTabulation(int[] arr, int target)
        {
            // Step 1:
            // Compute the total sum of all elements
            int arrSum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                arrSum += arr[i];
            }

            // Step 2:
            // If the target is outside the possible range [-arrSum, arrSum],
            // it is impossible to form the target
            if (Math.Abs(target) > arrSum)
            {
                return -1;
            }

            // Step 3:
            // Create DP table where:
            // dp[index, shiftedBalance] stores the number of ways
            // to achieve the required balance using the first 'index' elements.
            // Since balance can be negative, shift it by arrSum.
            int[,] dp = new int[arr.Length + 1, 2 * arrSum + 1];

            // Step 4:
            // Initialize the DP table
            for (int i = 0; i <= arr.Length; i++)
            {
                for (int j = 0; j <= 2 * arrSum; j++)
                {
                    dp[i, j] = int.MinValue;
                }
            }

            // Step 5:
            // Base case:
            // With no elements, there is exactly one way to achieve balance 0
            dp[0, arrSum] = 1;

            // Step 6:
            // Build the DP table
            for (int index = 1; index <= arr.Length; index++)
            {
                // Try every possible balance
                for (int balance = -arrSum; balance <= arrSum; balance++)
                {
                    int shifted = arrSum + balance;

                    // Assign '-' sign to the current element
                    int negSide = balance + arr[index - 1] <= arrSum
                        ? dp[index - 1, shifted + arr[index - 1]]
                        : 0;

                    // Assign '+' sign to the current element
                    int posSide = balance - arr[index - 1] >= -arrSum
                        ? dp[index - 1, shifted - arr[index - 1]]
                        : 0;

                    // Total ways from both choices
                    dp[index, shifted] = negSide + posSide;
                }
            }

            // Step 7:
            // Return the number of ways to achieve the target
            return dp[arr.Length, arrSum + target];
        }


        // Minimum Insertions to Make a String Palindrome
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²) (Depends on LongestPalindromeSubSequence implementation)
        public static int MinimumInsertionsToMakePalindrome(string s)
        {
            // Step 1:
            // Find the length of the Longest Palindromic Subsequence (LPS)
            int lpsLength = LongestPalindromeSubSequence(s);

            // Step 2:
            // The minimum number of insertions required is the number of
            // characters that are not part of the LPS
            return s.Length - lpsLength;
        }

        public static int MinimunInsertionAndDeletionsToMakeStrinAtoStringB(string a, string b)
        {
            // Step 1: Get longest common subsequence length
            int lcsLength = LongestCommonSubsequenceTabulation(a, b).Item1;

            // Step 2: Minimum insertions and deletions required =
            // sum of both string lengths - 2 * longest common subsequence length
            return (a.Length + b.Length) - 2 * lcsLength;
        }

        // Shortest Common Supersequence (SCS)
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m)
        public static string ShortestCommonSupersequenceString(string s1, string s2)
        {
            // Step 1:
            // Store the lengths of both strings
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2:
            // Create DP table where dp[i,j] stores the length of the
            // Longest Common Subsequence (LCS) of the first i and j characters
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3:
            // Fill the LCS DP table
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // If either string is empty, LCS length is 0
                    if (i == 0 || j == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // If characters match, include the character in the LCS
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = 1 + dp[i - 1, j - 1];
                    }

                    // Otherwise, take the maximum LCS from the two possibilities
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }

            // Step 4:
            // Backtrack through the DP table to construct the SCS
            int row = n1;
            int col = n2;
            var sb = new StringBuilder();

            while (row > 0 && col > 0)
            {
                // Common character belongs to the SCS only once
                if (s1[row - 1] == s2[col - 1])
                {
                    sb.Append(s1[row - 1]);
                    row--;
                    col--;
                }

                // Move in the direction of the larger LCS value
                // and include the corresponding character
                else if (dp[row - 1, col] > dp[row, col - 1])
                {
                    sb.Append(s1[row - 1]);
                    row--;
                }
                else
                {
                    sb.Append(s2[col - 1]);
                    col--;
                }
            }

            // Step 5:
            // Append the remaining characters of the first string
            while (row > 0)
            {
                sb.Append(s1[row - 1]);
                row--;
            }

            // Step 6:
            // Append the remaining characters of the second string
            while (col > 0)
            {
                sb.Append(s2[col - 1]);
                col--;
            }

            // Step 7:
            // Reverse the constructed string since it was built backwards
            return new string(sb.ToString().Reverse().ToArray());
        }


        // Distinct Subsequences using Memoization
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m) + O(n) (Recursion Stack)
        public static int NumDistinctMemoization(string s, string t)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the number of distinct
            // subsequences of the first i characters of s
            // that form the first j characters of t
            long?[,] dp = new long?[s.Length + 1, t.Length + 1];

            // Step 2:
            // Recursive helper function
            long Solve(int i, int j)
            {
                // If the target string is empty,
                // there is exactly one valid subsequence
                if (j == 0)
                    return 1;

                // If the source string is empty but the target is not,
                // it is impossible to form the target
                if (i == 0)
                    return 0;

                // Return memoized answer if already computed
                if (dp[i, j] != null)
                    return dp[i, j].Value;

                long ans;

                // If the current characters match,
                // either use the current character or skip it
                if (s[i - 1] == t[j - 1])
                {
                    ans = Solve(i - 1, j - 1) + Solve(i - 1, j);
                }

                // Otherwise, skip the current character of s
                else
                {
                    ans = Solve(i - 1, j);
                }

                // Store the computed answer
                dp[i, j] = ans;

                return ans;
            }

            // Step 3:
            // Return the number of distinct subsequences
            return (int)Solve(s.Length, t.Length);
        }

        // Distinct Subsequences using Tabulation
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m)
        public static int NumDistinctTabulation(string s, string t)
        {
            // Step 1:
            // Create DP table where dp[i,j] stores the number of distinct
            // subsequences of the first i characters of s
            // that form the first j characters of t
            long?[,] dp = new long?[s.Length + 1, t.Length + 1];

            // Step 2:
            // Fill the DP table
            for (int i = 0; i <= s.Length; i++)
            {
                for (int j = 0; j <= t.Length; j++)
                {
                    // An empty target can always be formed
                    // by choosing no characters
                    if (j == 0)
                    {
                        dp[i, j] = 1;
                    }

                    // A non-empty target cannot be formed
                    // from an empty source string
                    else if (i == 0)
                    {
                        dp[i, j] = 0;
                    }

                    // If the current characters match,
                    // either include the current character
                    // or skip it
                    else if (s[i - 1] == t[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + dp[i - 1, j];
                    }

                    // Otherwise, skip the current character of s
                    else
                    {
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            // Step 3:
            // Return the number of distinct subsequences
            return (int)dp[s.Length, t.Length].Value;
        }

        // Edit Distance using Memoization
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m) + O(n + m) (Recursion Stack)
        public static int EditDistanceMemoization(string s, string t)
        {
            // Step 1:
            // Store the lengths of both strings
            int n1 = s.Length;
            int n2 = t.Length;

            // Step 2:
            // Create DP table where dp[i,j] stores the minimum number of
            // operations required to convert the first i characters of s
            // into the first j characters of t
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3:
            // Initialize valid states as uncomputed
            for (int i = 1; i <= n1; i++)
            {
                for (int j = 1; j <= n2; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 4:
            // Recursive helper function
            int Helper(int i, int j)
            {
                // Both strings are empty
                if (i == 0 && j == 0)
                {
                    return 0;
                }

                // Source string is empty
                // Insert all remaining characters of target
                if (i == 0)
                {
                    return j;
                }

                // Target string is empty
                // Delete all remaining characters of source
                if (j == 0)
                {
                    return i;
                }

                // Return memoized answer if already computed
                if (dp[i, j] != -1)
                {
                    return dp[i, j];
                }

                // If characters match, no operation is needed
                if (s[i - 1] == t[j - 1])
                {
                    dp[i, j] = Helper(i - 1, j - 1);
                }

                // Otherwise, consider all three possible operations
                else
                {
                    // Insert current target character
                    int insert = 1 + Helper(i, j - 1);

                    // Delete current source character
                    int delete = 1 + Helper(i - 1, j);

                    // Replace current source character
                    int replace = 1 + Helper(i - 1, j - 1);

                    // Choose the minimum cost operation
                    dp[i, j] = Math.Min(insert, Math.Min(delete, replace));
                }

                return dp[i, j];
            }

            // Step 5:
            // Return the minimum edit distance
            return Helper(n1, n2);
        }

        // Edit Distance using Tabulation
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m)
        public static int EditTabulation(string s, string t)
        {
            // Step 1:
            // Store the lengths of both strings
            int n1 = s.Length;
            int n2 = t.Length;

            // Step 2:
            // Create DP table where dp[i,j] stores the minimum number of
            // operations required to convert the first i characters of s
            // into the first j characters of t
            int[,] dp = new int[n1 + 1, n2 + 1];

            // Step 3:
            // Fill the DP table
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // If the source string is empty,
                    // insert all characters of the target string
                    if (i == 0)
                    {
                        dp[i, j] = j;
                    }

                    // If the target string is empty,
                    // delete all characters of the source string
                    else if (j == 0)
                    {
                        dp[i, j] = i;
                    }

                    // If the current characters match,
                    // no operation is required
                    else if (s[i - 1] == t[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }

                    // Otherwise, consider all three operations
                    else
                    {
                        // Insert the current target character
                        int insert = 1 + dp[i, j - 1];

                        // Delete the current source character
                        int delete = 1 + dp[i - 1, j];

                        // Replace the current source character
                        int replace = 1 + dp[i - 1, j - 1];

                        // Choose the minimum cost operation
                        dp[i, j] = Math.Min(insert, Math.Min(delete, replace));
                    }
                }
            }

            // Step 4:
            // Return the minimum edit distance
            return dp[n1, n2];
        }


        // Wildcard Matching using Memoization
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m) + O(n + m) (Recursion Stack)
        public static bool WildCardMatchingMemoization(string s1, string s2)
        {
            // Step 1:
            // Store the lengths of both strings
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2:
            // Create DP table where dp[i,j] stores whether
            // the first i characters of s1 match
            // the first j characters of the wildcard pattern s2
            bool?[,] dp = new bool?[n1 + 1, n2 + 1];

            // Step 3:
            // Recursive helper function
            bool Helper(int i, int j)
            {
                // Both strings are completely matched
                if (i == 0 && j == 0)
                {
                    dp[i, j] = true;
                    return true;
                }

                // Source string is empty
                // Remaining pattern must consist only of '*'
                if (i == 0)
                {
                    for (int k = 1; k <= j; k++)
                    {
                        if (s2[k - 1] != '*')
                        {
                            return false;
                        }
                    }

                    return true;
                }

                // Pattern is empty but source string is not
                if (j == 0)
                {
                    return false;
                }

                // Return memoized answer if already computed
                if (dp[i, j].HasValue)
                {
                    return dp[i, j].Value;
                }

                bool ans = false;

                // '*' can match zero or more characters
                if (s2[j - 1] == '*')
                {
                    // Match one character
                    bool left = Helper(i - 1, j);

                    // Match zero characters
                    bool right = Helper(i, j - 1);

                    ans = left || right;
                }

                // '?' matches exactly one character
                else if (s2[j - 1] == '?')
                {
                    ans = Helper(i - 1, j - 1);
                }

                // Current characters must match exactly
                else if (s1[i - 1] == s2[j - 1])
                {
                    ans = Helper(i - 1, j - 1);
                }

                // Store the computed result
                dp[i, j] = ans;

                return ans;
            }

            // Step 4:
            // Return whether the strings match
            return Helper(n1, n2);
        }

        // Wildcard Matching using Tabulation
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m)
        public static bool WildCardMatchingTabulation(string s1, string s2)
        {
            // Step 1:
            // Store the lengths of the source string and pattern
            int n1 = s1.Length;
            int n2 = s2.Length;

            // Step 2:
            // Create DP table where dp[i,j] indicates whether
            // the first i characters of s1 match
            // the first j characters of the wildcard pattern s2
            bool?[,] dp = new bool?[n1 + 1, n2 + 1];

            // Step 3:
            // Fill the DP table
            for (int i = 0; i <= n1; i++)
            {
                for (int j = 0; j <= n2; j++)
                {
                    // Both strings are empty
                    if (i == 0 && j == 0)
                    {
                        dp[i, j] = true;
                    }

                    // Source string is empty
                    // Pattern must contain only '*' characters
                    else if (i == 0)
                    {
                        dp[i, j] = s2[j - 1] == '*'
                            ? dp[i, j - 1]
                            : false;
                    }

                    // Pattern is empty but source string is not
                    else if (j == 0)
                    {
                        dp[i, j] = false;
                    }

                    // '*' can match zero or more characters
                    else if (s2[j - 1] == '*')
                    {
                        dp[i, j] = dp[i - 1, j].Value || dp[i, j - 1].Value;
                    }

                    // '?' matches exactly one character
                    else if (s2[j - 1] == '?')
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }

                    // Current characters match exactly
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }

                    // Characters do not match
                    else
                    {
                        dp[i, j] = false;
                    }
                }
            }

            // Step 4:
            // Return whether the entire string matches the pattern
            return dp[n1, n2].Value;
        }

        // Longest Increasing Subsequence using Memoization
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²) + O(n) (Recursion Stack)
        public static int LongestIncreasingSubSequence(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // Create DP table where:
            // dp[curr, prev] stores the length of the LIS
            // considering elements from 1..curr,
            // where 'prev' is the index of the previously chosen element.
            // prev = n + 1 indicates that no element has been chosen yet.
            int[,] dp = new int[n + 1, n + 2];

            // Step 3:
            // Initialize DP table with -1 (uncomputed)
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n + 1; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 4:
            // Recursive helper function
            int Helper(int curr, int prev)
            {
                // Base case:
                // No elements left to consider
                if (curr == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[curr, prev] != -1)
                {
                    return dp[curr, prev];
                }

                int pick = int.MinValue;

                // Current element can be picked if
                // no previous element has been chosen
                // or it is smaller than the previously picked element
                if (prev == n + 1 || arr[prev - 1] > arr[curr - 1])
                {
                    pick = 1 + Helper(curr - 1, curr);
                }

                // Skip the current element
                int notPick = Helper(curr - 1, prev);

                // Store the better choice
                int ans = Math.Max(pick, notPick);
                dp[curr, prev] = ans;

                return ans;
            }

            // Step 5:
            // Compute the LIS length
            Helper(n, n + 1);

            // Step 6:
            // Reconstruct one possible LIS from the DP table
            List<int> lis = new();

            int curr = n;
            int prev = n + 1;

            while (curr > 0)
            {
                bool canPick =
                    prev == n + 1 ||
                    arr[prev - 1] > arr[curr - 1];

                if (canPick)
                {
                    int pick = 1 + dp[curr - 1, curr];
                    int notPick = dp[curr - 1, prev];

                    // Pick the current element if it gives an optimal answer
                    if (pick >= notPick)
                    {
                        lis.Add(arr[curr - 1]);
                        prev = curr;
                        curr--;
                        continue;
                    }
                }

                // Otherwise skip it
                curr--;
            }

            // lis currently contains the LIS in reverse order.
            // Reverse it if the actual sequence is required.
            // lis.Reverse();

            // Step 7:
            // Return the length of the LIS
            return dp[n, n + 1];
        }

        // Longest Increasing Subsequence using Tabulation
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²)
        public static int LongestIncreasingSubSequenceTabulation(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // Create DP table where:
            // dp[curr, prev] stores the length of the LIS
            // considering the first 'curr' elements,
            // where 'prev' is the index of the previously chosen element.
            // prev = n + 1 means no previous element has been chosen.
            int[,] dp = new int[n + 1, n + 2];

            // Step 3:
            // Fill the DP table from smaller subproblems to larger ones
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n + 1; j++)
                {
                    // Base case:
                    // No elements left to consider
                    if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        int pick = int.MinValue;

                        // Current element can be picked if
                        // no previous element has been selected
                        // or it is smaller than the previously selected element
                        if (j == n + 1 || arr[j - 1] > arr[i - 1])
                        {
                            pick = 1 + dp[i - 1, i];
                        }

                        // Skip the current element
                        int notPick = dp[i - 1, j];

                        // Store the better choice
                        dp[i, j] = Math.Max(pick, notPick);
                    }
                }
            }

            // Step 4:
            // Reconstruct one possible LIS from the DP table
            List<int> lis = new();

            int curr = n;
            int prev = n + 1;

            while (curr > 0)
            {
                bool canPick =
                    prev == n + 1 ||
                    arr[prev - 1] > arr[curr - 1];

                if (canPick)
                {
                    int pick = 1 + dp[curr - 1, curr];
                    int notPick = dp[curr - 1, prev];

                    // Pick the current element if it contributes
                    // to an optimal LIS
                    if (pick >= notPick)
                    {
                        lis.Add(arr[curr - 1]);
                        prev = curr;
                        curr--;
                        continue;
                    }
                }

                // Otherwise skip the current element
                curr--;
            }

            // lis contains the LIS in reverse order.
            // Reverse it if the sequence is needed.
            // lis.Reverse();

            // Step 5:
            // Return the length of the LIS
            return dp[n, n + 1];
        }

        // Longest Increasing Subsequence using Binary Search
        // Time Complexity  : O(n log n)
        // Space Complexity : O(n)
        public static int LongestIncreasingSubSequenceBinarySearch(int[] arr)
        {
            // Step 1:
            // temp[i] stores the smallest possible tail element
            // of an increasing subsequence of length (i + 1)
            List<int> temp = new List<int>();

            // Initialize with the first element
            temp.Add(arr[0]);

            // Step 2:
            // Process the remaining elements
            for (int i = 1; i < arr.Length; i++)
            {
                // If the current element extends the longest subsequence,
                // append it to temp
                if (arr[i] > temp[temp.Count - 1])
                {
                    temp.Add(arr[i]);
                }

                // Otherwise, replace the first element in temp
                // that is greater than or equal to the current element
                // to maintain the smallest possible tail
                else
                {
                    int index = LowerBound(temp, arr[i]);
                    temp[index] = arr[i];
                }
            }

            // Helper function to find the first index
            // whose value is greater than or equal to the target
            int LowerBound(List<int> t, int element)
            {
                int low = 0;
                int high = t.Count - 1;

                while (low <= high)
                {
                    int mid = low + (high - low) / 2;

                    if (t[mid] >= element)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }

                // 'low' is the insertion position
                return low;
            }

            // Step 3:
            // The size of temp is the length of the LIS.
            // Note: temp does NOT necessarily contain the actual LIS.
            return temp.Count;
        }

        // Longest Increasing Subsequence using 1D DP (Space Optimized)
        // Time Complexity  : O(n²)
        // Space Complexity : O(n)
        public static int LongestIncreasingSubSequenceSpaceOptimization(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // dp[i] stores the length of the LIS
            // ending at the i-th element (1-based indexing)
            int[] dp = new int[n + 1];

            // Every element itself forms an LIS of length 1
            for (int i = 0; i <= n; i++)
            {
                dp[i] = 1;
            }

            // Step 3:
            // Build the DP array
            for (int i = 1; i <= n; i++)
            {
                // Try extending the LIS ending at every previous element
                for (int j = 1; j < i; j++)
                {
                    if (arr[j - 1] < arr[i - 1])
                    {
                        dp[i] = Math.Max(dp[i], 1 + dp[j]);
                    }
                }
            }

            // Step 4:
            // Find the maximum LIS length
            int ans = 0;

            for (int i = 1; i <= n; i++)
            {
                ans = Math.Max(ans, dp[i]);
            }

            // Step 5:
            // Reconstruct one possible LIS
            List<int> lis = new();

            int curr = n;
            int prev = -1;

            while (curr > 0)
            {
                // Pick the current element if it can precede the previously
                // selected element in the reconstructed LIS
                if (prev == -1 ||
                    (arr[prev - 1] > arr[curr - 1] &&
                     dp[prev] == dp[curr] + 1))
                {
                    lis.Add(arr[curr - 1]);
                    prev = curr;
                }

                curr--;
            }

            // lis currently contains the LIS in reverse order.
            // Reverse it if the actual sequence is required.
            // lis.Reverse();

            // Step 6:
            // Return the length of the LIS
            return ans;
        }

        // Largest Divisible Subset using Memoization
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²)
        public static int LargestDivisionSubsetMemoization(int[] arr)
        {
            // Step 1:
            // Sort the array so that every valid divisor
            // appears before its multiples
            int n = arr.Length;

            // Step 2:
            // dp[curr, prev] stores the maximum size of the divisible subset
            // considering the first 'curr' elements,
            // where 'prev' is the previously selected element.
            // prev = n + 1 means no previous element has been selected.
            int[,] dp = new int[n + 1, n + 2];

            // Initialize DP table with -1 (uncomputed state)
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n + 1; j++)
                {
                    dp[i, j] = -1;
                }
            }

            int ans = 0;

            // Step 3:
            // Recursive helper
            int Helper(int curr, int prev)
            {
                // Base case:
                // No elements left
                if (curr == 0)
                {
                    return 0;
                }

                // Return memoized result
                if (dp[curr, prev] != -1)
                {
                    return dp[curr, prev];
                }

                int pick = 0;

                // Current element can be picked if
                // there is no previous element selected
                // or the previous element is divisible by the current element
                if (prev == n + 1 || arr[prev - 1] % arr[curr - 1] == 0)
                {
                    pick = 1 + Helper(curr - 1, curr);
                }

                // Skip the current element
                int notPick = Helper(curr - 1, prev);

                // Store the best choice
                dp[curr, prev] = Math.Max(pick, notPick);

                return dp[curr, prev];
            }

            // Step 4:
            // Compute all DP states
            Helper(n, n + 1);

            // Step 5:
            // Reconstruct one possible largest divisible subset
            List<int> subSequence = new List<int>();

            int curr = n;
            int prev = n + 1;

            while (curr > 0)
            {
                bool canPick =
                    prev == n + 1 ||
                    arr[prev - 1] % arr[curr - 1] == 0;

                if (canPick)
                {
                    int pick = 1 + dp[curr - 1, curr];
                    int notPick = dp[curr - 1, prev];

                    // Pick the current element if it contributes
                    // to an optimal divisible subset
                    if (pick >= notPick)
                    {
                        subSequence.Add(arr[curr - 1]);
                        prev = curr;
                        curr--;
                        continue;
                    }
                }

                // Otherwise skip the current element
                curr--;
            }

            // subSequence is in reverse order.
            // Reverse it if the actual subset is required.
            // subSequence.Reverse();

            // Step 6:
            // Return the size of the largest divisible subset
            return dp[n, n + 1];
        }

        // Largest Divisible Subset using Tabulation
        // Time Complexity  : O(n²)
        // Space Complexity : O(n²)
        public static int LargestDivisionSubsetTabulation(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // Create DP table where:
            // dp[curr, prev] stores the maximum size of the divisible subset
            // considering the first 'curr' elements,
            // where 'prev' is the previously selected element.
            // prev = n + 1 means no previous element has been selected.
            int[,] dp = new int[n + 1, n + 2];

            // Step 3:
            // Fill the DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n + 1; j++)
                {
                    // Base case:
                    // No elements left to consider
                    if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        int pick = 0;

                        // Pick the current element if
                        // no previous element has been selected
                        // or the previous element is divisible by the current element
                        if (j == n + 1 || arr[j - 1] % arr[i - 1] == 0)
                        {
                            pick = 1 + dp[i - 1, i];
                        }

                        // Skip the current element
                        int notPick = dp[i - 1, j];

                        // Store the better choice
                        dp[i, j] = Math.Max(pick, notPick);
                    }
                }
            }

            // Step 4:
            // Reconstruct one possible largest divisible subset
            List<int> subSequence = new List<int>();

            int curr = n;
            int prev = n + 1;

            while (curr > 0)
            {
                bool canPick =
                    prev == n + 1 ||
                    arr[prev - 1] % arr[curr - 1] == 0;

                if (canPick)
                {
                    int pick = 1 + dp[curr - 1, curr];
                    int notPick = dp[curr - 1, prev];

                    // Pick the current element if it contributes
                    // to an optimal divisible subset
                    if (pick >= notPick)
                    {
                        subSequence.Add(arr[curr - 1]);
                        prev = curr;
                        curr--;
                        continue;
                    }
                }

                // Otherwise skip the current element
                curr--;
            }

            // subSequence is constructed in reverse order.
            // Reverse it if the actual subset is required.
            // subSequence.Reverse();

            // Step 5:
            // Return the size of the largest divisible subset
            return dp[n, n + 1];
        }
        // Largest Divisible Subset using 1D DP + Parent Array
        // Time Complexity  : O(n²)
        // Space Complexity : O(n)
        public static List<int> LargestSubsetAnotherwaySpaceOptimized(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // dp[i] stores the size of the largest divisible subset
            // ending at the i-th element (1-based indexing)
            int[] dp = new int[n + 1];

            // parent[i] stores the previous element's index
            // in the largest divisible subset ending at i
            int[] parent = new int[n + 1];

            // Step 3:
            // Build the DP and Parent arrays
            for (int i = 1; i <= n; i++)
            {
                // Every element alone forms a subset of size 1
                dp[i] = 1;

                // Initially, no parent exists
                parent[i] = -1;

                // Check every previous element
                for (int j = 1; j < i; j++)
                {
                    // If the current element is divisible by the previous element
                    // and extending that subset produces a better answer
                    if (arr[i - 1] % arr[j - 1] == 0 && dp[j] + 1 > dp[i])
                    {
                        dp[i] = dp[j] + 1;
                        parent[i] = j;
                    }
                }
            }

            // Step 4:
            // Find the ending index of the largest divisible subset
            int maxIndex = 0;

            for (int i = 1; i <= n; i++)
            {
                if (dp[i] > dp[maxIndex])
                {
                    maxIndex = i;
                }
            }

            // Step 5:
            // Reconstruct the subset using the parent array
            List<int> subSequence = new List<int>();

            while (maxIndex != -1)
            {
                subSequence.Add(arr[maxIndex - 1]);
                maxIndex = parent[maxIndex];
            }

            // The subset is constructed in reverse order.
            // Reverse it if the required order is ascending.
            // subSequence.Reverse();

            // Step 6:
            // Return the largest divisible subset
            return subSequence;
        }

        // Longest String Chain using Memoization
        // Time Complexity  : O(n² * L)
        // Space Complexity : O(n²)
        // L = Maximum length of a string
        public static int LongestStringChainMemoization(string[] arr)
        {
            // Step 1:
            // Sort strings by length so that every possible predecessor
            // appears before its successor
            Array.Sort(arr, (a, b) => a.Length.CompareTo(b.Length));

            // Step 2:
            // Store the number of strings
            int n = arr.Length;

            // Step 3:
            // Create DP table where:
            // dp[curr, prev] stores the maximum chain length
            // considering the first 'curr' strings,
            // where 'prev' is the previously selected string.
            // prev = n + 1 means no string has been selected yet.
            int[,] dp = new int[n + 1, n + 2];

            // Initialize DP table with -1 (uncomputed states)
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n + 1; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 4:
            // Recursive helper function
            int Helper(int curr, int prev)
            {
                // Base case:
                // No strings left to consider
                if (curr == 0)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[curr, prev] != -1)
                {
                    return dp[curr, prev];
                }

                int pick = 0;

                // If no previous string has been selected,
                // the current string can always be picked
                if (prev == n + 1)
                {
                    pick = 1 + Helper(curr - 1, curr);
                }
                else
                {
                    // Check whether the current string is a predecessor
                    // of the previously selected string
                    var prevString = arr[prev - 1];

                    if (IsPredecessor(arr[curr - 1], prevString))
                    {
                        pick = 1 + Helper(curr - 1, curr);
                    }
                }

                // Skip the current string
                int notPick = Helper(curr - 1, prev);

                // Store the better choice
                dp[curr, prev] = Math.Max(pick, notPick);

                return dp[curr, prev];
            }

            // Step 5:
            // Compute the maximum string chain
            Helper(n, n + 1);

            // Step 6:
            // Return the length of the longest string chain
            return dp[n, n + 1];
        }

        // Longest String Chain using Tabulation
        // Time Complexity  : O(n² * L)
        // Space Complexity : O(n²)
        // L = Maximum length of a string
        public static int LongestStringChainTabulation(string[] arr)
        {
            // Step 1:
            // Sort strings by length so that every possible predecessor
            // appears before its successor
            Array.Sort(arr, (a, b) => a.Length.CompareTo(b.Length));

            // Step 2:
            // Store the number of strings
            int n = arr.Length;

            // Step 3:
            // Create DP table where:
            // dp[curr, prev] stores the maximum chain length
            // considering the first 'curr' strings,
            // where 'prev' is the previously selected string.
            // prev = n + 1 means no string has been selected yet.
            int[,] dp = new int[n + 1, n + 2];

            // Step 4:
            // Fill the DP table
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= n + 1; j++)
                {
                    // Base case:
                    // No strings left to consider
                    if (i == 0)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        int pick = 0;

                        // If no previous string has been selected,
                        // the current string can always be picked
                        if (j == n + 1)
                        {
                            pick = 1 + dp[i - 1, i];
                        }
                        else
                        {
                            // Check whether the current string is a predecessor
                            // of the previously selected string
                            var prevString = arr[j - 1];

                            if (IsPredecessor(arr[i - 1], prevString))
                            {
                                pick = 1 + dp[i - 1, i];
                            }
                        }

                        // Skip the current string
                        int notPick = dp[i - 1, j];

                        // Store the better choice
                        dp[i, j] = Math.Max(pick, notPick);
                    }
                }
            }

            // Step 5:
            // Return the length of the longest string chain
            return dp[n, n + 1];
        }

        // Longest String Chain using 1D DP (Space Optimized)
        // Time Complexity  : O(n² * L)
        // Space Complexity : O(n)
        // L = Maximum length of a string
        public static int LongestStringChainSpaceOptimization(string[] arr)
        {
            // Step 1:
            // Sort strings by length so that every possible predecessor
            // appears before its successor
            Array.Sort(arr, (a, b) => a.Length.CompareTo(b.Length));

            // Step 2:
            // Store the number of strings
            int n = arr.Length;

            // Step 3:
            // dp[i] stores the length of the longest string chain
            // ending at the i-th string (1-based indexing)
            int[] dp = new int[n + 1];

            // Step 4:
            // Build the DP array
            for (int i = 1; i <= n; i++)
            {
                // Every string alone forms a chain of length 1
                dp[i] = 1;

                // Check every shorter string as a possible predecessor
                for (int j = 1; j < i; j++)
                {
                    // If arr[j-1] is a predecessor of arr[i-1],
                    // extend the chain ending at j
                    if (IsPredecessor(arr[j - 1], arr[i - 1]))
                    {
                        dp[i] = Math.Max(dp[i], 1 + dp[j]);
                    }
                }
            }

            // Step 5:
            // Find the maximum chain length
            int ans = 0;

            for (int i = 1; i <= n; i++)
            {
                ans = Math.Max(ans, dp[i]);
            }

            // Step 6:
            // Return the length of the longest string chain
            return ans;
        }

        // Checks whether 'shorter' is a predecessor of 'longer'
        // i.e., 'longer' can be formed by inserting exactly one character
        // into 'shorter'.
        //
        // Examples:
        // "abc" -> "abac"  => true
        // "abc" -> "abcd"  => true
        // "abc" -> "acbd"  => false
        // Time Complexity  : O(L)
        // Space Complexity : O(1)
        // L = Length of the longer string
        static bool IsPredecessor(string shorter, string longer)
        {
            // The longer string must have exactly one extra character
            if (longer.Length != shorter.Length + 1)
                return false;

            // i -> index for shorter string
            // j -> index for longer string
            int i = 0;
            int j = 0;

            // Indicates whether the extra character has already been skipped
            bool skipped = false;

            // Compare both strings
            while (i < shorter.Length && j < longer.Length)
            {
                // Characters match, move both pointers
                if (shorter[i] == longer[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    // More than one mismatch means
                    // shorter cannot be a predecessor
                    if (skipped)
                        return false;

                    // Skip the extra character in the longer string
                    skipped = true;
                    j++;
                }
            }

            // If the loop completes, at most one character
            // was skipped, so shorter is a valid predecessor
            return true;
        }


        // Longest Bitonic Subsequence using 1D DP
        // Time Complexity  : O(n²)
        // Space Complexity : O(n)
        public static int LongestBitonicSequenceSpaceOptimization(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // increasingDp[i] stores the length of the
            // Longest Increasing Subsequence ending at i (1-based indexing)
            int[] increasingDp = new int[n + 1];

            // decreasingDp[i] stores the length of the
            // Longest Decreasing Subsequence starting from i (1-based indexing)
            int[] decreasingDp = new int[n + 1];

            // Step 3:
            // Compute LIS ending at every index
            for (int i = 1; i <= n; i++)
            {
                // Every element itself forms an increasing subsequence of length 1
                increasingDp[i] = 1;

                // Try extending every previous increasing subsequence
                for (int j = 1; j < i; j++)
                {
                    if (arr[j - 1] < arr[i - 1])
                    {
                        increasingDp[i] = Math.Max(increasingDp[i], 1 + increasingDp[j]);
                    }
                }
            }

            // Step 4:
            // Compute LDS starting from every index
            for (int i = n; i >= 1; i--)
            {
                // Every element itself forms a decreasing subsequence of length 1
                decreasingDp[i] = 1;

                // Try extending every decreasing subsequence on the right
                for (int j = n; j > i; j--)
                {
                    if (arr[j - 1] < arr[i - 1])
                    {
                        decreasingDp[i] = Math.Max(decreasingDp[i], decreasingDp[j] + 1);
                    }
                }
            }

            // Step 5:
            // Treat every element as the peak of the bitonic sequence
            // Total length = LIS + LDS - 1
            // (subtract 1 because the peak element is counted twice)
            int ans = 0;

            for (int i = 1; i <= n; i++)
            {
                ans = Math.Max(ans, increasingDp[i] + decreasingDp[i] - 1);
            }

            // Step 6:
            // Return the length of the longest bitonic subsequence
            return ans;
        }

        // Number of Longest Increasing Subsequences
        // Time Complexity  : O(n²)
        // Space Complexity : O(n)
        public static int NumberOflongestIncreasingSubSequences(int[] arr)
        {
            // Step 1:
            // Store the length of the array
            int n = arr.Length;

            // Step 2:
            // len[i]  -> Length of the LIS ending at index i (1-based indexing)
            // path[i] -> Number of LIS having length len[i] and ending at index i
            int[] len = new int[n + 1];
            int[] path = new int[n + 1];

            // Stores the maximum LIS length found so far
            int maxLength = 0;

            // Step 3:
            // Compute LIS length and count ending at every index
            for (int i = 1; i <= n; i++)
            {
                // Every element alone forms an LIS of length 1
                len[i] = 1;

                // There is initially one way to form that LIS
                path[i] = 1;

                // Try extending all previous increasing subsequences
                for (int j = 1; j < i; j++)
                {
                    if (arr[i - 1] > arr[j - 1])
                    {
                        // Found a longer LIS ending at i
                        if (len[j] + 1 > len[i])
                        {
                            len[i] = len[j] + 1;

                            // Inherit the number of ways from j
                            path[i] = path[j];
                        }

                        // Found another LIS of the same maximum length
                        else if (len[j] + 1 == len[i])
                        {
                            // Add the number of ways from j
                            path[i] += path[j];
                        }
                    }
                }

                // Update the overall maximum LIS length
                maxLength = Math.Max(maxLength, len[i]);
            }

            // Step 4:
            // Sum the number of LIS ending at all indices
            // having the overall maximum length
            int ans = 0;

            for (int i = 1; i <= n; i++)
            {
                if (len[i] == maxLength)
                {
                    ans += path[i];
                }
            }

            // Step 5:
            // Return the total number of longest increasing subsequences
            return ans;
        }


        //Time: O(n^3)
        //Space: O(n^2) for memoization + O(n) recursion stack
        // Matrix Chain Multiplication using Memoization
        // Time Complexity  : O(n³)
        // Space Complexity : O(n²) + O(n) recursion stack
        public static int MatrixChainMultiplicationRecursion(int[] arr)
        {
            // Step 1:
            // Number of dimensions
            int n = arr.Length;

            // Step 2:
            // dp[i, j] stores the minimum multiplication cost
            // required to multiply matrices from i to j
            int[,] dp = new int[n, n];

            // Initialize DP table with -1 (uncomputed states)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 3:
            // Recursive helper function
            int Helper(int i, int j)
            {
                // Base case:
                // A single matrix requires no multiplication
                if (i == j)
                {
                    return 0;
                }

                // Return memoized answer if already computed
                if (dp[i, j] != -1)
                {
                    return dp[i, j];
                }

                int min = int.MaxValue;

                // Step 4:
                // Try every possible partition between i and j
                for (int k = i; k <= j - 1; k++)
                {
                    // Cost =
                    // Cost of left subchain +
                    // Cost of right subchain +
                    // Cost of multiplying the two resulting matrices
                    int temp =
                        arr[i - 1] * arr[k] * arr[j] +
                        Helper(i, k) +
                        Helper(k + 1, j);

                    // Keep the minimum cost
                    min = Math.Min(min, temp);
                }

                // Store the answer for this subproblem
                dp[i, j] = min;

                return min;
            }

            // Step 5:
            // Compute the minimum multiplication cost
            // for the complete matrix chain
            return Helper(1, n - 1);
        }

        // Matrix Chain Multiplication using Tabulation
        // Time Complexity  : O(n³)
        // Space Complexity : O(n²)
        public static int MatrixChainMultiplicationTabulation(int[] arr)
        {
            // Step 1:
            // Number of dimensions
            int n = arr.Length;

            // Step 2:
            // dp[i, j] stores the minimum multiplication cost
            // required to multiply matrices from i to j
            int[,] dp = new int[n, n];

            // Step 3:
            // Base case:
            // A single matrix requires no multiplication
            for (int i = 0; i < n; i++)
            {
                dp[i, i] = 0;
            }

            // Step 4:
            // Fill the DP table from smaller chains to larger chains.
            // Iterate i backwards so that dp[i][k] is already computed.
            for (int i = n - 1; i >= 1; i--)
            {
                // j always starts after i because
                // chain length must be at least 2
                for (int j = i + 1; j < n; j++)
                {
                    int min = int.MaxValue;

                    // Try every possible partition between i and j
                    for (int k = i; k <= j - 1; k++)
                    {
                        // Cost =
                        // Left chain +
                        // Right chain +
                        // Cost of multiplying the resulting matrices
                        int temp =
                            arr[i - 1] * arr[k] * arr[j] +
                            dp[i, k] +
                            dp[k + 1, j];

                        // Keep the minimum cost
                        min = Math.Min(min, temp);
                    }

                    // Store the minimum multiplication cost
                    dp[i, j] = min;
                }
            }

            // Step 5:
            // Return the minimum cost to multiply
            // the complete matrix chain
            return dp[1, n - 1];
        }

        // Minimum Cost to Cut a Stick using Memoization
        // Time Complexity  : O(m³)
        // Space Complexity : O(m²) + O(m) recursion stack
        // where m = cuts.Length + 2
        public static int MinCostToCutStickMemoization(int n, int[] cuts)
        {
            // Step 1:
            // Create a new array containing
            // 0, all cut positions, and n
            int[] points = new int[cuts.Length + 2];

            points[0] = 0;
            points[points.Length - 1] = n;

            // Sort the cut positions
            Array.Sort(cuts);

            // Copy sorted cuts into the points array
            for (int i = 0; i < cuts.Length; i++)
            {
                points[i + 1] = cuts[i];
            }

            // Step 2:
            // memo[left, right] stores the minimum cost
            // to cut the stick between points[left] and points[right]
            int[,] memo = new int[points.Length, points.Length];

            // Step 3:
            // Recursive helper function
            int Helper(int left, int right)
            {
                // Base case:
                // No cut exists between left and right
                if (right - left <= 1)
                {
                    return 0;
                }

                // Return previously computed answer
                if (memo[left, right] != 0)
                {
                    return memo[left, right];
                }

                int minCost = int.MaxValue;

                // Step 4:
                // Try making every possible cut first
                for (int k = left + 1; k < right; k++)
                {
                    // Cost =
                    // Current stick length +
                    // Cost of left segment +
                    // Cost of right segment
                    int cost =
                        (points[right] - points[left]) +
                        Helper(left, k) +
                        Helper(k, right);

                    // Keep the minimum possible cost
                    minCost = Math.Min(minCost, cost);
                }

                // Store the computed minimum cost
                memo[left, right] = (minCost == int.MaxValue) ? 0 : minCost;

                return memo[left, right];
            }

            // Step 5:
            // Compute the minimum cost for the entire stick
            return Helper(0, points.Length - 1);
        }

        // Minimum Cost to Cut a Stick using Tabulation
        // Time Complexity  : O(m³)
        // Space Complexity : O(m²)
        // where m = cuts.Length + 2
        public static int MinCostToCustSticks(int n, int[] cuts)
        {
            // Step 1:
            // Number of cut positions
            int x = cuts.Length;

            // Step 2:
            // Create an array containing
            // 0, all cuts, and n
            int[] points = new int[x + 2];

            points[0] = 0;
            points[x + 1] = n;

            // Sort the cut positions
            Array.Sort(cuts);

            // Copy sorted cuts into the points array
            for (int i = 0; i < cuts.Length; i++)
            {
                points[i + 1] = cuts[i];
            }

            // Step 3:
            // dp[i, j] stores the minimum cost
            // to cut the stick between points[i] and points[j]
            int[,] dp = new int[points.Length, points.Length];

            // Step 4:
            // Fill the DP table from smaller intervals
            // to larger intervals
            for (int i = points.Length - 1; i >= 0; i--)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    // Base case:
                    // No cuts exist between adjacent points
                    if (j - i <= 1)
                    {
                        continue;
                    }

                    int minCost = int.MaxValue;

                    // Try every possible first cut
                    // between i and j
                    for (int k = i + 1; k < j; k++)
                    {
                        // Cost =
                        // Current stick length +
                        // Left interval +
                        // Right interval
                        int cost =
                            points[j] - points[i] +
                            dp[i, k] +
                            dp[k, j];

                        // Keep the minimum cost
                        minCost = Math.Min(minCost, cost);
                    }

                    // Store the minimum cost
                    dp[i, j] = minCost;
                }
            }

            // Step 5:
            // Return the minimum cost to cut the entire stick
            return dp[0, points.Length - 1];
        }


        // Burst Balloons using Memoization
        // Time Complexity  : O(n³)
        // Space Complexity : O(n²) + O(n) recursion stack
        public static int BurstBaloonsMemoization(int[] arr)
        {
            // Step 1:
            // Number of balloons
            int n = arr.Length;

            // Step 2:
            // Add virtual balloons of value 1 at both ends.
            // This removes boundary checks while calculating coins.
            int[] points = new int[n + 2];
            points[0] = 1;
            points[n + 1] = 1;

            // Copy the original balloons
            for (int i = 0; i < n; i++)
            {
                points[i + 1] = arr[i];
            }

            // Step 3:
            // dp[left, right] stores the maximum coins obtainable
            // by bursting balloons from left to right (inclusive)
            int[,] dp = new int[n + 2, n + 2];

            // Initialize DP table with -1 (uncomputed states)
            for (int i = 0; i < n + 2; i++)
            {
                for (int j = 0; j < n + 2; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Step 4:
            // Recursive helper function
            int Helper(int left, int right)
            {
                // Base case:
                // No balloons left to burst
                if (left > right)
                {
                    return 0;
                }

                // Return memoized answer
                if (dp[left, right] != -1)
                {
                    return dp[left, right];
                }

                int max = 0;

                // Step 5:
                // Assume every balloon i is burst LAST
                // in the interval [left, right]
                for (int i = left; i <= right; i++)
                {
                    // Coins gained by bursting balloon i last
                    int temp =
                        points[left - 1] * points[i] * points[right + 1] +
                        Helper(left, i - 1) +
                        Helper(i + 1, right);

                    // Keep the maximum coins
                    if (temp > max)
                    {
                        max = temp;
                    }
                }

                // Store the computed answer
                dp[left, right] = max;

                return max;
            }

            // Step 6:
            // Compute the maximum coins for all balloons
            return Helper(1, n);
        }

        // Burst Balloons using Tabulation
        // Time Complexity  : O(n³)
        // Space Complexity : O(n²)
        public static int BurstBaloonsTabulation(int[] arr)
        {
            // Step 1:
            // Number of balloons
            int n = arr.Length;

            // Step 2:
            // Add virtual balloons of value 1
            // at both ends to avoid boundary checks
            int[] points = new int[n + 2];
            points[0] = 1;
            points[n + 1] = 1;

            // Copy the original balloon values
            for (int i = 0; i < n; i++)
            {
                points[i + 1] = arr[i];
            }

            // Step 3:
            // dp[left, right] stores the maximum coins
            // obtainable by bursting balloons from
            // left to right (inclusive)
            int[,] dp = new int[n + 2, n + 2];

            // Step 4:
            // Process intervals in increasing order of length
            for (int length = 1; length <= n; length++)
            {
                // Starting index of the interval
                for (int left = 1; left <= n - length + 1; left++)
                {
                    // Ending index of the interval
                    int right = left + length - 1;

                    // Assume every balloon k is burst LAST
                    // in the current interval
                    for (int k = left; k <= right; k++)
                    {
                        // Coins gained =
                        // Coins from bursting k last +
                        // Best answer for left interval +
                        // Best answer for right interval
                        int temp =
                            points[left - 1] * points[k] * points[right + 1] +
                            dp[left, k - 1] +
                            dp[k + 1, right];

                        // Store the maximum coins
                        dp[left, right] = Math.Max(dp[left, right], temp);
                    }
                }
            }

            // Step 5:
            // Return the maximum coins obtainable
            // by bursting all balloons
            return dp[1, n];
        }

        // Evaluate Boolean Expression to True using Memoization
        // Time Complexity  : O(n³)
        // Space Complexity : O(n² * 2) + O(n) recursion stack
        public static int EvaluateBooleanExpressionNoofWaysForTrueMemoization(string expression)
        {
            int mod = 1000000007;

            // dp[i, j, isTrue] stores the number of ways
            // to evaluate expression[i...j] to True (1) or False (0)
            int[,,] dp = new int[expression.Length, expression.Length, 2];

            // Initialize DP table with -1 (uncomputed states)
            for (int i = 0; i < expression.Length; i++)
            {
                for (int j = 0; j < expression.Length; j++)
                {
                    dp[i, j, 0] = -1;
                    dp[i, j, 1] = -1;
                }
            }

            // Recursive helper
            int Helper(int i, int j, int isTrue)
            {
                // Invalid expression
                if (i > j)
                {
                    return 0;
                }

                // Base case:
                // Expression contains only one operand
                if (i == j)
                {
                    if (isTrue == 1)
                    {
                        return expression[i] == 'T' ? 1 : 0;
                    }
                    else
                    {
                        return expression[i] == 'F' ? 1 : 0;
                    }
                }

                // Return memoized answer
                if (dp[i, j, isTrue] != -1)
                {
                    return dp[i, j, isTrue];
                }

                int ans = 0;

                // Partition expression at every operator
                for (int k = i + 1; k < j; k += 2)
                {
                    char op = expression[k];

                    // Number of ways left/right evaluate to True/False
                    int lt = Helper(i, k - 1, 1);
                    int lf = Helper(i, k - 1, 0);
                    int rt = Helper(k + 1, j, 1);
                    int rf = Helper(k + 1, j, 0);

                    // '&' operator
                    if (op == '&')
                    {
                        if (isTrue == 1)
                        {
                            ans = (ans + (lt * rt) % mod) % mod;
                        }
                        else
                        {
                            ans = (
                                ans +
                                (lt * rf) % mod +
                                (lf * rt) % mod +
                                (lf * rf) % mod
                            ) % mod;
                        }
                    }

                    // '|' operator
                    else if (op == '|')
                    {
                        if (isTrue == 1)
                        {
                            ans = (
                                ans +
                                (lt * rt) % mod +
                                (lt * rf) % mod +
                                (lf * rt) % mod
                            ) % mod;
                        }
                        else
                        {
                            ans = (ans + (lf * rf) % mod) % mod;
                        }
                    }

                    // '^' operator
                    else if (op == '^')
                    {
                        if (isTrue == 1)
                        {
                            ans = (
                                ans +
                                (lt * rf) % mod +
                                (lf * rt) % mod
                            ) % mod;
                        }
                        else
                        {
                            ans = (
                                ans +
                                (lt * rt) % mod +
                                (lf * rf) % mod
                            ) % mod;
                        }
                    }
                }

                // Store computed answer
                dp[i, j, isTrue] = ans;

                return ans;
            }

            // Compute the number of ways
            // the entire expression evaluates to True
            return Helper(0, expression.Length - 1, 1);
        }


        // Evaluate Boolean Expression to True using Tabulation
        // Time Complexity  : O(n³)
        // Space Complexity : O(n² * 2)
        public static int EvaluateBooleanExpressionNoofWaysForTrueTabulation(string expression)
        {
            int mod = 1000000007;

            // dp[i, j, isTrue] stores the number of ways
            // expression[i...j] can evaluate to
            // True (1) or False (0)
            int[,,] dp = new int[expression.Length, expression.Length, 2];

            // Initialize DP table
            for (int i = 0; i < expression.Length; i++)
            {
                for (int j = 0; j < expression.Length; j++)
                {
                    dp[i, j, 0] = -1;
                    dp[i, j, 1] = -1;
                }
            }

            // Fill DP table from smaller expressions
            // to larger expressions
            for (int i = expression.Length - 1; i >= 0; i--)
            {
                for (int j = i; j < expression.Length; j++)
                {
                    // Compute both False (0) and True (1)
                    for (int isTrue = 0; isTrue <= 1; isTrue++)
                    {
                        // Base case:
                        // Single operand
                        if (i == j)
                        {
                            dp[i, j, isTrue] =
                                (isTrue == 1)
                                ? (expression[i] == 'T' ? 1 : 0)
                                : (expression[i] == 'F' ? 1 : 0);

                            continue;
                        }

                        int ans = 0;

                        // Partition expression at every operator
                        for (int k = i + 1; k < j; k += 2)
                        {
                            char op = expression[k];

                            // Number of ways left/right evaluate
                            // to True/False
                            int lt = dp[i, k - 1, 1];
                            int lf = dp[i, k - 1, 0];
                            int rt = dp[k + 1, j, 1];
                            int rf = dp[k + 1, j, 0];

                            // '&' operator
                            if (op == '&')
                            {
                                if (isTrue == 1)
                                {
                                    ans = (ans + (lt * rt) % mod) % mod;
                                }
                                else
                                {
                                    ans = (
                                        ans +
                                        (lt * rf) % mod +
                                        (lf * rt) % mod +
                                        (lf * rf) % mod
                                    ) % mod;
                                }
                            }

                            // '|' operator
                            else if (op == '|')
                            {
                                if (isTrue == 1)
                                {
                                    ans = (
                                        ans +
                                        (lt * rt) % mod +
                                        (lt * rf) % mod +
                                        (lf * rt) % mod
                                    ) % mod;
                                }
                                else
                                {
                                    ans = (ans + (lf * rf) % mod) % mod;
                                }
                            }

                            // '^' operator
                            else if (op == '^')
                            {
                                if (isTrue == 1)
                                {
                                    ans = (
                                        ans +
                                        (lt * rf) % mod +
                                        (lf * rt) % mod
                                    ) % mod;
                                }
                                else
                                {
                                    ans = (
                                        ans +
                                        (lt * rt) % mod +
                                        (lf * rf) % mod
                                    ) % mod;
                                }
                            }
                        }

                        // Store the computed answer
                        dp[i, j, isTrue] = ans;
                    }
                }
            }

            // Return the number of ways the
            // entire expression evaluates to True
            return dp[0, expression.Length - 1, 1];
        }


        // Partition Array for Maximum Sum using Recursion
        // Time Complexity  : Exponential (O(k^n))
        // Space Complexity : O(n) recursion stack
        public static int PartitonArrayForBestSum(int[] arr, int k)
        {
            int n = arr.Length;

            // Helper(i) returns the maximum sum
            // obtainable starting from index i
            int Helper(int i)
            {
                // Base case:
                // Reached the end of the array
                if (i == n)
                {
                    return 0;
                }

                int maxSum = 0;

                // Maximum element in the current partition
                int max = 0;

                // Try every possible partition
                // of length 1 to k
                for (int j = i; j < Math.Min(n, i + k); j++)
                {
                    // Update the maximum element
                    max = Math.Max(max, arr[j]);

                    // Current partition length
                    int length = j - i + 1;

                    // Sum obtained by taking this partition
                    int sum = max * length + Helper(j + 1);

                    // Keep the best answer
                    maxSum = Math.Max(maxSum, sum);
                }

                return maxSum;
            }

            // Compute the maximum sum
            return Helper(0);
        }

        // Partition Array for Maximum Sum using Memoization
        // Time Complexity  : O(n * k)
        // Space Complexity : O(n) + O(n) recursion stack
        public static int PartitonArrayForBestSumMemoization(int[] arr, int k)
        {
            int n = arr.Length;

            // dp[i] stores the maximum sum obtainable
            // starting from index i
            int[] dp = new int[n];

            // Initialize all states as uncomputed
            for (int i = 0; i < n; i++)
            {
                dp[i] = -1;
            }

            // Helper(i) returns the maximum sum
            // obtainable from index i to the end
            int Helper(int i)
            {
                // Base case:
                // Reached the end of the array
                if (i == n)
                {
                    return 0;
                }

                // Return memoized answer
                if (dp[i] != -1)
                {
                    return dp[i];
                }

                int maxSum = 0;

                // Maximum element in the current partition
                int max = 0;

                // Try every partition of size 1 to k
                for (int j = i; j < Math.Min(i + k, n); j++)
                {
                    // Update the maximum element
                    // in the current partition
                    max = Math.Max(max, arr[j]);

                    // Length of the current partition
                    int length = j - i + 1;

                    // Sum obtained by taking the current partition
                    // plus the best answer for the remaining array
                    int sum = max * length + Helper(j + 1);

                    // Keep the maximum possible answer
                    maxSum = Math.Max(maxSum, sum);
                }

                // Memoize the answer
                dp[i] = maxSum;

                return maxSum;
            }

            // Compute the maximum partition sum
            return Helper(0);
        }
        // Partition Array for Maximum Sum using Tabulation
        // Time Complexity  : O(n * k)
        // Space Complexity : O(n)
        public static int PartitonArrayForBestSumTabulation(int[] arr, int k)
        {
            int n = arr.Length;

            // dp[i] stores the maximum sum obtainable
            // starting from index i
            int[] dp = new int[n + 1];

            // Base case:
            // No elements left to partition
            dp[n] = 0;

            // Fill DP table from right to left
            for (int i = n - 1; i >= 0; i--)
            {
                int maxSum = 0;

                // Maximum element in the current partition
                int max = 0;

                // Try every partition of size 1 to k
                for (int j = i; j < Math.Min(i + k, n); j++)
                {
                    // Update the maximum element
                    // in the current partition
                    max = Math.Max(max, arr[j]);

                    // Length of the current partition
                    int length = j - i + 1;

                    // Sum obtained by taking the current partition
                    // plus the best answer for the remaining array
                    int sum = max * length + dp[j + 1];

                    // Keep the maximum possible answer
                    maxSum = Math.Max(maxSum, sum);
                }

                // Store the best answer for index i
                dp[i] = maxSum;
            }

            // Maximum sum starting from index 0
            return dp[0];
        }

        // Best Time to Buy and Sell Stock III using Memoization
        // At most 2 transactions are allowed
        // Time Complexity  : O(n * 2 * 3) = O(n)
        // Space Complexity : O(n * 2 * 3) + O(n) recursion stack
        public static int BestTimetoBuyandSellStockIIIMemoization(int[] prices)
        {
            int n = prices.Length;

            // dp[index][canBuy][transactionsLeft]
            // stores the maximum profit from 'index' onwards
            int[,,] dp = new int[n, 2, 3];

            // Initialize DP table with -1 (uncomputed states)
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        dp[i, j, k] = -1;
                    }
                }
            }

            // Helper(index, canBuy, transactionsLeft)
            // returns the maximum profit starting from 'index'
            int Helper(int index, int canBuy, int transactionsLeft)
            {
                // Base case:
                // Reached the end of the array
                // or no transactions are left
                if (index == n || transactionsLeft == 0)
                {
                    return 0;
                }

                // Return memoized answer
                if (dp[index, canBuy, transactionsLeft] != -1)
                {
                    return dp[index, canBuy, transactionsLeft];
                }

                // Option 1:
                // Skip today's action
                int doNothing = Helper(index + 1, canBuy, transactionsLeft);

                int doSomething;

                if (canBuy == 1)
                {
                    // Option 2:
                    // Buy today's stock
                    doSomething =
                        -prices[index] +
                        Helper(index + 1, 0, transactionsLeft);
                }
                else
                {
                    // Option 2:
                    // Sell today's stock
                    // Selling completes one transaction
                    doSomething =
                        prices[index] +
                        Helper(index + 1, 1, transactionsLeft - 1);
                }

                // Store the maximum profit
                dp[index, canBuy, transactionsLeft] =
                    Math.Max(doNothing, doSomething);

                return dp[index, canBuy, transactionsLeft];
            }

            // Start from day 0,
            // can buy initially,
            // with 2 transactions available
            return Helper(0, 1, 2);
        }

        // Best Time to Buy and Sell Stock III using Tabulation
        // At most 2 transactions are allowed
        // Time Complexity  : O(n * 2 * 3) = O(n)
        // Space Complexity : O(n * 2 * 3)
        public static int BestTimetoBuyandSellStockIIITabulation(int[] prices)
        {
            int n = prices.Length;

            // dp[index][canBuy][transactionsLeft]
            // stores the maximum profit starting from 'index'
            int[,,] dp = new int[n + 1, 2, 3];

            // Base cases are already initialized to 0:
            // 1. index == n (no days left)
            // 2. transactionsLeft == 0 (no transactions remaining)

            // Fill the DP table from the last day to the first
            for (int index = n - 1; index >= 0; index--)
            {
                // Evaluate both buying and selling states
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // At least one transaction must be available
                    for (int transactionsLeft = 1; transactionsLeft <= 2; transactionsLeft++)
                    {
                        // Option 1:
                        // Skip today's action
                        int doNothing = dp[index + 1, canBuy, transactionsLeft];

                        int doSomething;

                        if (canBuy == 1)
                        {
                            // Option 2:
                            // Buy today's stock
                            doSomething =
                                -prices[index] +
                                dp[index + 1, 0, transactionsLeft];
                        }
                        else
                        {
                            // Option 2:
                            // Sell today's stock
                            // Selling consumes one transaction
                            doSomething =
                                prices[index] +
                                dp[index + 1, 1, transactionsLeft - 1];
                        }

                        // Store the best possible profit
                        dp[index, canBuy, transactionsLeft] =
                            Math.Max(doNothing, doSomething);
                    }
                }
            }

            // Start from day 0,
            // can buy initially,
            // with 2 transactions available
            return dp[0, 1, 2];
        }

        // Best Time to Buy and Sell Stock III using Space Optimization
        // At most 2 transactions are allowed
        // Time Complexity  : O(n * 2 * 3) = O(n)
        // Space Complexity : O(2 * 3) = O(1)
        public static int BestTimetoBuyandSellStockIIISpaceOptimization(int[] prices)
        {
            int n = prices.Length;

            // dp[canBuy][transactionsLeft]
            // Stores the answers for the next day (index + 1)
            int[,] dp = new int[2, 3];

            // newDp[canBuy][transactionsLeft]
            // Stores the answers for the current day (index)
            int[,] newDp = new int[2, 3];

            // Process the days from last to first
            for (int index = n - 1; index >= 0; index--)
            {
                // Compute both buying and selling states
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // At least one transaction must remain
                    for (int transactionsLeft = 1; transactionsLeft <= 2; transactionsLeft++)
                    {
                        // Option 1:
                        // Skip today's action
                        int doNothing = dp[canBuy, transactionsLeft];

                        int doSomething;

                        if (canBuy == 1)
                        {
                            // Option 2:
                            // Buy today's stock
                            doSomething =
                                -prices[index] +
                                dp[0, transactionsLeft];
                        }
                        else
                        {
                            // Option 2:
                            // Sell today's stock
                            // Selling consumes one transaction
                            doSomething =
                                prices[index] +
                                dp[1, transactionsLeft - 1];
                        }

                        // Store the better choice
                        newDp[canBuy, transactionsLeft] =
                            Math.Max(doNothing, doSomething);
                    }
                }

                // Current day's DP becomes the next day's DP
                Array.Copy(newDp, dp, newDp.Length);
            }

            // Start from day 0,
            // can buy initially,
            // with 2 transactions available
            return dp[1, 2];
        }

        public static int BuyAndSelllStoctIV(int[] prices, int k)
        {
            int n = prices.Length;
            int[,,] dp = new int[n + 1, 2, k + 1];

            int Helper(int index, int canBuy, int transactionsLeft)
            {
                if (index == n || transactionsLeft == 0)
                {
                    return 0;
                }

                if (dp[index, canBuy, transactionsLeft] != 0)
                {
                    return dp[index, canBuy, transactionsLeft];
                }

                int doNothing = Helper(index + 1, canBuy, transactionsLeft);
                int doSomething;
                if (canBuy == 1)
                {
                    doSomething = -prices[index] + Helper(index + 1, 0, transactionsLeft);
                }
                else
                {
                    doSomething = prices[index] + Helper(index + 1, 1, transactionsLeft - 1);
                }

                dp[index, canBuy, transactionsLeft] = Math.Max(doNothing, doSomething);
                return dp[index, canBuy, transactionsLeft];
            }
            return Helper(0, 1, k);
        }

        // Best Time to Buy and Sell Stock IV using Tabulation
        // At most k transactions are allowed
        // Time Complexity  : O(n * 2 * k)
        // Space Complexity : O(n * 2 * k)
        public static int BuyAndSelllStoctIVTabulation(int[] prices, int k)
        {
            int n = prices.Length;

            // dp[index][canBuy][transactionsLeft]
            // stores the maximum profit possible from 'index' onwards
            int[,,] dp = new int[n + 1, 2, k + 1];

            // Base cases:
            // 1. index == n  -> no days remaining, profit = 0
            // 2. transactionsLeft == 0 -> no transactions available, profit = 0
            // Arrays are initialized with 0 by default in C#

            // Fill the DP table from the last day to the first day
            for (int index = n - 1; index >= 0; index--)
            {
                // Calculate both states:
                // canBuy = 1 -> We can buy a stock
                // canBuy = 0 -> We currently hold a stock and can sell
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // Try all possible remaining transactions
                    for (int transactionsLeft = 1; transactionsLeft <= k; transactionsLeft++)
                    {
                        // Option 1:
                        // Do nothing on the current day
                        int doNothing = dp[index + 1, canBuy, transactionsLeft];

                        int doSomething;

                        if (canBuy == 1)
                        {
                            // Option 2:
                            // Buy the stock today
                            // Money spent = prices[index]
                            // Move to selling state
                            doSomething =
                                -prices[index] +
                                dp[index + 1, 0, transactionsLeft];
                        }
                        else
                        {
                            // Option 2:
                            // Sell the stock today
                            // Selling completes one transaction
                            // So decrease transactionsLeft by 1
                            doSomething =
                                prices[index] +
                                dp[index + 1, 1, transactionsLeft - 1];
                        }

                        // Store maximum profit among:
                        // 1. Skipping current day
                        // 2. Buying/Selling current day
                        dp[index, canBuy, transactionsLeft] =
                            Math.Max(doNothing, doSomething);
                    }
                }
            }

            // Start from day 0,
            // initially we can buy,
            // with k transactions available
            return dp[0, 1, k];
        }

        // Best Time to Buy and Sell Stock IV using Space Optimization
        // At most k transactions are allowed
        // Time Complexity  : O(n * 2 * k)
        // Space Complexity : O(2 * k)
        public static int BuyAndSelllStoctIVSpaceOptimization(int[] prices, int k)
        {
            int n = prices.Length;

            // dp[canBuy][transactionsLeft]
            // Stores the result for the next day (index + 1)
            int[,] dp = new int[2, k + 1];

            // newDp[canBuy][transactionsLeft]
            // Stores the result for the current day (index)
            int[,] newDp = new int[2, k + 1];

            // Traverse days from last to first
            for (int index = n - 1; index >= 0; index--)
            {
                // Calculate both possible states:
                // canBuy = 1 -> We can buy
                // canBuy = 0 -> We can sell
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // Try all possible remaining transactions
                    for (int transactionsLeft = 1; transactionsLeft <= k; transactionsLeft++)
                    {
                        // Option 1:
                        // Skip today's action
                        int doNothing = dp[canBuy, transactionsLeft];

                        int doSomething;

                        if (canBuy == 1)
                        {
                            // Option 2:
                            // Buy stock today
                            // Move to selling state
                            doSomething =
                                -prices[index] +
                                dp[0, transactionsLeft];
                        }
                        else
                        {
                            // Option 2:
                            // Sell stock today
                            // One transaction is completed
                            doSomething =
                                prices[index] +
                                dp[1, transactionsLeft - 1];
                        }

                        // Store the maximum profit for current day
                        newDp[canBuy, transactionsLeft] =
                            Math.Max(doNothing, doSomething);
                    }
                }

                // Current day's result becomes the next day's result
                // for processing the previous index
                Array.Copy(newDp, dp, newDp.Length);
            }

            // Start from day 0:
            // Initially we can buy and have k transactions available
            return dp[1, k];
        }


        // Best Time to Buy and Sell Stock with Cooldown using Memoization
        // After selling a stock, we must wait one day before buying again
        // Time Complexity  : O(n * 2)
        // Space Complexity : O(n * 2) + O(n) recursion stack

        public static int BestTimeToBuyAndSellStocV(int[] prices)
        {
            int n = prices.Length;

            // dp[index][canBuy]
            // stores maximum profit from 'index' onwards
            // canBuy = 1 -> We can buy a stock
            // canBuy = 0 -> We currently hold a stock and can sell
            int[,] dp = new int[n + 1, 2];

            // Initialize DP with -1
            // -1 represents that the state is not calculated yet
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Helper(index, canBuy)
            // returns maximum profit from current index
            int Helper(int index, int canBuy)
            {
                // Base case:
                // No days left
                if (index >= n)
                {
                    return 0;
                }

                // Return already calculated result
                if (dp[index, canBuy] != -1)
                {
                    return dp[index, canBuy];
                }

                // Option 1:
                // Skip today's action
                int doNothing = Helper(index + 1, canBuy);

                int doSomething;

                if (canBuy == 1)
                {
                    // Option 2:
                    // Buy today's stock
                    // Money spent = prices[index]
                    // Move to sell state
                    doSomething =
                        -prices[index] +
                        Helper(index + 1, 0);
                }
                else
                {
                    // Option 2:
                    // Sell today's stock
                    // After selling, cooldown day is skipped
                    // So move to index + 2
                    doSomething =
                        prices[index] +
                        Helper(index + 2, 1);
                }

                // Store best choice
                dp[index, canBuy] =
                    Math.Max(doNothing, doSomething);

                return dp[index, canBuy];
            }

            // Start from day 0 and we can buy initially
            return Helper(0, 1);
        }

        // Best Time to Buy and Sell Stock with Cooldown using Tabulation
        // After selling a stock, we have one cooldown day where we cannot buy
        // Time Complexity  : O(n * 2) = O(n)
        // Space Complexity : O(n * 2) = O(n)

        public static int BestTimeToBuyAndSellStockV(int[] prices)
        {
            int n = prices.Length;

            // dp[index][canBuy]
            // Stores maximum profit from 'index' onwards
            //
            // canBuy = 1 -> We can buy a stock
            // canBuy = 0 -> We currently hold a stock and can sell
            //
            // n + 2 size is used because while selling:
            // we move to index + 2 due to cooldown day
            int[,] dp = new int[n + 2, 2];

            // Base cases:
            // dp[n][0/1] = 0
            // dp[n+1][0/1] = 0
            // No days left means no profit
            // Arrays are initialized with 0 by default

            // Fill DP table from last day to first day
            for (int index = n - 1; index >= 0; index--)
            {
                // Calculate both states:
                // canBuy = 0 -> Sell state
                // canBuy = 1 -> Buy state
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // Option 1:
                    // Skip today's action
                    int doNothing = dp[index + 1, canBuy];

                    int doSomething;

                    if (canBuy == 1)
                    {
                        // Option 2:
                        // Buy today's stock
                        // Spend money and move to sell state
                        doSomething =
                            -prices[index] +
                            dp[index + 1, 0];
                    }
                    else
                    {
                        // Option 2:
                        // Sell today's stock
                        // After selling, next day is cooldown
                        // So jump to index + 2
                        doSomething =
                            prices[index] +
                            dp[index + 2, 1];
                    }

                    // Store maximum profit between:
                    // 1. Skip
                    // 2. Buy/Sell
                    dp[index, canBuy] =
                        Math.Max(doNothing, doSomething);
                }
            }

            // Start from day 0:
            // Initially we are allowed to buy
            return dp[0, 1];
        }

        // Best Time to Buy and Sell Stock with Transaction Fee using Memoization
        // Every sell transaction has a fixed transaction fee
        // Time Complexity  : O(n * 2)
        // Space Complexity : O(n * 2) + O(n) recursion stack

        public static int BestTimeToBuyAndSellStockWithTransactionFeeMemoization(int[] prices, int fee)
        {
            int n = prices.Length;

            // dp[index][canBuy]
            // Stores maximum profit from 'index' onwards
            //
            // canBuy = 1 -> We are allowed to buy
            // canBuy = 0 -> We currently hold a stock and can sell
            int[,] dp = new int[n, 2];

            // Initialize memoization table
            // -1 represents that the state is not calculated yet
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    dp[i, j] = -1;
                }
            }

            // Helper(index, canBuy)
            // Returns maximum profit starting from current index
            int Helper(int index, int canBuy)
            {
                // Base case:
                // No days remaining
                if (index == n)
                {
                    return 0;
                }

                // Return already calculated result
                if (dp[index, canBuy] != -1)
                {
                    return dp[index, canBuy];
                }

                // Option 1:
                // Skip today's action
                int doNothing = Helper(index + 1, canBuy);

                int doSomething;

                if (canBuy == 1)
                {
                    // Option 2:
                    // Buy today's stock
                    // Money spent = prices[index]
                    // Move to sell state
                    doSomething =
                        -prices[index] +
                        Helper(index + 1, 0);
                }
                else
                {
                    // Option 2:
                    // Sell today's stock
                    // Transaction fee is deducted while selling
                    // Move back to buy state
                    doSomething =
                        prices[index] -
                        fee +
                        Helper(index + 1, 1);
                }

                // Store maximum profit for current state
                dp[index, canBuy] =
                    Math.Max(doNothing, doSomething);

                return dp[index, canBuy];
            }

            // Start from day 0:
            // Initially we are allowed to buy
            return Helper(0, 1);
        }

        // Best Time to Buy and Sell Stock with Transaction Fee using Tabulation
        // Every sell transaction has a fixed transaction fee
        // Time Complexity  : O(n * 2) = O(n)
        // Space Complexity : O(n * 2) = O(n)

        public static int BestTimeToBuyAndSellStockWithTransactionFeeTabulation(int[] prices, int fee)
        {
            int n = prices.Length;

            // dp[index][canBuy]
            // Stores maximum profit from 'index' onwards
            //
            // canBuy = 1 -> We can buy a stock
            // canBuy = 0 -> We currently hold a stock and can sell
            int[,] dp = new int[n + 1, 2];

            // Base case:
            // dp[n][0] = 0
            // dp[n][1] = 0
            // No days remaining means no profit
            // Array is initialized with 0 by default

            // Fill the DP table from last day to first day
            for (int index = n - 1; index >= 0; index--)
            {
                // Calculate both states:
                // canBuy = 1 -> Buy state
                // canBuy = 0 -> Sell state
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // Option 1:
                    // Skip today's action
                    int doNothing = dp[index + 1, canBuy];

                    int doSomething;

                    if (canBuy == 1)
                    {
                        // Option 2:
                        // Buy today's stock
                        // Spend money and move to sell state
                        doSomething =
                            -prices[index] +
                            dp[index + 1, 0];
                    }
                    else
                    {
                        // Option 2:
                        // Sell today's stock
                        // Deduct transaction fee while selling
                        // Move back to buy state
                        doSomething =
                            prices[index] -
                            fee +
                            dp[index + 1, 1];
                    }

                    // Store maximum profit:
                    // Either skip or perform transaction
                    dp[index, canBuy] =
                        Math.Max(doNothing, doSomething);
                }
            }

            // Start from day 0:
            // Initially we can buy
            return dp[0, 1];
        }

        // Best Time to Buy and Sell Stock with Transaction Fee using Space Optimization
        // Every sell transaction has a fixed transaction fee
        // Time Complexity  : O(n * 2) = O(n)
        // Space Complexity : O(2) = O(1)

        public static int BestTimeToBuyAndSellStockWithTransactionFeeSpaceOptimization(int[] prices, int fee)
        {
            int n = prices.Length;

            // dp[canBuy][0]
            // Stores the profit values for the next day (index + 1)
            //
            // canBuy = 1 -> We can buy
            // canBuy = 0 -> We can sell
            int[,] dp = new int[2, 1];

            // newDp stores the current day's calculated values
            int[,] newDp = new int[2, 1];

            // Traverse days from last to first
            for (int index = n - 1; index >= 0; index--)
            {
                // Calculate both states:
                // canBuy = 1 -> Buy state
                // canBuy = 0 -> Sell state
                for (int canBuy = 0; canBuy <= 1; canBuy++)
                {
                    // Option 1:
                    // Skip today's action
                    int doNothing = dp[canBuy, 0];

                    int doSomething;

                    if (canBuy == 1)
                    {
                        // Option 2:
                        // Buy today's stock
                        // Move to sell state
                        doSomething =
                            -prices[index] +
                            dp[0, 0];
                    }
                    else
                    {
                        // Option 2:
                        // Sell today's stock
                        // Deduct transaction fee
                        // Move back to buy state
                        doSomething =
                            prices[index] -
                            fee +
                            dp[1, 0];
                    }

                    // Store maximum profit for current state
                    newDp[canBuy, 0] =
                        Math.Max(doNothing, doSomething);
                }

                // Current day's result becomes previous day's result
                // for the next iteration
                Array.Copy(newDp, dp, newDp.Length);
            }

            // Start from day 0:
            // Initially we can buy
            return dp[1, 0];
        }


        // Count Number of Square Submatrices with All Ones
        // Uses Dynamic Programming
        //
        // Time Complexity  : O(n * m)
        // Space Complexity : O(n * m)

        public static int NoOdSquaresInMatrix(int[][] grid)
        {
            int n = grid.Length;
            int m = grid[0].Length;

            // dp[i][j] stores the size of the largest square
            // ending at cell (i, j) where all values are 1
            //
            // Example:
            // dp[i][j] = 3 means a 3x3 square of 1s ends at (i,j)
            int[,] dp = new int[n, m];

            // Stores total number of square submatrices
            int result = 0;

            // Traverse every cell in the matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    // Only cells containing 1 can form squares
                    if (grid[i][j] == 1)
                    {
                        // First row or first column:
                        // A single cell itself forms a 1x1 square
                        if (i == 0 || j == 0)
                        {
                            dp[i, j] = 1;
                        }
                        else
                        {
                            // The current square size depends on:
                            //
                            // Top        -> dp[i-1][j]
                            // Left       -> dp[i][j-1]
                            // Diagonal   -> dp[i-1][j-1]
                            //
                            // Minimum gives the limiting side length
                            // because all three neighboring squares must exist
                            dp[i, j] =
                                Math.Min(
                                    Math.Min(dp[i - 1, j], dp[i, j - 1]),
                                    dp[i - 1, j - 1]
                                ) + 1;
                        }

                        // If dp[i][j] = x,
                        // then this cell contributes:
                        //
                        // x number of squares ending at this cell
                        //
                        // Example:
                        // dp[i][j] = 3
                        //
                        // contributes:
                        // 1x1 square
                        // 2x2 square
                        // 3x3 square
                        result += dp[i, j];
                    }
                }
            }

            return result;
        }
    }

}
