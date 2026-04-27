using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class MergedIntervals
    {
        public static List<List<int>> MergeIntervals(int[][] intervals)
        {
            var result = new List<List<int>>();
            if (intervals == null || intervals.Length == 0)
            {
                return result;
            }
            foreach (var interval in intervals)
            {
                if (result.Count == 0)
                {
                    result.Add(new List<int> { interval[0], interval[1] });
                }
                else
                {
                    var lastInterval = result[result.Count - 1];
                    if (lastInterval[1] >= interval[0])
                    {
                        lastInterval[1] = Math.Max(lastInterval[1], interval[1]);
                    }
                    else
                    {
                        result.Add(new List<int> { interval[0], interval[1] });
                    }
                }
            }

            return result;
        }

        public static List<List<int>> InsertInterval(int[][] interval, int[] newInterval)
        {
            var result = new List<List<int>>();
            int i = 0;
            while(i<interval.Length && interval[i][1] < newInterval[0])
            {
                result.Add(new List<int> { interval[i][0], interval[i][1] });
                i++;
            }

            while(i < interval.Length && interval[i][0] <= newInterval[1])
            {
                newInterval[0] = Math.Min(newInterval[0], interval[i][0]);
                newInterval[1] = Math.Max(newInterval[1], interval[i][1]);
                i++;
            }

            result.Add(new List<int> { newInterval[0], newInterval[1] });
            while (i < interval.Length)
            {
                result.Add(new List<int> { interval[i][0], interval[i][1] });
                i++;
            }

            return result;
        }

        public static List<List<int>> IntersectionOfIntervals(int[][] interval1, int[][] interval2)
        {
            var result = new List<List<int>>();
            int i=0, j = 0;
            while(i < interval1.Length && j < interval2.Length)
            {
                int low = Math.Max(interval1[i][0], interval2[j][0]);
                int high = Math.Min(interval1[i][1], interval2[j][1]);
                if(low <= high)
                {
                    result.Add(new List<int> { low, high });
                }
                if (interval1[i][1] < interval2[j][1])
                {
                    i++;
                }
                else
                {
                    j++;
                }
            }
            return result;
        }

        public static bool CanAttendAllMeetings(int[][] intervals)
        {
            Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
            if (intervals == null || intervals.Length == 0)
            {
                return true;
            }
            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i][0] < intervals[i - 1][1])
                {
                    return false;
                }
            }
            return true;
        }

        public static int MinRoomsRequiredForMEetings(int[][] intervals)
        {
            var startTimes = new int[intervals.Length];
            var endTimes = new int[intervals.Length];
            for (int i = 0; i < intervals.Length; i++)
            {
                startTimes[i] = intervals[i][0];
                endTimes[i] = intervals[i][1];
            }
            Array.Sort(startTimes);
            Array.Sort(endTimes);
            int rooms = 0, endIndex = 0;
            int result = 0;
            for (int i = 0; i < startTimes.Length; i++)
            {
                if (startTimes[i] < endTimes[endIndex])
                {
                    rooms++;
                    result = Math.Max(result, rooms);
                }
                else
                {
                    rooms--;
                    endIndex++;
                }                         
            }
            return result;
        }
    }
}
