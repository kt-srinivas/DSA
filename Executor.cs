using DSA.Topics;

namespace DSA
{
    internal class Executor
    {
        static void Main(string[] args)
        {
            /*            int index = BinarySearch.CountOfRotations([2, 5, 6, 0, 0, 1, 2,2,2]);
                        Console.WriteLine(index);
                        *//*int[][] array =[
                                                [1,2,3,4],
                                                [5,6,7,8],
                                                [9,10,11,12],
                                                [13,14,15,16]
                                         ];
                         _2DArrays.SpiralTraverse(array);*//*

                        var s = Strings.CheckPalindrome("madame");
                        Strings.CountofEachChar("helloworld");
                        var w = Strings.ReverseWords("    Hello World. i am Srinivas     ");
                        var cu = Strings.LongestSubStringWithUniqueCharacters("abcdabcbbcdefg");
                        var k = Strings.LongestSubStringByExchangingAtMostKCharacters("aabacbebebe", 3);*/
            /*            Node cycleNode = new Node(1);
                        cycleNode.next = new Node(2);
                        cycleNode.next.next = new Node(3);
                        cycleNode.next.next.next = new Node(4);
                        cycleNode.next.next.next.next = new Node(5);
                        cycleNode.next.next.next.next.next = cycleNode.next.next; //creating a loop
                        var resultIS = LinkedListProblems.NodeCycleBegins(cycleNode);*/
            /*
                        Node node = new Node(1);
                        node.next = new Node(2);
                        node.next.next = new Node(3);
                        node.next.next.next = new Node(4);
                        node.next.next.next.next = new Node(5);
                        var result = LinkedListProblems.RotateLinkedlist(node,7);
                        LinkedListProblems.Traverse(result);*/

            //Quesues
            /*Queues.PrintBinaryVersionForN(5);
            var result = Queues.FirstNegativeNumberInSubArrayOfSizeK([2,-1,3,-4,-5,6,0,0],3);*/

            //HashMap
            /*            var r1 = HashMaps.FindPair([2, 7, 11, 15], 9);
                        var r2 = HashMaps.LongestSubArrayWithSumZero([1, -1, 3, 2, -2, -3]);
                        var r3 = HashMaps.NumOfUniqueElementsinKSizedWindow([1, 2, 3, 2, 1], 3);
                        var r4 = HashMaps.LongestSubStringWithAtmostkUniqueChar("aabacbebebe", 3);
                        var r5 = HashMaps.FindNoOfSubArrayWithSumEqualToK([1, 1,2,0,3,-1, 1,4], 4);
                        var r6 = HashMaps.FindMinWindowSubString("ADOBECODEBANC", "ABC");*/
            var result = Heaps.FindKthSmallestElement([3, 2, 1, 5, 6, 4], 3);
        }



    }
}
