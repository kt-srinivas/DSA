using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSA.Models;

namespace DSA.Topics
{
    public static class LinkedListProblems
    {
        public static void Traverse(Node head)
        {
            Node current = head;
            while (current != null)
            {
                Console.Write(current.data + " ");
                current = current.next;
            }
        }

        public static void CRUDOperations(Node node)
        {
            //insert at begining
            var newNode = new Node(25);
            newNode.next = node;
            Console.WriteLine($"Insert at begining:  ");
            Traverse(newNode);

            //insert at end
            var newNode2 = new Node(100);
            var temp = newNode;
            while(temp.next != null)
            {
                temp = temp.next;
            }
            temp.next = newNode2;
            newNode2.next = null;
            Console.WriteLine($"Insert at End:  ");
            Traverse(newNode);
            //insert at given position 3
            var newNode3 = new Node(50);
            int pos = 3;
            temp = newNode;
            for(int i = 1; i < pos - 1; i++)
            {
                temp = temp.next;
            }
            newNode3.next = temp.next;
            temp.next = newNode3;

            Console.WriteLine($"Insert at position:  ");
            Traverse(newNode);

            //Delete at End
            temp = newNode;
            while (temp.next.next != null)
            {
                temp = temp.next;
            }
            temp.next = null;
            Console.WriteLine($"Delete at End:  ");
            Traverse(newNode);


            //Delete at given position 3
            temp = newNode;
            for (int i = 1; i < pos - 1; i++)
            {
                temp = temp.next;
            }
            temp.next = temp.next.next;
            Console.WriteLine($"Delete at position:  ");
            Traverse(newNode);


            //Delete at Begining
            newNode = newNode.next;
            Console.WriteLine($"Delete at begining:  ");
            Traverse(newNode);

        }

        public static bool LinkedListCycleExists(Node node)
        {
            Node fast = node;
            Node slow = node;
            while (fast!=null &&fast.next != null) 
            {
                slow = slow.next;
                fast = fast.next.next;
                if (slow == fast)
                {
                    return true;
                }
            }
            return false;

        }

        //Find the middle node of a linked list
        //If there are even number of nodes, return the second middle node
        //Example: 1->2->3->4->5, middle node is 3
        //Example: 1->2->3->4, middle node is 3
        //Approach: Use two pointers, one slow and one fast. Move the slow pointer by 1 and fast pointer by 2. When fast pointer reaches the end, slow pointer will be at the middle node.
        public static Node MiddleNode(Node node)
        {
            Node fast = node;
            Node slow = node;
            while(fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
        }

        //Find the node where the cycle begins in a linked list. If there is no cycle, return null.
        //Example: 1->2->3->4->5->3 (cycle begins at node with value 3), return node with value 3
        //Approach: Use two pointers, one slow and one fast. Move the slow pointer by 1 and fast pointer by 2. If there is a cycle, they will meet at some point. Then, move one pointer to the head and keep the other pointer at the meeting point. Move both pointers by 1 until they meet again. The point at which they meet will be the node where the cycle begins.
        public static Node NodeCycleBegins(Node node)
        {
            var start = node;
            var slow = node;
            var fast = node;
            Node meetingPoint = null;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                if(slow == fast)
                {
                    meetingPoint = slow;
                    break;
                }
            }
            while (start != meetingPoint)
            {
                start = start.next;
                meetingPoint = meetingPoint.next;
            }
            return start;
        }

        //Remove the Nth node from the end of a linked list and return the head of the modified linked list.
        //Example: 1->2->3->4->5, N=2, return 1->2->3->5
        //Approach: Use two pointers, one fast and one slow. Move the fast pointer by N steps. Then, move both pointers by 1 until the fast pointer reaches the end. The slow pointer will be at the node before the node to be removed. Update the next pointer of the slow pointer to skip the node to be removed.
        public static Node RemoveNthNodeFromEnd(Node node, int n)
        {
            // using dummy becaus eif the length of the linked list is equal to n, then we need to remove the head node. In that case, we can simply return dummy.next which will be the new head of the linked list.
            Node dummy = new Node(0);
            dummy.next = node;
            Node fast = dummy;
            Node slow = dummy;
            for(int i = 0; i < n; i++)
            {
                fast = fast.next;
            }
            while (fast != null && fast.next != null)
            {
                fast = fast.next;
                slow = slow.next;
            }
            slow.next = slow.next.next;
            return dummy.next;
        }

        //Given a linked list, group all the nodes at odd indexes together followed by the nodes at even indexes, and return the reordered list. The first node is considered odd index and the second node is even index and so on.
        //Example: 1->2->3->4->5, return 1->3->5->2->4
        //Approach: Use two pointers, one for odd index and one for even index. Move the odd pointer by 2 and even pointer by 2 until the end of the linked list. Then, update the next pointer of the last odd node to point to the head of the even nodes.
        public static Node GroupByOddIndexesThenEven(Node node)
        {
            Node odd = node;
            Node even = odd.next;
            Node evenHead = even;
            while (even !=null && even.next !=null)
            {
                odd.next = even.next;
                odd = odd.next;
                even.next = odd.next;
                even = even.next;
            }

            odd.next = evenHead;
            return node;
        }

