using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Sorts
    {
        // Bubble Sort Algorithm
        //Time Complexity: O(n^2)
        // Space Complexity: O(1)
        //Repeatedly compare adjacent elements and swap them if they are in the wrong order.
        public static int[] BubbleSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            return arr;
        }

        // Selection Sort Algorithm
        //Time Complexity: O(n^2)
        // Space Complexity: O(1)
        //Repeatedly find the minimum element from the unsorted part and move it to the sorted part.
        public static int[] SelectionSort(int[] arr)
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
            return arr;
        }

        // Insertion Sort Algorithm
        //Time Complexity: O(n^2)
        // Space Complexity: O(1)
        // Repeatedly take the next element from the unsorted part and insert it into the correct position in the sorted part.
        public static int[] InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for(int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while(j>=0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
            return arr;
        }

        //MergeSort
        //
        public static void MergeSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                MergeSort(arr, left, mid);
                MergeSort(arr, mid + 1, right);

                Merge(arr, left, mid, right);
            }
        }

        private static void Merge(int[] arr, int left, int mid, int right)
        {
            int[] result = new int[right - left + 1];
            int temp = 0;
            int i = left;
            int j = mid + 1;
            while (i <= mid && j<= right) 
            {
                if (arr[i] <= arr[j])
                {
                    result[temp++] = arr[i++];
                }
                else
                {
                    result[temp++] = arr[j++];
                }
            }
            while (i <= mid)
            {
                result[temp++] = arr[i++];
            }
            while (j <= right)
            {
                result[temp++] = arr[j++];
            }
            for (int x = 0; x < result.Length; x++)
                arr[left + x] = result[x];
        }


        // QuickSort Algorithm
        // Time Complexity: O(n log n) on average, O(n^2) in the worst case
        // Space Complexity: O(log n) due to recursive stack space
        // Explanation: QuickSort is a divide-and-conquer algorithm that selects a 'pivot' element from the array and partitions the other elements into two sub-arrays, according to whether they are less than or greater than the pivot. The sub-arrays are then sorted recursively.
        // For example, consider the array [3, 6, 8, 10, 1, 2, 1]. If we choose the first element (3) as the pivot, we would partition the array into two sub-arrays: [1, 2, 1] and [6, 8, 10]. We would then recursively apply QuickSort to these sub-arrays until the entire array is sorted.
        public static void QuickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high);

                QuickSort(arr, low, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, high);
            }
        }

        public static int Partition(int[] arr, int low, int high)
        {
            int pivot = arr[low];
            int i = low + 1;
            int j = high;
            while (i <= j)
            {
                while (i <= high && arr[i] <= pivot)
                {
                    i++;
                }
                while (j >= low && arr[j] > pivot)
                {
                    j--;
                }
                if (i < j)
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            int temp1 = arr[low];
            arr[low] = arr[j];
            arr[j] = temp1;
            return j;
        }

        public static int CountInversions(int[] arr)
        {
            return MergeSort_CI(arr, 0, arr.Length - 1);
        }

        public static int MergeSort_CI(int[] arr, int left, int right)
        {
            int count = 0;
            if (left < right)
            {
                int mid = left + (right - left) / 2;

                count += MergeSort_CI(arr, left, mid);
                count += MergeSort_CI(arr, mid + 1, right);

                count += Merge_CI(arr, left, mid, right);
            }
            return count;
        }

        public static int Merge_CI(int[] arr, int left, int mid, int right)
        {
            int[] result = new int[right - left + 1];
            int temp = 0;
            int i = left;
            int j = mid + 1;
            int count = 0;
            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j])
                {
                    result[temp++] = arr[i++];
                }
                else
                {
                    result[temp++] = arr[j++];
                    count += (mid - i + 1);
                }
            }
            while (i <= mid)
            {
                result[temp++] = arr[i++];
            }
            while (j <= right)
            {
                result[temp++] = arr[j++];
            }
            for (int x = 0; x < result.Length; x++)
                arr[left + x] = result[x];
            return count;
        }

        public static int ReversePairs(int[] nums)
        {
            return (int)MergeSort_RP(nums, 0, nums.Length - 1);
        }
        public static long MergeSort_RP(int[] arr, int left, int right)
        {
            long count = 0;
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                count += MergeSort_RP(arr, left, mid);
                count += MergeSort_RP(arr, mid + 1, right);
                count += CountReversePairs(arr, left, mid, right);
                Merge(arr, left, mid, right);
            }
            return count;
        }

        public static long CountReversePairs(int[] arr, int low, int mid, int high)
        {
            int right = mid + 1;
            long count = 0;
            for (int i = low; i <= mid; i++)
            {
                while (right <= high && (long)arr[i] > 2L * arr[right])
                {
                    right++;
                }
                count += right - (mid + 1);
            }
            return count;
        }
    }
}
