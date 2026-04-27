using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class _2DArrays
    {
        //Traverse Array Row and Column Wise
        public static void TraverseArray(int[][]arr)
        {
            //Whatever index is constant have that in outer loop
            //Row wise
            Console.WriteLine("RowWise");
            for (int i=0; i<arr.Length; i++)
            {
                for(int j = 0; j < arr[0].Length; j++)
                {
                    Console.Write(arr[i][j] + " ");
                }
            }

            Console.WriteLine("\nColumn Wise");
            //Colums Wise
            for (int i=0; i<arr.Length;i++)
            {
                for (int j = 0;j < arr[i].Length; j++)
                {
                    Console.Write(arr[j][i]+ " ");
                }
            }
        }

        public static void Transpose(int[][]arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                for(int j = i; j < arr[0].Length; j++)
                {
                    int temp = arr[i][j];
                    arr[i][j] = arr[j][i];
                    arr[j][i] = temp;
                }
            }

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[0].Length; j++)
                {
                    Console.Write(arr[i][j] + " ");
                }
                Console.WriteLine("\n");
            }


        }

        //searchin matrix sorted  row and column wise.Time COmplexity = O(n+m) where n is number of rows and m is number of columns
        //BF: O(n*m)
        //Using  biinary search on each row: O(nlogm)
        public static bool SearchInSortedMatrix(int[][] arr, int target)
        {
            int row = 0;
            int col = arr[0].Length - 1;
            while (row < arr.Length && col >= 0)
            {
                if (arr[row][col] == target)
                {
                    return true;
                }
                else if (arr[row][col] > target)
                {
                    col--;
                }
                else
                {
                    row++;
                }
            }
            return false;
        }

        public static void TraverseInWavePatternRow(int[][] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < arr[0].Length; j++)
                    {
                        Console.Write(arr[i][j] + " ");
                    }
                }
                else
                {
                    for (int j = arr[0].Length - 1; j >= 0; j--)
                    {
                        Console.Write(arr[i][j] + " ");
                    }
                }
            }
        }

        public static void TraverseInWavePatternColumn(int[][] arr)
        {
            for (int i = 0; i < arr[0].Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        Console.Write(arr[j][i] + " ");
                    }
                }
                else
                {
                    for (int j = arr.Length - 1; j >= 0; j--)
                    {
                        Console.Write(arr[j][i] + " ");
                    }
                }
            }
        }

        public static void SpiralTraverse(int[][] arr)
        {
            int top = 0;
            int bottom = arr.Length - 1;
            int left = 0;
            int right = arr[0].Length - 1;

            while (top <= bottom && left <= right)
            {
                for (int i = left; i <= right; i++)
                {
                    Console.Write(arr[top][i] + " ");
                }
                top++;

                for (int i = top; i <= bottom; i++)
                {
                    Console.Write(arr[i][right] + " ");
                }
                right--;

                if (top <= bottom)
                {
                    for (int i = right; i >= left; i--)
                    {
                        Console.Write(arr[bottom][i] + " ");
                    }
                    bottom--;
                }

                if (left <= right)
                {
                    for (int i = bottom; i >= top; i--)
                    {
                        Console.Write(arr[i][left] + " ");
                    }
                    left++;
                }
            }
        }


        //Rotate 90 degree clockwise
        public static void Rotate90DegreeClockwise(int[][] arr)
        {
            //Transpose
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i; j < arr[0].Length; j++)
                {
                    int temp = arr[i][j];
                    arr[i][j] = arr[j][i];
                    arr[j][i] = temp;
                }
            }

            //Reverse each row
            for (int i = 0; i < arr.Length; i++)
            {
                int left = 0;
                int right = arr[0].Length - 1;
                while (left < right)
                {
                    int temp = arr[i][left];
                    arr[i][left] = arr[i][right];
                    arr[i][right] = temp;
                    left++;
                    right--;
                }
            }

            //Print the rotated matrix
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[0].Length; j++)
                {
                    Console.Write(arr[i][j] + " ");
                }
                Console.WriteLine("\n");
            }
        }

        //Set Matrix Zeroes: If an element is 0, set its entire row and column to 0. Do it in place.
        //BF: O(n*m) + O(n*m) => O(n*m)
        //Better: O(n+m) + O(n*m) => O(n*m)
        public static void SetZeroes(int[][] matrix)
        {

            int rows = matrix.Length;
            int cols = matrix[0].Length;

            // This variable tracks whether the FIRST COLUMN should be zero
            // We cannot use matrix[0][0] for both row & column → conflict
            int colj = 1;

            // 1️⃣ MARK PHASE
            // Use first row & first column as markers
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {

                    if (matrix[i][j] == 0)
                    {

                        // Mark this row
                        matrix[i][0] = 0;

                        if (j != 0)
                        {
                            // Mark this column
                            matrix[0][j] = 0;
                        }
                        else
                        {
                            // Special case → first column
                            colj = 0;
                        }
                    }
                }
            }

            // 2️⃣ FILL PHASE (excluding first row & column)
            // Set cells to zero based on markers
            for (int i = 1; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {

                    if (matrix[i][0] == 0 || matrix[0][j] == 0)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }

            // 3️⃣ HANDLE FIRST ROW
            // If matrix[0][0] is 0 → entire first row should be zero
            if (matrix[0][0] == 0)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[0][j] = 0;
                }
            }

            // 4️⃣ HANDLE FIRST COLUMN
            if (colj == 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    matrix[i][0] = 0;
                }
            }
        }
    }
}
