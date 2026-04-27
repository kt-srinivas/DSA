using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static  class Trees
    {
        public static IList<int> InorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();   // List to store traversal

            Inorder(root, result);                // Call helper method

            return result;                        // Return final result
        }

        private static void Inorder(TreeNode node, List<int> result)
        {
            if (node == null) return;             // Base case: if node is null, stop recursion

            Inorder(node.left, result);           // Step 1: Traverse left subtree

            result.Add(node.val);                 // Step 2: Visit current node

            Inorder(node.right, result);          // Step 3: Traverse right subtree
        }

        // =========================
        // 2. Preorder Traversal (Recursive)
        // =========================
        public static IList<int> PreorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();   // Store result

            Preorder(root, result);               // Call helper

            return result;                        // Return traversal
        }

        private static void Preorder(TreeNode node, List<int> result)
        {
            if (node == null) return;             // Base case

            result.Add(node.val);                 // Step 1: Visit node first

            Preorder(node.left, result);          // Step 2: Traverse left

            Preorder(node.right, result);         // Step 3: Traverse right
        }

        // =========================
        // 3. Postorder Traversal (Recursive)
        // =========================
        public static IList<int> PostorderTraversal(TreeNode root)
        {
            List<int> result = new List<int>();   // Store result

            Postorder(root, result);              // Call helper

            return result;                        // Return result
        }

        private static void Postorder(TreeNode node, List<int> result)
        {
            if (node == null) return;             // Base case

            Postorder(node.left, result);         // Step 1: Traverse left

            Postorder(node.right, result);        // Step 2: Traverse right

            result.Add(node.val);                 // Step 3: Visit node at last
        }
        public static IList<int> InorderTraversalIterative(TreeNode root)
        {
            List<int> result = new List<int>();           // Result list
            Stack<TreeNode> stack = new Stack<TreeNode>(); // Stack to simulate recursion
            TreeNode curr = root;                         // Start from root

            while (curr != null || stack.Count > 0)
            {     // Continue until all nodes processed

                while (curr != null)
                {                    // Reach leftmost node
                    stack.Push(curr);                     // Push current node to stack
                    curr = curr.left;                     // Move to left child
                }

                curr = stack.Pop();                       // Pop node from stack

                result.Add(curr.val);                     // Visit node

                curr = curr.right;                        // Move to right subtree
            }

            return result;                                // Return traversal result
        }

        // Level Order Traversal (BFS)
        // Traverse the tree level by level from left to right
        // Uses Queue to process nodes in FIFO order

        public static IList<IList<int>> LevelOrder(TreeNode root)
        {
            // Result list to store all levels
            List<IList<int>> result = new List<IList<int>>();

            // If tree is empty, return empty result
            if (root == null) return result;

            // Queue for BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Add root to queue
            queue.Enqueue(root);

            // Process until queue is empty
            while (queue.Count > 0)
            {
                // Number of nodes at current level
                int size = queue.Count;

                // List to store current level
                List<int> level = new List<int>();

                // Process all nodes at current level
                for (int i = 0; i < size; i++)
                {
                    // Remove node from queue
                    TreeNode node = queue.Dequeue();

                    // Add node value to current level
                    level.Add(node.val);

                    // Add left child if exists
                    if (node.left != null)
                        queue.Enqueue(node.left);

                    // Add right child if exists
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }

                // Add current level to result
                result.Add(level);
            }

            // Return final result
            return result;
        }

        // Height of Binary Tree
        // The height (or max depth) is the number of nodes 
        // along the longest path from root to a leaf node

        public static int MaxDepth(TreeNode root)
        {
            // Base case: empty tree has height 0
            if (root == null) return 0;

            // Recursively find height of left subtree
            int leftHeight = MaxDepth(root.left);

            // Recursively find height of right subtree
            int rightHeight = MaxDepth(root.right);

            // Return max of both heights + 1 for current node
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        // Check Balanced Binary Tree
        // A tree is balanced if for every node,
        // the height difference between left and right subtree is at most 1

        public static bool IsBalanced(TreeNode root)
        {
            // If helper returns -1, tree is not balanced
            return CheckHeight(root) != -1;
        }

        private static int CheckHeight(TreeNode node)
        {
            // Base case: null node has height 0
            if (node == null) return 0;

            // Get left subtree height
            int left = CheckHeight(node.left);

            // If left subtree is not balanced, propagate -1
            if (left == -1) return -1;

            // Get right subtree height
            int right = CheckHeight(node.right);

            // If right subtree is not balanced, propagate -1
            if (right == -1) return -1;

            // If height difference is more than 1, tree is not balanced
            if (Math.Abs(left - right) > 1) return -1;

            // Return height of current node
            return Math.Max(left, right) + 1;
        }

        // Diameter of Binary Tree
        // Diameter is the length of the longest path between any two nodes
        // The path may or may not pass through the root

        public static int DiameterOfBinaryTree(TreeNode root)
        {
            // Variable to store maximum diameter
            int diameter = 0;

            // Helper function to compute height and update diameter
            Height(root, ref diameter);

            // Return final diameter
            return diameter;
        }

        private static int Height(TreeNode node, ref int diameter)
        {
            // Base case: null node has height 0
            if (node == null) return 0;

            // Get left subtree height
            int left = Height(node.left, ref diameter);

            // Get right subtree height
            int right = Height(node.right, ref diameter);

            // Update diameter if current path is larger
            diameter = Math.Max(diameter, left + right);

            // Return height of current node
            return Math.Max(left, right) + 1;
        }

        // Maximum Path Sum in Binary Tree
        // Find the maximum sum of any path in the tree
        // A path can start and end at any node

        public static int MaxPathSum(TreeNode root)
        {
            // Variable to store maximum path sum
            int maxSum = int.MinValue;

            // Call helper function
            MaxGain(root, ref maxSum);

            // Return final result
            return maxSum;
        }

        private static int MaxGain(TreeNode node, ref int maxSum)
        {
            // Base case: null node contributes 0
            if (node == null) return 0;

            // Get max gain from left subtree (ignore negative paths)
            int left = Math.Max(0, MaxGain(node.left, ref maxSum));

            // Get max gain from right subtree (ignore negative paths)
            int right = Math.Max(0, MaxGain(node.right, ref maxSum));

            // Compute current path sum including this node
            int currentSum = node.val + left + right;

            // Update global maximum
            maxSum = Math.Max(maxSum, currentSum);

            // Return max gain including this node
            return node.val + Math.Max(left, right);
        }

        // Zigzag Level Order Traversal
        // Traverse levels alternately left-to-right and right-to-left

        public static IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            // Result list
            List<IList<int>> result = new List<IList<int>>();

            // If tree is empty
            if (root == null) return result;

            // Queue for BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Start with root
            queue.Enqueue(root);

            // Flag to control direction
            bool leftToRight = true;

            // Process until queue is empty
            while (queue.Count > 0)
            {
                // Current level size
                int size = queue.Count;

                // Use array to control insertion direction
                int[] level = new int[size];

                // Process nodes at current level
                for (int i = 0; i < size; i++)
                {
                    // Remove node
                    TreeNode node = queue.Dequeue();

                    // Decide index based on direction
                    int index = leftToRight ? i : size - 1 - i;

                    // Insert value
                    level[index] = node.val;

                    // Add left child
                    if (node.left != null)
                        queue.Enqueue(node.left);

                    // Add right child
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }

                // Flip direction
                leftToRight = !leftToRight;

                // Add level to result
                result.Add(new List<int>(level));
            }

            // Return result
            return result;
        }

        // Boundary Traversal of Binary Tree
        // Print nodes on the boundary in anti-clockwise direction:
        // Root -> Left Boundary -> Leaves -> Right Boundary (reversed)

        public static IList<int> BoundaryTraversal(TreeNode root)
        {
            // Result list
            List<int> result = new List<int>();

            // If tree is empty
            if (root == null) return result;

            // Add root (if not leaf)
            if (!IsLeaf(root))
                result.Add(root.val);

            // Add left boundary
            AddLeftBoundary(root.left, result);

            // Add leaf nodes
            AddLeaves(root, result);

            // Add right boundary in reverse
            AddRightBoundary(root.right, result);

            // Return result
            return result;
        }

        private static bool IsLeaf(TreeNode node)
        {
            // Node is leaf if both children are null
            return node.left == null && node.right == null;
        }

        private static void AddLeftBoundary(TreeNode node, List<int> result)
        {
            // Traverse left boundary excluding leaves
            while (node != null)
            {
                // Add if not leaf
                if (!IsLeaf(node))
                    result.Add(node.val);

                // Move left if possible, else right
                if (node.left != null)
                    node = node.left;
                else
                    node = node.right;
            }
        }

        private static void AddLeaves(TreeNode node, List<int> result)
        {
            // Base case
            if (node == null) return;

            // If leaf, add to result
            if (IsLeaf(node))
            {
                result.Add(node.val);
                return;
            }

            // Traverse left subtree
            AddLeaves(node.left, result);

            // Traverse right subtree
            AddLeaves(node.right, result);
        }

        private static void AddRightBoundary(TreeNode node, List<int> result)
        {
            // Temporary list to reverse later
            List<int> temp = new List<int>();

            // Traverse right boundary excluding leaves
            while (node != null)
            {
                // Add if not leaf
                if (!IsLeaf(node))
                    temp.Add(node.val);

                // Move right if possible, else left
                if (node.right != null)
                    node = node.right;
                else
                    node = node.left;
            }

            // Add in reverse order
            for (int i = temp.Count - 1; i >= 0; i--)
                result.Add(temp[i]);
        }

        // Vertical Order Traversal
        // Group nodes based on vertical distance (column index)
        // Use BFS and store (node, column, row)

        public static IList<IList<int>> VerticalTraversal(TreeNode root)
        {
            // Result list
            List<IList<int>> result = new List<IList<int>>();

            // If tree is empty
            if (root == null) return result;

            // Dictionary: column -> list of (row, value)
            SortedDictionary<int, List<(int row, int val)>> map =
                new SortedDictionary<int, List<(int, int)>>();

            // Queue for BFS storing node, column, row
            Queue<(TreeNode node, int col, int row)> queue =
                new Queue<(TreeNode, int, int)>();

            // Start with root at column 0, row 0
            queue.Enqueue((root, 0, 0));

            // BFS traversal
            while (queue.Count > 0)
            {
                // Dequeue element
                var (node, col, row) = queue.Dequeue();

                // If column not present, initialize list
                if (!map.ContainsKey(col))
                    map[col] = new List<(int, int)>();

                // Add current node
                map[col].Add((row, node.val));

                // Add left child with col-1
                if (node.left != null)
                    queue.Enqueue((node.left, col - 1, row + 1));

                // Add right child with col+1
                if (node.right != null)
                    queue.Enqueue((node.right, col + 1, row + 1));
            }

            // Process columns in sorted order
            foreach (var kvp in map)
            {
                // Sort by row, then value
                kvp.Value.Sort((a, b) =>
                {
                    if (a.row == b.row)
                        return a.val.CompareTo(b.val);
                    return a.row.CompareTo(b.row);
                });

                // Extract values
                List<int> column = new List<int>();
                foreach (var pair in kvp.Value)
                    column.Add(pair.val);

                // Add to result
                result.Add(column);
            }

            // Return result
            return result;
        }

        // Vertical Order Traversal (DFS Approach)
        // Store nodes as (column, row, value)
        // Then sort based on column -> row -> value

        public static IList<IList<int>> VerticalTraversalDFS(TreeNode root)
        {
            // Result list
            List<IList<int>> result = new List<IList<int>>();

            // If tree is empty
            if (root == null) return result;

            // List to store (column, row, value)
            List<(int col, int row, int val)> nodes =
                new List<(int, int, int)>();

            // Perform DFS traversal
            DFS(root, 0, 0, nodes);

            // Sort nodes by column, then row, then value
            nodes.Sort((a, b) =>
            {
                // First compare column
                if (a.col != b.col)
                    return a.col.CompareTo(b.col);

                // Then compare row
                if (a.row != b.row)
                    return a.row.CompareTo(b.row);

                // Finally compare value
                return a.val.CompareTo(b.val);
            });

            // Variable to track current column
            int prevCol = int.MinValue;

            // Iterate through sorted nodes
            foreach (var node in nodes)
            {
                // If new column encountered
                if (node.col != prevCol)
                {
                    // Add new list for this column
                    result.Add(new List<int>());

                    // Update previous column
                    prevCol = node.col;
                }

                // Add value to last column list
                result[result.Count - 1].Add(node.val);
            }

            // Return final result
            return result;
        }

        private static void DFS(TreeNode node, int col, int row, List<(int, int, int)> nodes)
        {
            // Base case: if node is null
            if (node == null) return;

            // Add current node to list
            nodes.Add((col, row, node.val));

            // Traverse left subtree (column - 1, row + 1)
            DFS(node.left, col - 1, row + 1, nodes);

            // Traverse right subtree (column + 1, row + 1)
            DFS(node.right, col + 1, row + 1, nodes);
        }


        // Top View of Binary Tree
        // Return nodes visible when the tree is viewed from the top
        // For each vertical column, pick the first node encountered (minimum level)

        public static IList<int> TopView(TreeNode root)
        {
            // Result list
            List<int> result = new List<int>();

            // If tree is empty
            if (root == null) return result;

            // Map to store first node at each column
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();

            // Queue for BFS storing node and column
            Queue<(TreeNode node, int col)> queue = new Queue<(TreeNode, int)>();

            // Start with root at column 0
            queue.Enqueue((root, 0));

            // BFS traversal
            while (queue.Count > 0)
            {
                // Dequeue node
                var (node, col) = queue.Dequeue();

                // If column not seen before, add it
                if (!map.ContainsKey(col))
                    map[col] = node.val;

                // Add left child with column -1
                if (node.left != null)
                    queue.Enqueue((node.left, col - 1));

                // Add right child with column +1
                if (node.right != null)
                    queue.Enqueue((node.right, col + 1));
            }

            // Extract values in sorted column order
            foreach (var kvp in map)
                result.Add(kvp.Value);

            // Return result
            return result;
        }

        // Bottom View of Binary Tree
        // Return nodes visible when viewed from the bottom
        // For each column, pick the last node encountered in BFS

        public static IList<int> BottomView(TreeNode root)
        {
            // Result list
            List<int> result = new List<int>();

            // If tree is empty
            if (root == null) return result;

            // Map to store latest node at each column
            SortedDictionary<int, int> map = new SortedDictionary<int, int>();

            // Queue for BFS storing node and column
            Queue<(TreeNode node, int col)> queue = new Queue<(TreeNode, int)>();

            // Start with root
            queue.Enqueue((root, 0));

            // BFS traversal
            while (queue.Count > 0)
            {
                // Dequeue node
                var (node, col) = queue.Dequeue();

                // Overwrite value for column (last seen wins)
                map[col] = node.val;

                // Add left child
                if (node.left != null)
                    queue.Enqueue((node.left, col - 1));

                // Add right child
                if (node.right != null)
                    queue.Enqueue((node.right, col + 1));
            }

            // Extract values
            foreach (var kvp in map)
                result.Add(kvp.Value);

            // Return result
            return result;
        }

        // Right View of Binary Tree
        // Return nodes visible when seen from the right side
        // Take the last node at each level

        public static IList<int> RightView(TreeNode root)
        {
            // Result list
            List<int> result = new List<int>();

            // If tree is empty
            if (root == null) return result;

            // Queue for BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Add root
            queue.Enqueue(root);

            // BFS traversal
            while (queue.Count > 0)
            {
                // Number of nodes at current level
                int size = queue.Count;

                // Process level
                for (int i = 0; i < size; i++)
                {
                    // Dequeue node
                    TreeNode node = queue.Dequeue();

                    // If last node in level, add to result
                    if (i == size - 1)
                        result.Add(node.val);

                    // Add left child
                    if (node.left != null)
                        queue.Enqueue(node.left);

                    // Add right child
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }
            }

            // Return result
            return result;
        }

        // Left View of Binary Tree
        // Return nodes visible when seen from the left side
        // Take the first node at each level

        public static IList<int> LeftView(TreeNode root)
        {
            // Result list
            List<int> result = new List<int>();

            // If tree is empty
            if (root == null) return result;

            // Queue for BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Add root
            queue.Enqueue(root);

            // BFS traversal
            while (queue.Count > 0)
            {
                // Number of nodes at current level
                int size = queue.Count;

                // Process level
                for (int i = 0; i < size; i++)
                {
                    // Dequeue node
                    TreeNode node = queue.Dequeue();

                    // If first node in level, add to result
                    if (i == 0)
                        result.Add(node.val);

                    // Add left child
                    if (node.left != null)
                        queue.Enqueue(node.left);

                    // Add right child
                    if (node.right != null)
                        queue.Enqueue(node.right);
                }
            }

            // Return result
            return result;
        }

        // Lowest Common Ancestor (LCA)
        // Find the lowest node that has both p and q as descendants

        public static TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            // Base case: if root is null or matches p or q
            if (root == null || root == p || root == q)
                return root;

            // Search in left subtree
            TreeNode left = LowestCommonAncestor(root.left, p, q);

            // Search in right subtree
            TreeNode right = LowestCommonAncestor(root.right, p, q);

            // If both sides return non-null, current node is LCA
            if (left != null && right != null)
                return root;

            // Otherwise return non-null child
            return left != null ? left : right;
        }

        // Root to Node Path
        // Return path from root to given target node

        public static IList<int> RootToNodePath(TreeNode root, int target)
        {
            // List to store path
            List<int> path = new List<int>();

            // Call helper function
            FindPath(root, target, path);

            // Return path
            return path;
        }

        private static bool FindPath(TreeNode node, int target, List<int> path)
        {
            // Base case: if node is null
            if (node == null) return false;

            // Add current node to path
            path.Add(node.val);

            // If current node is target, return true
            if (node.val == target) return true;

            // Check left subtree
            if (FindPath(node.left, target, path)) return true;

            // Check right subtree
            if (FindPath(node.right, target, path)) return true;

            // If not found, remove current node (backtrack)
            path.RemoveAt(path.Count - 1);

            // Return false
            return false;
        }

        // Nodes at Distance K from Target
        // Return all nodes at distance K from a given target node
        // Convert tree into graph using parent mapping, then BFS

        public static IList<int> DistanceK(TreeNode root, TreeNode target, int k)
        {
            // Result list
            List<int> result = new List<int>();

            // Map to store parent of each node
            Dictionary<TreeNode, TreeNode> parentMap = new Dictionary<TreeNode, TreeNode>();

            // Build parent mapping
            BuildParentMap(root, null, parentMap);

            // Set to track visited nodes
            HashSet<TreeNode> visited = new HashSet<TreeNode>();

            // Queue for BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Start BFS from target node
            queue.Enqueue(target);

            // Mark target as visited
            visited.Add(target);

            // Current distance
            int distance = 0;

            // BFS traversal
            while (queue.Count > 0)
            {
                // If reached distance k, stop expanding
                if (distance == k) break;

                // Process current level
                int size = queue.Count;

                for (int i = 0; i < size; i++)
                {
                    // Dequeue node
                    TreeNode node = queue.Dequeue();

                    // Visit left child
                    if (node.left != null && !visited.Contains(node.left))
                    {
                        visited.Add(node.left);
                        queue.Enqueue(node.left);
                    }

                    // Visit right child
                    if (node.right != null && !visited.Contains(node.right))
                    {
                        visited.Add(node.right);
                        queue.Enqueue(node.right);
                    }

                    // Visit parent
                    if (parentMap[node] != null && !visited.Contains(parentMap[node]))
                    {
                        visited.Add(parentMap[node]);
                        queue.Enqueue(parentMap[node]);
                    }
                }

                // Increment distance after each level
                distance++;
            }

            // Remaining nodes in queue are at distance k
            while (queue.Count > 0)
                result.Add(queue.Dequeue().val);

            // Return result
            return result;
        }

        private static void BuildParentMap(TreeNode node, TreeNode parent, Dictionary<TreeNode, TreeNode> map)
        {
            // Base case
            if (node == null) return;

            // Store parent
            map[node] = parent;

            // Recurse left
            BuildParentMap(node.left, node, map);

            // Recurse right
            BuildParentMap(node.right, node, map);
        }

        // Serialize and Deserialize Binary Tree
        // Convert tree to string and reconstruct it back
        // Use level order traversal

        public static string Serialize(TreeNode root)
        {
            // If tree is empty
            if (root == null) return "";

            // Queue for BFS
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Result string builder
            List<string> result = new List<string>();

            // Start with root
            queue.Enqueue(root);

            // BFS traversal
            while (queue.Count > 0)
            {
                // Dequeue node
                TreeNode node = queue.Dequeue();

                // If node is null, add marker
                if (node == null)
                {
                    result.Add("null");
                    continue;
                }

                // Add node value
                result.Add(node.val.ToString());

                // Add children
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }

            // Join into string
            return string.Join(",", result);
        }

        public static TreeNode Deserialize(string data)
        {
            // If string is empty
            if (string.IsNullOrEmpty(data)) return null;

            // Split string
            string[] values = data.Split(',');

            // Create root node
            TreeNode root = new TreeNode(int.Parse(values[0]));

            // Queue for reconstruction
            Queue<TreeNode> queue = new Queue<TreeNode>();

            // Add root
            queue.Enqueue(root);

            // Index for values
            int i = 1;

            // Reconstruct tree
            while (queue.Count > 0)
            {
                // Get current node
                TreeNode node = queue.Dequeue();

                // Process left child
                if (values[i] != "null")
                {
                    node.left = new TreeNode(int.Parse(values[i]));
                    queue.Enqueue(node.left);
                }

                i++;

                // Process right child
                if (values[i] != "null")
                {
                    node.right = new TreeNode(int.Parse(values[i]));
                    queue.Enqueue(node.right);
                }

                i++;
            }

            // Return root
            return root;
        }

        // Construct Binary Tree from Preorder and Inorder
        // Preorder gives root, Inorder splits left and right subtrees

        public static TreeNode PreOrderInorderBuildTree(int[] preorder, int[] inorder)
        {
            // Map to store index of inorder values
            Dictionary<int, int> map = new Dictionary<int, int>();

            // Fill map
            for (int i = 0; i < inorder.Length; i++)
                map[inorder[i]] = i;

            // Call recursive builder
            return PreOrderInorderBuild(preorder, 0, preorder.Length - 1,
                         inorder, 0, inorder.Length - 1, map);
        }

        private static TreeNode PreOrderInorderBuild(int[] preorder, int preStart, int preEnd,
                                      int[] inorder, int inStart, int inEnd,
                                      Dictionary<int, int> map)
        {
            // Base case
            if (preStart > preEnd || inStart > inEnd) return null;

            // Root value from preorder
            int rootVal = preorder[preStart];

            // Create root node
            TreeNode root = new TreeNode(rootVal);

            // Find index in inorder
            int inRoot = map[rootVal];

            // Number of nodes in left subtree
            int numsLeft = inRoot - inStart;

            // Build left subtree
            root.left = PreOrderInorderBuild(preorder, preStart + 1, preStart + numsLeft,
                              inorder, inStart, inRoot - 1, map);

            // Build right subtree
            root.right = PreOrderInorderBuild(preorder, preStart + numsLeft + 1, preEnd,
                               inorder, inRoot + 1, inEnd, map);

            // Return root
            return root;
        }

        public static TreeNode InorderPostOrderBuildTree(int[] inorder, int[] postorder)
        {
            var map = new Dictionary<int, int>();
            for (int i = 0; i < inorder.Length; i++)
            {
                map[inorder[i]] = i;
            }
            TreeNode root = InorderPostOrderHelper(inorder, 0, inorder.Length - 1, postorder, 0, postorder.Length - 1, map);
            return root;
        }

        public static TreeNode InorderPostOrderHelper(int[] inorder, int istart, int iend, int[] postorder, int pstart, int pend, Dictionary<int, int> map)
        {
            if (istart > iend || pstart > pend)
            {
                return null;
            }

            TreeNode root = new TreeNode(postorder[pend]);
            var iroot = map[postorder[pend]];
            var numsLeft = iend - iroot;
            root.right = InorderPostOrderHelper(inorder, iroot + 1, iend, postorder, pend - numsLeft, pend - 1, map);
            root.left = InorderPostOrderHelper(inorder, istart, iroot - 1, postorder, pstart, pend - numsLeft - 1, map);
            return root;
        }


        public static bool IsSymmetric(TreeNode root)
        {
            // Step 1: Empty tree is symmetric
            if (root == null)
            {
                return true;
            }

            // Step 2: Check if left and right subtrees are mirror images
            return Helper(root.left, root.right);
        }

        public static bool Helper(TreeNode root1, TreeNode root2)
        {
            // Step 1: If both nodes are null → symmetric at this level
            if (root1 == null && root2 == null)
            {
                return true;
            }

            // Step 2: If only one is null → not symmetric
            if (root1 == null || root2 == null)
            {
                return false;
            }

            // Step 3: Recursively check mirror structure
            // Compare cross children:
            // left of root1 ↔ right of root2
            bool case1 = Helper(root1.left, root2.right);

            // right of root1 ↔ left of root2
            bool case2 = Helper(root1.right, root2.left);

            // Step 4: Check current values + both subtrees
            return (root1.val == root2.val) && case1 && case2;
        }

        public static List<int> MorrisInorderTraversal(TreeNode root)
        {
            var result = new List<int>();

            // Step 1: Handle empty tree
            if (root == null)
            {
                return result;
            }

            // Step 2: Traverse the tree
            while (root != null)
            {
                // Case 1: No left subtree
                if (root.left == null)
                {
                    // Visit node directly
                    result.Add(root.val);

                    // Move to right subtree
                    root = root.right;
                }
                else
                {
                    // Step 3: Find inorder predecessor (rightmost node in left subtree)
                    var leftNode = root.left;

                    while (leftNode.right != null && leftNode.right != root)
                    {
                        leftNode = leftNode.right;
                    }

                    // Case 2: First time visiting this node
                    if (leftNode.right == null)
                    {
                        // Create temporary thread to come back later
                        // WHY: Avoid using stack/recursion
                        leftNode.right = root;

                        // Move to left subtree
                        root = root.left;
                    }
                    else
                    {
                        // Case 3: Thread already exists → left subtree fully processed

                        // Remove the thread (restore tree)
                        leftNode.right = null;

                        // Visit current node
                        result.Add(root.val);

                        // Move to right subtree
                        root = root.right;
                    }
                }
            }

            return result;
        }

    }
}