        //Reverse a linked list and return the head of the reversed linked list.
        //Example: 1->2->3->4->5, return 5->4->3->2->1
        //Approach: Use three pointers, prev, current and next. Initialize prev to null and current to the head of the linked list. Iterate through the linked list and update the next pointer of the current node to point to the prev node. Then, move the prev pointer to the current node and move the current pointer to the next node. Finally, return the prev pointer which will be the new head of the reversed linked list.
        public static Node ReverseLinkedList(Node node)
        {
            Node prev = null;
            Node current = node;
            while (current != null)
            {
                Node next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            return prev;
        }

        public static Node ReverseBetweenTwoIndexes(Node node, int m, int n)
        {
            Node dummy = new Node(0);
            dummy.next = node;
            Node NodeBeforeM = dummy;
            for (int i = 1; i < m; i++)
            {
                NodeBeforeM = NodeBeforeM.next;
            }
            Node First = NodeBeforeM.next;
            Node NoadeAfterN = First;
            for (int i = m; i <= n; i++)
            {
                NoadeAfterN = NoadeAfterN.next;
            }
            Node current = First;
            Node prev = NodeBeforeM;

            while (current != NoadeAfterN)
            {
                Node temp = current.next;
                current.next = prev;
                prev = current;
                current = temp;
            }
            NodeBeforeM.next = prev;
            First.next = NoadeAfterN;
            return dummy.next;
        }

        public static Node SwapPairs(Node node)
        {
            var dummy = new Node(0);
            dummy.next = node;
            var prev = dummy;
            while(prev.next!=null && prev.next.next !=null)
            {
                var A = prev.next;
                var B = prev.next.next;
                A.next = B.next;
                B.next = A;
                prev.next = B;
                prev = A;
            }
            return dummy.next;
        }

        //Given the head of a linked list, rotate the list to the right by k places and return the new head of the rotated linked list.
        //Example: 1->2->3->4->5, k=2, return 4->5->1->2->3
        public static Node RotateLinkedlist(Node node, int k)
        {
            if (node == null || node.next == null || k == 0)
            {
                return node;
            }

            var fast = node;
            var head = node;
            var slow = node;
            int length = 0;
            while (node != null)
            {
                length++;
                node = node.next;
            }
            k = k % length;
            for(int i = 0; i < k; i++)
            {
                fast = fast.next;
            }
            while (fast!= null && fast.next !=null)
            {
                slow = slow.next;
                fast = fast.next;
            }
            var ans = slow.next;
            slow.next = null;
            fast.next = head;
            return ans;

        }

        //Given the head of a linked list, reverse the nodes of the list k at a time, and return the modified list. k is a positive integer and is less than or equal to the length of the linked list. If the number of nodes is not a multiple of k then left-out nodes in the end should remain as it is.
        //Example: 1->2->3->4->5, k=2, return 2->1->4->3->5
        public static Node LInkedListGroupReverse(Node node, int k)
        {
            var dummy = new Node(0);
            dummy.next = node;
            var groupPrev = dummy;
            while (true)
            {
                var kth = groupPrev;
                //Move the kth pointer k steps ahead to find the end of the current group. If we reach the end of the linked list before moving k steps, it means there are less than k nodes left and we can break out of the loop.
                for (int i = 0; i < k && kth != null; i++)
                {
                    kth = kth.next;
                }
                if (kth == null)
                {
                    break;
                }
                //Now we have the groupPrev pointer pointing to the node before the current group and the kth pointer pointing to the last node of the current group. We can reverse the nodes in the current group by using three pointers: prev, current and next. Initialize prev to the node after kth (which is the node after the current group), current to the node after groupPrev (which is the first node of the current group) and next to null. Iterate through the nodes in the current group and update the next pointer of each node to point to the prev node. Then, move the prev pointer to the current node and move the current pointer to the next node. Finally, update the next pointer of groupPrev to point to kth (which is now the first node of the reversed group) and update groupPrev to point to temp2 (which is now the last node of the reversed group).
                var groupNext = kth.next;
                var prev = groupNext;
                var current = groupPrev.next;
                while (current != groupNext)
                {
                    var temp = current.next;
                    current.next = prev;
                    prev = current;
                    current = temp;
                }

                //After reversing the nodes in the current group, we need to update the next pointer of groupPrev to point to kth (which is now the first node of the reversed group) and update groupPrev to point to temp2 (which is now the last node of the reversed group).
                var temp2 = groupPrev.next;
                groupPrev.next = kth;
                groupPrev = temp2;
            }
            return dummy.next;
        }

        public static Node FlattenLinkedList(Node head)
        {
            if(head == null || head.next == null)
            {
                return head;
            }
            var mergedHead = FlattenLinkedList(head.next);
            return MergeTwoSortedLinkedLists(head, mergedHead);
        }
        
        public static Node MergeTwoSortedLinkedLists(Node n1, Node n2)
        {
            if(n1 == null)
            {
                return n2;
            }
            if (n2 == null)
            {
                return n1;
            }

            var dummy = new Node(0);
            var current = dummy;
            while(n1 != null && n2 != null)
            {
                if(n1.data < n2.data)
                {
                    current.child = n1;
                    n1 = n1.child;
                }
                else
                {
                    current.child = n2;
                    n2 = n2.child;
                }
                current = current.child;
            }
            if(n1 != null)
            {
                current.child = n1;
            }
            else
            {
                current.child = n2;
            }

            return dummy.child;
        }

        public static Node CopyRandomList(Node head)
        {
            if (head == null)
            {
                return null;
            }
            Node dummy = new Node(-1);
            Node curr = dummy;
            Node temp = head;
            while (temp != null)
            {
                Node n = new Node(temp.data);
                n.next = temp.next;
                temp.next = n;
                temp = temp.next.next;
            }
            temp = head;
            while (temp != null)
            {
                if (temp.random != null)
                {
                    temp.next.random = temp.random.next;
                }
                temp = temp.next.next;
            }
            temp = head;
            while (temp != null)
            {
                curr.next = temp.next;
                curr = curr.next;
                temp.next = temp.next.next;
                temp = temp.next;
            }
            return dummy.next;

        }
    }


}
