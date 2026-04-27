using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace DSA.Topics
{
    public static class Stacks
    {
        public static bool ValidParanthesis(string s)
        {
            var stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    stack.Push(s[i]);
                }
                else if (stack.Count != 0 && ((s[i] == ')' && stack.Peek() == '(') || (s[i] == '}' && stack.Peek() == '{') || (s[i] == ']' && stack.Peek() == '[')))
                {
                    stack.Pop();
                }
                else
                {
                    return false;
        
                }
            }
            return true;
        }

        public static string RemoveAdjacentDuplicates(string s)
        {
            var stack = new Stack<char>();
            for (int i = 0; i < s.Length; i++)
            {
                if (stack.Count != 0 && stack.Peek() == s[i])
                {
                    stack.Pop();
                }
                else
                {
                    stack.Push(s[i]);
                }
            }
            var result = new StringBuilder();
            while (stack.Count > 0)
            {
                result.Append(stack.Pop());
            }

            return new string(result.ToString().Reverse().ToArray());

        }

        /// Given an array of integers, find the next greater element for each element in the array. The next greater element for an element x is the first greater element on the right side of x in the array. If there is no greater element for x, then the next greater element for x is -1.
        public static int[] FindGreaterElementsToTheRight(int[] arr)
        {
            int[] result = new int[arr.Length];
            var stack = new Stack<int>();
            for(int i = arr.Length - 1; i >=0; i--)
            {
                if(stack.Count !=0 && stack.Peek() <= arr[i])
                {
                    stack.Pop();
                }
                if (stack.Count == 0)
                {
                    result[i] = -1;
                }
                else
                {
                    result[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return result;
        }

        public static int[] FindLesserElementToTheRight(int[] arr)
        {
            int[] result = new int[arr.Length];
            var stack = new Stack<int>();
            for(int i = arr.Length-1; i >=0; i--)
            {
                if(stack.Count !=0 && stack.Peek() >= arr[i])
                {
                    stack.Pop();
                }
                if(stack.Count == 0)
                {
                    result[i] = -1;
                }
                else
                {
                    result[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return result;
        }


        public static int[] FindGreaterElementToTheeLeft(int[] arr)
        {
            var result = new int[arr.Length];
            var stack = new Stack<int>();
            for(int i = 0; i < arr.Length; i++)
            {
                if(stack.Count !=0 && stack.Peek() <= arr[i])
                {
                    stack.Pop();
                }
                if(stack.Count == 0)
                {
                    result[i] = -1;
                }
                else
                {
                    result[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return result;

        }

        public static int[] FindLesserElementToTheLeft(int[] arr)
        {
            var result = new int[arr.Length];
            var stack = new Stack<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (stack.Count != 0 && stack.Peek() >= arr[i])
                {
                    stack.Pop();
                }
                if (stack.Count == 0)
                {
                    result[i] = -1;
                }
                else
                {
                    result[i] = stack.Peek();
                }
                stack.Push(arr[i]);
            }
            return result;
        }
        
        public static int[] FindNextGreaterElementInCircularArray(int[] arr)
        {
            int[] result = new int[arr.Length];
            var stack = new Stack<int>();
            for (int i = 2 * arr.Length - 1; i >= 0; i--)
            {
                while (stack.Count != 0 && stack.Peek() <= arr[i % arr.Length])
                {
                    stack.Pop();
                }
                if (stack.Count == 0)
                {
                    result[i % arr.Length] = -1;
                }
                else
                {
                    result[i % arr.Length] = stack.Peek();
                }
                stack.Push(arr[i % arr.Length]);
            }
            return result;
        }


        // Given an array of integers representing the heights of bars in a histogram, find the area of the largest rectangle that can be formed within the bounds of the histogram.
        // For example, given the array [2, 1, 5, 6, 2, 3], the largest rectangle has an area of 10 (formed by the bars of heights 5 and 6).
        public static long FindHistogramWithLargestArea(int[] arr)
        {
            long maxArea = 0;                  // Stores the maximum rectangle area found so far
            var stack = new Stack<int>();      // Stack stores INDICES of histogram bars (not heights)

            // Traverse all bars
            for (int i = 0; i < arr.Length; i++)
            {
                // If current bar is smaller than the bar at stack top,
                // it means we found the "Next Smaller Element (NSE)"
                // for the bar at stack top.
                while (stack.Count != 0 && arr[stack.Peek()] > arr[i])
                {
                    int element = stack.Pop();         // Index of bar whose area we calculate now
                    long height = arr[element];        // Height of that bar

                    int nse = i;                      // Current index is Next Smaller Element
                    int pse = stack.Count == 0        // If stack empty, no Previous Smaller Element
                              ? -1
                              : stack.Peek();         // Otherwise top is Previous Smaller Element

                    long width = nse - pse - 1;       // Width between PSE and NSE
                    maxArea = Math.Max(maxArea, height * width);  // Update max area
                }

                // Push current index to stack
                // Stack always maintains increasing heights
                stack.Push(i);
            }

            // After finishing array traversal,
            // some bars might not have found their NSE
            while (stack.Count != 0)
            {
                int element = stack.Pop();            // Index of remaining bar
                long height = arr[element];           // Height of that bar

                int nse = arr.Length;                // No smaller element to right → assume end
                int pse = stack.Count == 0           // Check previous smaller
                          ? -1
                          : stack.Peek();

                long width = nse - pse - 1;          // Compute width
                maxArea = Math.Max(maxArea, height * width);
            }

            return maxArea;                          // Final maximum rectangle area
        }

        public static int FindCelebrity(int[][] arr)
        {
            int top = 0;                         // Start pointer (candidate from top)
            int bottom = arr.Length - 1;         // End pointer (candidate from bottom)

            // STEP 1: Eliminate non-celebrities
            while (top < bottom)
            {
                // If top knows bottom → top CANNOT be celebrity
                if (arr[top][bottom] == 1)
                {
                    top++;                       // Eliminate top
                }
                // Else if bottom knows top → bottom CANNOT be celebrity
                else if (arr[bottom][top] == 1)
                {
                    bottom--;                    // Eliminate bottom
                }
                else
                {
                    // If neither knows each other,
                    // both cannot be celebrity
                    top++;
                    bottom--;
                }
            }

            // If pointers crossed, no valid candidate
            if (top > bottom)
            {
                return -1;
            }

            // Now 'top' (or bottom) is the only remaining candidate
            // STEP 2: Verify if candidate is actually celebrity

            for (int i = 0; i < arr.Length; i++)
            {
                if (i == top)
                    continue;                    // Skip self-check

                // Celebrity conditions:
                // 1. Celebrity should NOT know anyone → arr[top][i] must be 0
                // 2. Everyone should know celebrity → arr[i][top] must be 1
                if (arr[top][i] == 1 || arr[i][top] == 0)
                {
                    return -1;                   // Not a celebrity
                }
            }

            return top;                          // Valid celebrity index
        }

        public static int[] FindStockSpan(int[] arr)
        {
            int[] result = new int[arr.Length];
            var stack = new Stack<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                while (stack.Count != 0 && arr[stack.Peek()] <= arr[i])
                {
                    stack.Pop();
                }
                if (stack.Count == 0)
                {
                    result[i] = i + 1;
                }
                else
                {
                    result[i] = i - stack.Peek();
                }
                stack.Push(i);
            }
            return result;
        }
        public static long FindMaxRectangle(int[][] arr)
        {
            long maxArea = 0;
            int[] histogram = new int[arr[0].Length];
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[0].Length; j++)
                {
                    if (arr[i][j] == 1)
                    {
                        histogram[j] += 1;
                    }
                    else
                    {
                        histogram[j] = 0;
                    }
                }
                maxArea = Math.Max(maxArea, FindHistogramWithLargestArea(histogram));
            }
            return maxArea;

        }
        public static string infixToPostfix(string s)
        {
            // code here
            string ans = "";
            var stack = new Stack<char>();
            int i = 0;
            int n = s.Length;
            while (i < n)
            {
                if ((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' &&
                s[i] <= '9'))
                {
                    ans += s[i];
                }
                else if (s[i] == '(')
                {
                    stack.Push(s[i]);
                }
                else if (s[i] == ')')
                {
                    while (stack.Count != 0 && stack.Peek() != '(')
                    {
                        ans += stack.Peek();
                        stack.Pop();
                    }
                    stack.Pop();
                }
                else
                {
                    while (stack.Count != 0 &&
                    (Priority(s[i]) < Priority(stack.Peek()) ||
                    (Priority(s[i]) == Priority(stack.Peek()) && s[i] != '^')))
                    {
                        ans += stack.Peek();
                        stack.Pop();
                    }
                    stack.Push(s[i]);
                }
                i++;
            }
            while (stack.Count != 0)
            {
                ans += stack.Peek();
                stack.Pop();
            }
            return ans;
        }

        public static int Priority(char c)
        {
            if (c == '^')
            {
                return 3;
            }
            else if (c == '*' || c == '/')
            {
                return 2;
            }
            else if (c == '+' || c == '-')
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static string infixtToPrefix(string s)
        {
            string ans = "";
            var stack = new Stack<char>();
            int i = s.Length - 1;
            while (i >= 0)
            {
                if ((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' &&
                                   s[i] <= '9'))
                {
                    ans = s[i] + ans;
                }
                else if (s[i] == ')')
                {
                    stack.Push(s[i]);
                }
                else if (s[i] == '(')
                {
                    while (stack.Count != 0 && stack.Peek() != ')')
                    {
                        ans = stack.Peek() + ans;
                        stack.Pop();
                    }
                    stack.Pop();
                }
                else
                {
                    while (stack.Count != 0 &&
                                           (Priority(s[i]) < Priority(stack.Peek()) ||
                                                              (Priority(s[i]) == Priority(stack.Peek()) && s[i] != '^')))
                    {
                        ans = stack.Peek() + ans;
                        stack.Pop();
                    }
                    stack.Push(s[i]);
                }
                i--;
            }
            while (stack.Count != 0)
            {
                ans = stack.Peek() + ans;
                stack.Pop();
            }
            return ans;
        }

        public static string postFixToInfix(string s)
        {
            var stack = new Stack<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' &&
                                                      s[i] <= '9'))
                {
                    stack.Push(s[i].ToString());
                }
                else
                {
                    string op1 = stack.Pop();
                    string op2 = stack.Pop();
                    string exp = "(" + op2 + s[i] + op1 + ")";
                    stack.Push(exp);
                }
            }
            return stack.Pop();
        }

        public static string prefixToInfix(string s)
        {
            var stack = new Stack<string>();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if ((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' &&
                                                                         s[i] <= '9'))
                {
                    stack.Push(s[i].ToString());
                }
                else
                {
                    string op1 = stack.Pop();
                    string op2 = stack.Pop();
                    string exp = "(" + op1 + s[i] + op2 + ")";
                    stack.Push(exp);
                }
            }
            return stack.Pop();
        }

        public static string prefixToPostfix(string s)
        {
            var stack = new Stack<string>();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if ((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' &&
                                                                                            s[i] <= '9'))
                {
                    stack.Push(s[i].ToString());
                }
                else
                {
                    string op1 = stack.Pop();
                    string op2 = stack.Pop();
                    string exp = op1 + op2 + s[i];
                    stack.Push(exp);
                }
            }
            return stack.Pop();
        }

        public static string postFixToPrefix(string s)
        {
            var stack = new Stack<string>();
            for (int i = 0; i < s.Length; i++)
            {
                if ((s[i] >= 'A' && s[i] <= 'Z') || (s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' &&
                                                                                                         s[i] <= '9'))
                {
                    stack.Push(s[i].ToString());
                }
                else
                {
                    string op1 = stack.Pop();
                    string op2 = stack.Pop();
                    string exp = s[i] + op2 + op1;
                    stack.Push(exp);
                }
            }
            return stack.Pop();
        }

        // same as postFixToInfix but instead of returning the infix expression, we evaluate it and return the result
        public static int evaluatePostfix(string[] arr)
        {
            Stack<int> stack = new Stack<int>();
            foreach (string token in arr)
            {
                if (token == "+" || token == "-" || token == "*" || token == "/" || token == "^")
                {
                    int b = stack.Pop();
                    int a = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(a + b);
                            break;
                        case "-":
                            stack.Push(a - b);
                            break;
                        case "*":
                            stack.Push(a * b);
                            break;
                        case "/":
                            stack.Push((int)Math.Floor((double)a / b));
                            break;
                        case "^":
                            stack.Push((int)Math.Pow(a, b));
                            break;
                    }
                }
                else
                {
                    stack.Push(int.Parse(token));
                }
            }
            return stack.Pop();
        }
    }
}
