using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Greedy
    {
        // N Meetings In One Room using Greedy
        // Time Complexity  : O(n log n)
        // Space Complexity : O(n)
        public static int NMeetingsInOneRoom(int[] start, int[] end)
        {
            // Step 1:
            // Store meetings as (start,end,meetingNumber)
            List<(int start, int end, int no)> meetings = new List<(int, int, int)>();

            // Step 2:
            // Build meetings list
            for (int i = 0; i < start.Length; i++)
            {
                meetings.Add((start[i], end[i], i + 1));
            }

            // Step 3:
            // Sort meetings by ending time
            meetings.Sort((a, b) => a.end.CompareTo(b.end));

            // Step 4:
            // Store end time of last selected meeting
            int lastMeetingEnd = -1;

            // Step 5:
            // Count maximum meetings
            int count = 0;

            // Step 6:
            // Traverse sorted meetings
            foreach (var meeting in meetings)
            {
                // Select meeting if it starts after previous meeting ends
                if (meeting.start > lastMeetingEnd)
                {
                    count++;
                    lastMeetingEnd = meeting.end;
                }
            }

            // Step 7:
            // Return answer
            return count;
        }

        //Fractionl Knapsack Problem using Greedy
        // Time Complexity  : O(n log n)
        // Space Complexity : O(n)
        public static double FractionalKnapsack(int[] weight, int[] value, int capacity)
        {
            // Step 1:
            // Store items as (value, weight, index)
            List<(double value, double weight, int index)> items = new List<(double, double, int)>();

            // Step 2:
            // Build items list
            for (int i = 0; i < weight.Length; i++)
            {
                items.Add((value[i], weight[i], i));
            }

            // Step 3:
            // Sort items by value/weight ratio in descending order
            items.Sort((a, b) => (b.value / b.weight).CompareTo(a.value / a.weight));

            // Step 4:
            // Initialize total value
            double totalValue = 0;

            // Step 5:
            // Traverse sorted items
            foreach (var item in items)
            {
                if (capacity == 0)
                    break;

                if (item.weight <= capacity)
                {
                    totalValue += item.value;
                    capacity -= (int)item.weight;
                }
                else
                {
                    totalValue += item.value * ((double)capacity / item.weight);
                    capacity = 0;
                }
            }

            // Step 6:
            // Return answer
            return totalValue;
        }
    }
}
