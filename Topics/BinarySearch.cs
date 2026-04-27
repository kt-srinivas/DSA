using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class BinarySearch
    {
        public static int FindElement(int[] nums, int target)
        {
            
            int i=0,j=nums.Length - 1;
            while (i <= j)
            {
                int mid = i + (j - i) / 2;
                if (nums[mid] == target) return mid;
                else if (nums[mid] < target) i = mid + 1;
                else j = mid - 1;
            }
            return -1;
        }

        //Floor of a number
        // The floor of a number is defined as the largest element in the array which is smaller than or equal to the target number.
        // Example: Input: arr = [1, 2, 8, 10, 10, 12, 19], target = 5
        // Output: 1
        public static int FindFloor(int[] nums, int target)
        {
            int i = 0,j= nums.Length - 1, floor = -1;
            while (i <= j)
            {
                int mid = i + (j - i) / 2;
                if (nums[mid] <= target)
                {
                    floor = mid;
                    i = mid + 1;
                }
                else
                {
                    j = mid - 1;
                }
            }
            return floor;
        }

        //Ceiling of a number
        // The ceiling of a number is defined as the smallest element in the array which is greater than or equal to the target number.
        //Example: Input: arr = [1, 2, 8, 10, 10, 12, 19], target = 5
        // Output: 2
        public static int FindCeiling(int[] nums, int target)
        {
            int i = 0, j = nums.Length - 1, ceiling = -1;
            while(i <= j)
            {
                int mid = i + (j - i) / 2;
                if (nums[mid] >= target)
                {
                    ceiling = mid;
                    j = mid - 1;
                }
                else
                {
                    i = mid + 1;
                }
            }
            return ceiling;
        }

        //Lowest index of a target number
        // Example: Input: arr = [1, 2, 2, 2, 3, 4, 5], target = 2
        public static int FindLowestIndex(int[] nums, int target)
        {
            int i = 0, j = nums.Length - 1, lowestIndex = -1;
            while (i <= j)
            {
                int mid = i + (j - i) / 2;
                if (nums[mid] == target)
                {
                    lowestIndex = mid;
                    j = mid - 1;
                }
                else if (nums[mid] < target)
                {
                    i = mid + 1;
                }
                else
                {
                    j = mid - 1;
                }
            }
            return lowestIndex;
        }


        //Highest index of a target number
        // Example: Input: arr = [1, 2, 2, 2, 3, 4, 5], target = 2
        public static int FindHighestIndex(int[] nums, int target)
        {
            int i = 0, j = nums.Length - 1, highestIndex = -1;
            while (i <= j)
            {
                int mid = i + (j - i) / 2;
                if (nums[mid] == target)
                {
                    highestIndex = mid;
                    i = mid + 1;
                }
                else if (nums[mid] < target)
                {
                    i = mid + 1;
                }
                else
                {
                    j = mid - 1;
                }
            }
            return highestIndex;
        }

        //Count of occurrences of a target number
        public static int CountOfOccurrences(int[] nums, int target)
        {
            int lowestIndex = FindLowestIndex(nums, target);
            if (lowestIndex == -1) return 0;
            int highestIndex = FindHighestIndex(nums, target);
            return highestIndex - lowestIndex + 1;//sliding Window concept
        }

        //Count of Rotations in a sorted rotated array
        // Example: Input: arr = [15, 18, 2, 3, 6, 12]
        // Output: 2
        public static int CountOfRotations(int[] nums)
        {
            int i = 0, j = nums.Length - 1;
            while (i < j)
            {
               int mid = i + (j - i) / 2;
                if (nums[mid] < nums[j])
                {
                    j = mid;
                }
                else if (nums[mid] > nums[j])
                {
                    i = mid + 1;
                }
                else
                {
                    j--;
                }
            }
            return i;
        }


        //Find target in infinite sorted array
        // Example: Input: arr = [3, 5, 7, 9, 10, 90, 100, 130, 140, 160, 170], target = 10
        public static int FindInInfiniteSortedArray(int[] nums, int target)
        {
            int i = 0, j = 1;
            while (target > nums[j])
            {
                i = j;
                j *= 2;
            }
            //now do binary search between i and j
            j = Math.Min(j, nums.Length - 1); //in case j exceeds array length
            while (i <= j)
            {
                int mid = i + (j - i) / 2;
                if (nums[mid] == target) return mid;
                else if (nums[mid] < target) i = mid + 1;
                else j = mid - 1;
            }
            return -1;
        }

        //Find minimum difference element in a sorted array for target
        // Ex :[1,2,4,6,10,15] ,k =12
        // Output :10
        public static int FindMinDifferenceElement(int[] nums, int target)
        {
            int i = 0, j = nums.Length - 1;
            int ceil = FindCeiling(nums, target);
            int floor = FindFloor(nums, target);
            int ceilDifference = ceil == -1 ? int.MaxValue : Math.Abs(nums[ceil] - target);
            int floorDifference = floor == -1 ? int.MaxValue : Math.Abs(nums[floor] - target);
            if (ceilDifference < floorDifference) return nums[ceil];
            else return nums[floor];
        }

        public static double FindMedian(int[] nums1, int[] nums2)
        {

            if (nums1.Length > nums2.Length)
            {
                return FindMedian(nums1, nums2);
            }

            int n1 = nums1.Length;
            int n2 = nums2.Length;
            int low = 0;
            int high = n1;
            while (low <= high)
            {
                int leftXLength = (low + high) / 2;
                int leftYLength = ((n1+n2+1)/2)-leftXLength;

                int maxLeftX = leftXLength == 0 ? int.MinValue : nums1[leftXLength - 1];
                int maxLeftY = leftYLength == 0 ? int.MinValue: nums2[leftYLength - 1];

                int minRightX = leftXLength == n1 ? int.MaxValue: nums1[leftXLength];
                int minRightY = leftYLength == n2 ? int.MaxValue : nums2[leftYLength];
                if(maxLeftX <= minRightX && maxLeftY <= minRightX) 
                {
                    if ((n1 + n2) % 2 == 0)
                    {
                        return (double)((Math.Max(maxLeftX,maxLeftY) + Math.Min(minRightX,minRightY))/2.0);
                    }
                    else
                    {
                        return (double)(Math.Max(maxLeftX,maxLeftY));
                    }
                }
                if(maxLeftX >= minRightX)
                {
                    high = leftXLength - 1;
                }
                else
                {
                    low = leftXLength + 1;
                }

            }
            return -1;

        }
            public static int MinDays(int[] bloomDay, int m, int k)
            {
                if ((long)m * k > bloomDay.Length) return -1;

                int low = bloomDay.Min();
                int high = bloomDay.Max();
                int ans = -1;

                while (low <= high)
                {
                    int mid = low + (high - low) / 2;

                    int bouquets = CountBouquets(bloomDay, mid, k);

                    if (bouquets >= m)
                    {
                        ans = mid;
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }

                return ans;
            }

            private static int CountBouquets(int[] nums, int day, int k)
            {
                int bouquets = 0;
                int consecutive = 0;

                foreach (int bloom in nums)
                {
                    if (bloom <= day)
                    {
                        consecutive++;
                        if (consecutive == k)
                        {
                            bouquets++;
                            consecutive = 0;
                        }
                    }
                    else
                    {
                        consecutive = 0;
                    }
                }

                return bouquets;
            }
        




    }
}
