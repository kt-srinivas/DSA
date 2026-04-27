using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class HashSets
    {
        //Find Duplicate
        public static bool FindDuplicate(int[] arr)
        {
            var set = new HashSet<int>();
            for (int i = 0; i < arr.Length; i++)
            {
                if (set.Contains(arr[i]))
                {
                    return true;
                }
                else
                {
                    set.Add(arr[i]);
                }
            }
            return false;
        }

        //Find the duplicate Number
        //Given an array of integers nums containing n + 1 integers where each integer is in the range [1, n] inclusive, there is only one repeated number in nums, return this repeated number.
        //Example:
        //Input: nums = [1,3,4,2,2]
        //Output: 2
        public static int FindDuplicateNumber(int[] arr)
        {
            int slow = arr[0];
            int fast = arr[0];
            do
            {
                slow = arr[slow];
                fast = arr[arr[fast]];

            } while (slow != fast);
            int start = arr[0];
            while(start != slow)
            {
                start = arr[start];
                slow = arr[slow];
            }
            return slow;
        }

        //Find Happy Number
        public static bool IsHappyUsingHashSet(int n)
        {
            var set = new HashSet<int>();
            while (n != 1 && !set.Contains(n))
            {
                set.Add(n);
                int sum = 0;
                while (n > 0)
                {
                    int digit = n % 10;
                    sum += digit * digit;
                    n /= 10;
                }
                n = sum;
            }
            return n == 1;
        }

        // Checks if a number is a Happy Number using Floyd's Cycle Detection Algorithm
        public static bool IsHappyUsingFloydsAlgorithm(int n)
        {
            // Slow pointer moves one step at a time
            int slow = n;

            // Fast pointer moves two steps at a time
            int fast = n;

            // Loop until both pointers meet
            // If the number is not happy, they will meet in a cycle
            do
            {
                // Move slow pointer by one transformation
                slow = GetNext(slow);

                // Move fast pointer by two transformations
                fast = GetNext(GetNext(fast));

            } while (slow != fast); // Stop when a cycle is detected

            // If the meeting point is 1, the number is happy
            // Otherwise it is stuck in a cycle (not happy)
            return slow == 1;
        }


        // Computes the next number in the sequence
        // by replacing the number with the sum of the squares of its digits
        public static int GetNext(int n)
        {
            int sum = 0;

            // Process each digit of the number
            while (n > 0)
            {
                // Extract the last digit
                int digit = n % 10;

                // Add square of the digit to the sum
                sum += digit * digit;

                // Remove the last digit
                n /= 10;
            }

            // Return the computed sum of squares
            return sum;
        }

        public static int FIndLongestSequence(int[] arr)
        {
            var set =  new HashSet<int>(arr);
            int result = int.MinValue;
            for (int i=0;i< arr.Length; i++)
            {
                int length = 1;
                if (!set.Contains(arr[i]-1))
                {
                    int currentNum = arr[i];
                    while (set.Contains(currentNum + 1))
                    {
                        length++;
                        currentNum++;
                    }
                }
                result = Math.Max(result, length);

            }
            return result==int.MinValue?0:result;
        }

    }
}
