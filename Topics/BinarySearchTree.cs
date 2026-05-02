using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class BinarySearchTree
    {
        public static TreeNode SearchBST(TreeNode root, int val)
        {
            if (root == null)
            {
                return null;
            }
            if (root.val == val)
            {
                return root;
            }
            else if (root.val < val)
            {
                return SearchBST(root.right, val);
            }
            else if (root.val > val)
            {
                return SearchBST(root.left, val);
            }
            else return null;
        }

        public static List<int> FindFloorCeil(TreeNode root, int key)
        {
            List<int> result = new List<int> { -1, -1 }; // [floor, ceil]
            Helper(root, key, result);
            return result;
        }

        private static void Helper(TreeNode node, int key, List<int> res)
        {
            if (node == null) return;

            if (node.val == key)
            {
                res[0] = node.val; // floor
                res[1] = node.val; // ceil
                return;
            }

            if (key < node.val)
            {
                res[1] = node.val;
                Helper(node.left, key, res);
            }
            else
            {
                res[0] = node.val; // possible floor
                Helper(node.right, key, res);
            }
        }

        public static TreeNode InsertIntoBST(TreeNode root, int val)
        {
            if (root == null)
            {
                return new TreeNode(val);
            }
            if (root.val < val)
            {
                root.right = InsertIntoBST(root.right, val);
            }
            else
            {
                root.left = InsertIntoBST(root.left, val);
            }
            return root;
        }

        public static TreeNode DeleteNode(TreeNode root, int key)
        {

            // Base Case: If tree is empty
            if (root == null)
            {
                return null;
            }

            // Step 1: Traverse the BST to find the node
            // WHY: Use BST property to reduce search space (O(log n) avg)
            if (root.val < key)
            {
                // Key lies in right subtree
                root.right = DeleteNode(root.right, key);
                return root; // return updated root
            }
            else if (root.val > key)
            {
                // Key lies in left subtree
                root.left = DeleteNode(root.left, key);
                return root; // return updated root
            }
            else
            {
                // Step 2: Node found → handle deletion cases

                // Case 1: Leaf node (no children)
                // WHY: Simply remove it
                if (root.left == null && root.right == null)
                {
                    return null;
                }

                // Case 2: Only left child exists
                // WHY: Replace node with its left subtree
                else if (root.right == null)
                {
                    return root.left;
                }

                // Case 3: Only right child exists
                // WHY: Replace node with its right subtree
                else if (root.left == null)
                {
                    return root.right;
                }

                // Case 4: Both children exist
                else
                {
                    // Step 3: Find inorder successor (smallest in right subtree)
                    // WHY: Maintains BST property after replacement
                    var temp = root.right;
                    while (temp.left != null)
                    {
                        temp = temp.left;
                    }

                    // Step 4: Replace current node value with successor value
                    root.val = temp.val;

                    // Step 5: Delete the duplicate node from right subtree
                    // WHY: We copied its value, now remove original node
                    root.right = DeleteNode(root.right, temp.val);

                    return root;
                }
            }
        }

        public static TreeNode BstFromPreorder(int[] preorder)
        {

            // Step 1: Initialize stack to simulate recursion
            // WHY: Keeps track of ancestors where right child may be attached later
            var stack = new Stack<TreeNode>();

            // Step 2: First element is always root
            var root = new TreeNode(preorder[0]);
            stack.Push(root);

            // Step 3: Process remaining elements
            for (int i = 1; i < preorder.Length; i++)
            {

                TreeNode tempNode = null;

                // Step 4: Find correct parent for current node
                // WHY:
                // If current value is greater → we are moving to right subtree
                // Pop until we find a node smaller than current
                while (stack.Count > 0 && preorder[i] > stack.Peek().val)
                {
                    tempNode = stack.Pop();
                }

                // Step 5: Create current node
                var currNode = new TreeNode(preorder[i]);

                if (tempNode != null)
                {
                    // Step 6A: Attach as RIGHT child
                    // WHY:
                    // Last popped node is the correct parent for right child
                    tempNode.right = currNode;
                }
                else
                {
                    // Step 6B: Attach as LEFT child
                    // WHY:
                    // Current value is smaller → belongs to left subtree of top node
                    stack.Peek().left = currNode;
                }

                // Step 7: Push current node to stack
                // WHY:
                // It can act as parent for upcoming nodes
                stack.Push(currNode);
            }

            // Step 8: Return constructed BST
            return root;
        }

        public static int FindPredecessor(TreeNode root, int key)
        {
            // Step 1: Initialize answer
            // WHY: Stores best candidate found so far
            var predecessor = -1;

            void Helper(TreeNode node, int k)
            {
                // Base case: reached null → stop
                if (node == null) { return; }

                // Case 1: Current node is <= key
                if (node.val <= k)
                {
                    // This node is a valid predecessor candidate
                    predecessor = node.val;

                    // Try to find a larger valid value in right subtree
                    // WHY: We want the "largest" ≤ key
                    Helper(node.right, k);
                }
                else
                {
                    // Case 2: Current node is greater than key
                    // Cannot be predecessor → go left for smaller values
                    Helper(node.left, k);
                }
            }

            // Step 2: Start recursion
            Helper(root, key);

            // Step 3: Return result
            return predecessor;
        }

        public static int FindSuccessor(TreeNode root, int key)
        {
            // Step 1: Initialize answer
            // WHY: Stores best candidate found so far
            var successor = -1;

            void Helper(TreeNode node, int k)
            {
                // Base case: reached null → stop
                if (node == null) { return; }

                // Case 1: Current node is >= key
                if (node.val >= k)
                {
                    // This node is a valid successor candidate
                    successor = node.val;

                    // Try to find a smaller valid value in left subtree
                    // WHY: We want the "smallest" ≥ key
                    Helper(node.left, k);
                }
                else
                {
                    // Case 2: Current node is less than key
                    // Cannot be successor → go right for larger values
                    Helper(node.right, k);
                }
            }

            // Step 2: Start recursion
            Helper(root, key);

            // Step 3: Return result
            return successor;
        }
    }
}
