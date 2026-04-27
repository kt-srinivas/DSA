using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static  class BackTracking
    {
        //controlled recursion.
        public static List<List<int>> GetAllSubSetsDistinctElements(int[] arr)
        {
            List<List<int>> result = new List<List<int>>();
            GetAllSubsetsDistinctElementsHelper(arr, 0, new List<int>(), result);
            return result;
        }
        public static void GetAllSubsetsDistinctElementsHelper(int[] arr, int index, List<int> current, List<List<int>> result)
        {
            result.Add(new List<int>(current));
            for (int i = index; i < arr.Length; i++)
            {
                //Add the current element to the subset
                current.Add(arr[i]);

                //Recursively call the helper function to explore further subsets
                GetAllSubsetsDistinctElementsHelper(arr, i + 1, current, result);

                //Backtrack by removing the last element added to the subset
                current.RemoveAt(current.Count - 1);
            }
        }

        public static List<List<int>> GteAllPermutationsDistinct(int[] arr)
        {
            List<List<int>> result = new List<List<int>>();
            bool[] isUsed = new bool[arr.Length];
            for(int i =0; i < arr.Length; i++)
            {
                isUsed[i] = false;
            }
            GetAllPermutationsHelper(arr, 0, new List<int>(), result,isUsed);
            return result;
        }

        public static void GetAllPermutationsHelper(int[] arr, int index, List<int> current, List<List<int>> result, bool[] isUSed)
        {
           if(index == arr.Length)
            {
                result.Add(new List<int>(current));
            }
           for(int i=0;i< arr.Length; i++)
            {
                if (isUSed[i])
                {
                    continue;
                }

                //Mark the current element as used and add it to the current permutation
                isUSed[i] = true;
                //Add the element to the current permutation
                current.Add(arr[i]);

                //Recursively call the helper function to explore further permutations
                GetAllPermutationsHelper(arr, index + 1, current, result,isUSed);

                //Backtrack by removing the last element added to the current permutation and marking it as unused
                current.RemoveAt(current.Count - 1);
                isUSed[i] = false;
           }
        }

        public static List<List<int>> GetAllSubSetsRepeatedElements(int[] arr)
        {
            List<List<int>> result = new List<List<int>>();
            GetAllSubsetsRepeatedElementsHelper(arr, 0, new List<int>(), result);
            return result;
        }

        public static void GetAllSubsetsRepeatedElementsHelper(int[] arr, int index, List<int> current, List<List<int>> result)
        {
            result.Add(new List<int>(current));
            for (int i = index; i < arr.Length; i++)
            {
                //Skip duplicates(At the same depth if the elments are same skip those)
                if (i > index && arr[i] == arr[index])
                {
                    continue;
                }

                //Add the current element to the subset
                current.Add(arr[i]);

                //Recursively call the helper function to explore further subsets
                GetAllSubsetsRepeatedElementsHelper(arr, i + 1, current, result);

                //Backtrack by removing the last element added to the subset
                current.RemoveAt(current.Count - 1);
            }
        }

        public static List<List<int>> GetAllPermutationsRepeated(int[] arr)
        {
            List<List<int>> result = new List<List<int>>();
            bool[] isUsed = new bool[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                isUsed[i] = false;
            }
            GetAllPermutationsRepeatedHelper(arr, 0, new List<int>(), result, isUsed);
            return result;
        }
        public static void GetAllPermutationsRepeatedHelper(int[] arr, int index, List<int> current, List<List<int>> result, bool[] isUsed)
        {
            if (index == arr.Length)
            {
                result.Add(new List<int>(current));
            }
            for (int i = 0; i < arr.Length; i++)
            {
                //Skip duplicates(At the same depth if the elments are same skip those)
                if (i > 0 && arr[i] == arr[i - 1] && !isUsed[i - 1])
                {
                    continue;
                }

                if (isUsed[i])
                {
                    continue;
                }

                //Mark the current element as used and add it to the current permutation
                isUsed[i] = true;
                //Add the element to the current permutation
                current.Add(arr[i]);

                //Recursively call the helper function to explore further permutations
                GetAllPermutationsRepeatedHelper(arr, index + 1, current, result, isUsed);

                //Backtrack by removing the last element added to the current permutation and marking it as unused
                current.RemoveAt(current.Count - 1);
                isUsed[i] = false;
            }
        }

        public static List<List<int>> GetAllCombinationsForTargetCanRepeatTheElements(int[] arr, int target)
        {
            List<List<int>> result = new List<List<int>>();
            GetAllCombinationsForTargetHelper(arr, target, 0, new List<int>(), result,0);
            return result;
        }
        public static void GetAllCombinationsForTargetHelper(int[] arr, int target, int index, List<int> current, List<List<int>> result, int currentSum)
        {
            if (currentSum == target)
            {
                result.Add(new List<int>(current));
                return;
            }
            if (currentSum > target)
            {
                return;
            }
            for (int i = index; i < arr.Length; i++)
            {
                //Add the current element to the combination
                current.Add(arr[i]);
                currentSum += arr[i];

                //Recursively call the helper function to explore further combinations
                GetAllCombinationsForTargetHelper(arr, target, i, current, result, currentSum);

                //Backtrack by removing the last element added to the combination and updating the current sum
                current.RemoveAt(current.Count - 1);
                currentSum -= arr[i];
            }
        }

            public static IList<IList<string>> SolveNQueens(int n)
            {
                // Final result → list of all valid boards
                var result = new List<IList<string>>();

                // Board representation (2D char array)
                char[][] board = new char[n][];

                // Initialize board with '.'
                for (int i = 0; i < n; i++)
                {
                    board[i] = new string('.', n).ToCharArray();
                }

                // Tracks if a column already has a queen
                bool[] col = new bool[n];

                // Tracks "\" diagonals → row + col
                bool[] diag1 = new bool[2 * n];

                // Tracks "/" diagonals → row - col + n (offset to avoid negative index)
                bool[] diag2 = new bool[2 * n];

                // Start backtracking from row 0
                Backtrack(0, board, result, col, diag1, diag2);

                return result;
            }

            private static void Backtrack(int row, char[][] board, List<IList<string>> result,
                                   bool[] col, bool[] diag1, bool[] diag2)
            {

                // ✅ Base case → all queens placed (one per row)
                if (row == board.Length)
                {

                    // Convert board (char[][]) → List<string>
                    var temp = new List<string>();
                    foreach (var r in board)
                        temp.Add(new string(r));

                    // Add valid configuration to result
                    result.Add(temp);
                    return;
                }

                // Try placing queen in each column of current row
                for (int c = 0; c < board.Length; c++)
                {

                    // ❌ If column or diagonals are already occupied → skip
                    if (col[c] || diag1[row + c] || diag2[row - c + board.Length])
                        continue;

                    // ✅ Place queen
                    board[row][c] = 'Q';

                    // Mark column and diagonals as occupied
                    col[c] = true;
                    diag1[row + c] = true;                 // "\" diagonal
                    diag2[row - c + board.Length] = true;  // "/" diagonal

                    // Recurse for next row
                    Backtrack(row + 1, board, result, col, diag1, diag2);

                    // 🔙 Backtrack → remove queen
                    board[row][c] = '.';

                    // Unmark column and diagonals
                    col[c] = false;
                    diag1[row + c] = false;
                    diag2[row - c + board.Length] = false;
                }
            }
        
    }
}
