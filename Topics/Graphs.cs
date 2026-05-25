using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Graphs
    {
        
        public static List<int> BFSUsingAdjList(int source,List<List<int>> graph)
        {
            var result = new List<int>();
            var isVisisted = new bool[graph.Count];
            var queue = new Queue<int>();
            queue.Enqueue(source);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (isVisisted[node])
                {
                    continue;
                }
                isVisisted[node] = true;
                result.Add(node);
                foreach (var neighbour in graph[node])
                {
                    if (!isVisisted[neighbour])
                    {
                        queue.Enqueue(neighbour);
                    }
                }
            }
            return result;
        }

        public static List<int> BFSSuingAdjMatrix(int[][] matrix)
        {
            var result = new List<int>();
            var isVisited = new bool[matrix.Length];
            var queue = new Queue<int>();
            queue.Enqueue(0);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (isVisited[node])
                {
                    continue;
                }
                isVisited[node] = true;
                result.Add(node);
                for (int i = 0; i < matrix[node].Length; i++)
                {
                    if (matrix[node][i] == 1 && !isVisited[i])
                    {
                        queue.Enqueue(i);
                    }
                }
            }
            return result;
        }

        public static List<int>DFS(int source,List<List<int>> graph)
        {
            var isVisited = new bool[graph.Count];
            var result = new List<int>();
            void DFSHelper(int node)
            {
                isVisited[node] = true;
                result.Add(node);
                for(int i = 0; i < graph[node].Count; i++)
                {
                    var neighbour = graph[node][i];
                    if (!isVisited[neighbour])
                    {
                        DFSHelper(neighbour);
                    }
                }
            }
            return result;
        }

        public static List<int> DFSUsingMAtrix(int[][] graph)
        {
            var isVisited = new bool[graph.Length];
            var result = new List<int>();
            for(int i=0;i< graph.Length; i++)
            {
                if (!isVisited[i])
                {
                    DFSHelper(i);
                }
            }
            void DFSHelper(int node)
            {
                isVisited[node] = true;
                result.Add(node);
                for (int i = 0; i < graph[node].Length; i++)
                {
                    if (graph[node][i] == 1 && !isVisited[i])
                    {
                        DFSHelper(i);
                    }
                }
            }
            return result;
        }

        // Leetcode 547. Number of Provinces
        public static int NumberOfProvinces(int[][] graph)
        {
            int count = 0;
            var isVisited = new bool[graph.Length];
            void DFSHelper(int node)
            {
                isVisited[node] = true;
                for (int i = 0; i < graph[node].Length; i++)
                {
                    if (graph[node][i] == 1 && !isVisited[i])
                    {
                        DFSHelper(i);
                    }
                }
            }
            for (int i = 0; i < graph.Length; i++)
            {
                if (!isVisited[i])
                {
                    count++;
                    DFSHelper(i);
                }
            }
            return count;
        }



        // Leetcode 200. Number of Islands
        public static int NumIslands(char[][] grid)
        {

            // Step 1: Count islands
            int count = 0;

            // DFS function
            void DFSHelper(int row, int col)
            {
                // Step 2: Boundary + invalid checks
                if (row < 0 || row >= grid.Length ||
                    col < 0 || col >= grid[row].Length ||
                    grid[row][col] == '0')
                {
                    return;
                }

                // Step 3: Mark current land as visited
                // WHY:
                // Avoid revisiting same cell
                grid[row][col] = '0';

                // Step 4: Explore all 4 directions
                DFSHelper(row - 1, col);
                DFSHelper(row + 1, col);
                DFSHelper(row, col - 1);
                DFSHelper(row, col + 1);
            }

            // Step 5: Traverse entire grid
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    // Found new island
                    if (grid[i][j] == '1')
                    {
                        count++;

                        // Flood fill entire island
                        DFSHelper(i, j);
                    }
                }
            }

            return count;
        }

        // Leetcode 994. Rotting Oranges
        public static int RottenOranges(int[][] matrix)
        {
            // Step 1: Queue for BFS (stores positions of rotten oranges)
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

            // Step 2: Count fresh oranges
            int goodOranges = 0;

            // Step 3: Initialize queue with all rotten oranges (multi-source BFS)
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 2)
                    {
                        // Add rotten orange as starting point
                        queue.Enqueue((i, j));
                    }
                    else if (matrix[i][j] == 1)
                    {
                        // Count fresh oranges
                        goodOranges++;
                    }
                }
            }

            // Step 4: Time counter (each BFS level = 1 minute)
            int time = 0;

            // Step 5: Direction vectors (up, right, down, left)
            var dx = new int[] { -1, 0, 1, 0 };
            var dy = new int[] { 0, 1, 0, -1 };

            // Step 6: BFS traversal
            // Continue while we have rotten oranges to spread AND fresh oranges remaining
            while (queue.Count > 0 && goodOranges > 0)
            {
                int size = queue.Count;

                // Process all oranges at current "time level"
                for (int i = 0; i < size; i++)
                {
                    var rottenOrange = queue.Dequeue();

                    // Check all 4 directions
                    for (int j = 0; j < 4; j++)
                    {
                        int newRow = rottenOrange.x + dx[j];
                        int newColumn = rottenOrange.y + dy[j];

                        // Step 7: Boundary check
                        if (newRow < 0 || newColumn < 0 ||
                           newRow >= matrix.Length || newColumn >= matrix[0].Length)
                        {
                            continue;
                        }

                        // Step 8: If fresh orange found → rot it
                        if (matrix[newRow][newColumn] == 1)
                        {
                            matrix[newRow][newColumn] = 2; // mark as rotten
                            goodOranges--;                // decrease fresh count

                            // Add to queue for next level processing
                            queue.Enqueue((newRow, newColumn));
                        }
                    }
                }

                // Step 9: One level completed → increment time
                time++;
            }

            // Step 10: If all oranges are rotten → return time
            // Else → impossible case
            return goodOranges == 0 ? time : -1;
        }

        public static int[][] DistanceFromZeroes(int[][] matrix)
        {
            // Step 1: Initialize result array
            var result = new int[matrix.Length][];

            // Step 2: Queue for BFS (multi-source)
            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();

            // Step 3: Initialize result rows
            for (int i = 0; i < matrix.Length; i++)
            {
                result[i] = new int[matrix[i].Length];
            }

            // Step 4: Fill initial values
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 1)
                    {
                        // Mark unvisited cells with -1
                        result[i][j] = -1;
                    }
                    else
                    {
                        // All 0s act as starting points
                        queue.Enqueue((i, j));

                        // Distance to itself is 0
                        result[i][j] = 0;
                    }
                }
            }

            // Step 5: Directions (up, right, down, left)
            var dx = new int[] { -1, 0, 1, 0 };
            var dy = new int[] { 0, 1, 0, -1 };

            // Step 6: BFS traversal
            while (queue.Count > 0)
            {
                var size = queue.Count;

                for (int i = 0; i < size; i++)
                {
                    var currentPosition = queue.Dequeue();

                    // Explore all 4 directions
                    for (int j = 0; j < 4; j++)
                    {
                        int nx = currentPosition.x + dx[j];
                        int ny = currentPosition.y + dy[j];

                        // Step 7: Boundary check ⚠️ (FIX HERE)
                        if (nx < 0 || ny < 0 ||
                           nx >= matrix.Length || ny >= matrix[0].Length)
                        {
                            continue;
                        }

                        // Step 8: Visit only unvisited cells (-1)
                        if (result[nx][ny] == -1)
                        {
                            // Distance = parent distance + 1
                            result[nx][ny] = result[currentPosition.x][currentPosition.y] + 1;

                            // Add to queue for further expansion
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
            }

            // Step 9: Return result matrix
            return result;
        }

        public static bool DetectCylceInUndirectedGraph(List<List<int>> graph)
        {
            // Step 1: Track visited nodes
            bool[] isVisited = new bool[graph.Count];

            // DFS function with parent tracking
            bool DFSHelper(int currentNode, int parent)
            {
                // Mark current node as visited
                isVisited[currentNode] = true;

                // Traverse all neighbors
                for (int i = 0; i < graph[currentNode].Count; i++)
                {
                    var neighBour = graph[currentNode][i];

                    // Case 1: If neighbor is not visited → explore it
                    if (!isVisited[neighBour])
                    {
                        // Recursive DFS call
                        if (DFSHelper(neighBour, currentNode))
                        {
                            return true; // cycle found in deeper recursion
                        }
                    }
                    else
                    {
                        // Case 2: Neighbor already visited
                        // Check if it's NOT the parent → cycle detected
                        if (neighBour != parent)
                        {
                            return true;
                        }
                    }
                }

                // No cycle found from this path
                return false;
            }

            // Step 2: Handle disconnected components
            for (int i = 0; i < graph.Count; i++)
            {
                if (!isVisited[i])
                {
                    // Start DFS from unvisited node
                    if (DFSHelper(i, -1)) // parent = -1 (no parent)
                    {
                        return true;
                    }
                }
            }

            // No cycles in any component
            return false;
        }

        public static bool DetectCylceInDirectedGraph(List<List<int>> graph)
        {
            // Step 1: Track visited nodes
            bool[] isVisited = new bool[graph.Count];

            // Step 2: Track nodes in current DFS path (recursion stack)
            bool[] inPath = new bool[graph.Count];

            // DFS function
            bool DFSHelper(int currentNode)
            {
                // Mark node as visited and part of current path
                isVisited[currentNode] = true;
                inPath[currentNode] = true;

                // Traverse all neighbors
                for (int i = 0; i < graph[currentNode].Count; i++)
                {
                    var neighbour = graph[currentNode][i];

                    // Case 1: If not visited → explore
                    if (!isVisited[neighbour])
                    {
                        if (DFSHelper(neighbour))
                        {
                            return true; // cycle found deeper
                        }
                    }
                    else
                    {
                        // Case 2: Already visited AND still in current path
                        // 👉 This means back edge → cycle
                        if (inPath[neighbour])
                        {
                            return true;
                        }
                    }
                }

                // ⚠️ IMPORTANT: Remove node from current path before backtracking
                inPath[currentNode] = false;

                return false;
            }

            // Step 3: Handle disconnected graph
            for (int i = 0; i < graph.Count; i++)
            {
                if (!isVisited[i])
                {
                    if (DFSHelper(i))
                    {
                        return true;
                    }
                }
            }

            // No cycle found
            return false;
        }
        
        public static List<int> TopoLogicalSortDFS(List<List<int>> graph)
        {
            // Step 1: Track visited nodes
            bool[] isVisited = new bool[graph.Count];

            // Step 2: Final result list
            var result = new List<int>();

            // Step 3: Stack to store topo order (reverse)
            var stack = new Stack<int>();

            // DFS function
            void DFSHelper(int currentNode)
            {
                // Mark node as visited
                isVisited[currentNode] = true;

                // Step 4: Visit all neighbors (dependencies)
                for (int i = 0; i < graph[currentNode].Count; i++)
                {
                    var neighbour = graph[currentNode][i];

                    if (!isVisited[neighbour])
                    {
                        DFSHelper(neighbour);
                    }
                }

                // Step 5: Push AFTER visiting neighbors (postorder)
                // WHY: Ensures dependencies come before current node
                stack.Push(currentNode);
            }

            // Step 6: Handle disconnected components
            for (int i = 0; i < graph.Count; i++)
            {
                if (!isVisited[i])
                {
                    DFSHelper(i);
                }
            }

            // Step 7: Reverse order using stack
            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }
        
        public static List<int> TopologicalSortBFS(List<List<int>> graph)
        {
            // Step 1: Compute indegree of each node
            int[] indegree = new int[graph.Count];

            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph[i].Count; j++)
                {
                    indegree[graph[i][j]]++;
                }
            }

            // Step 2: Queue for nodes with indegree 0
            Queue<int> queue = new Queue<int>();

            for (int i = 0; i < indegree.Length; i++)
            {
                if (indegree[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }

            // Step 3: Store result
            List<int> result = new List<int>();

            // Step 4: BFS processing
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                // Add to topo order
                result.Add(node);

                // Step 5: Reduce indegree of neighbors
                for (int i = 0; i < graph[node].Count; i++)
                {
                    int neighbour = graph[node][i];

                    indegree[neighbour]--;

                    // If indegree becomes 0 → add to queue
                    if (indegree[neighbour] == 0)
                    {
                        queue.Enqueue(neighbour);
                    }
                }
            }

            // Step 6: Cycle detection
            // If not all nodes processed → cycle exists
            if (result.Count != graph.Count)
            {
                return new List<int>(); // or throw exception / indicate cycle
            }

            return result;
        }

        // Leetcode 542. 01 Matrix
        public static int[][] DistanceFromZero(int[][] mat)
        {

            // Step 1: Queue for multi-source BFS
            Queue<(int x, int y)> queue = new Queue<(int, int)>();

            // Step 2: Result matrix
            int[][] result = new int[mat.Length][];

            // Step 3: Initialize result matrix
            for (int i = 0; i < result.Length; i++)
            {

                result[i] = new int[mat[i].Length];

                for (int j = 0; j < result[i].Length; j++)
                {

                    // Copy original values
                    result[i][j] = mat[i][j];

                    // Step 4: All 0s are BFS starting points
                    if (result[i][j] == 0)
                    {

                        queue.Enqueue((i, j));
                    }

                    // Step 5: Mark 1s as unvisited
                    // WHY: Distance not calculated yet
                    else if (result[i][j] == 1)
                    {

                        result[i][j] = -1;
                    }
                }
            }

            // Step 6: Direction vectors (up, right, down, left)
            int[] dx = new int[] { -1, 0, 1, 0 };
            int[] dy = new int[] { 0, 1, 0, -1 };

            // Step 7: BFS traversal
            while (queue.Count > 0)
            {

                (int row, int col) = queue.Dequeue();

                // Explore all 4 directions
                for (int i = 0; i < 4; i++)
                {

                    int nr = row + dx[i];
                    int nc = col + dy[i];

                    // Step 8: Boundary + visited check
                    if (nr < 0 || nc < 0 ||
                        nr >= result.Length || nc >= result[0].Length ||
                        result[nr][nc] != -1)
                    {
                        continue;
                    }

                    // Step 9: Distance = parent distance + 1
                    result[nr][nc] = result[row][col] + 1;

                    // Add newly visited cell to queue
                    queue.Enqueue((nr, nc));
                }
            }

            // Step 10: Return final distance matrix
            return result;
        }

        // Leetcode 1020. Number of Enclaves
        public static int NumEnclaves(int[][] grid)
        {

            // Step 1: Get dimensions
            int rows = grid.Length;
            int cols = grid[0].Length;


            // Step 2: Remove boundary-connected land from LEFT boundary
            for (int i = 0; i < rows; i++)
            {

                if (grid[i][0] == 1)
                {
                    Helper(grid, i, 0);
                }
            }

            // Step 3: Remove boundary-connected land from RIGHT boundary
            for (int i = 0; i < rows; i++)
            {

                if (grid[i][cols - 1] == 1)
                {
                    Helper(grid, i, cols - 1);
                }
            }

            // Step 4: Remove boundary-connected land from TOP boundary
            for (int j = 0; j < cols; j++)
            {

                if (grid[0][j] == 1)
                {
                    Helper(grid, 0, j);
                }
            }

            // Step 5: Remove boundary-connected land from BOTTOM boundary
            for (int j = 0; j < cols; j++)
            {

                if (grid[rows - 1][j] == 1)
                {
                    Helper(grid, rows - 1, j);
                }
            }

            // Step 6: Count remaining land cells
            // These are enclaves
            int result = 0;

            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < cols; j++)
                {

                    if (grid[i][j] == 1)
                    {
                        result++;
                    }
                }
            }

            void Helper(int[][] grid, int i, int j)
            {

                // Step 7: Boundary + invalid checks
                if (i < 0 || j < 0 ||
                    i >= grid.Length ||
                    j >= grid[0].Length ||
                    grid[i][j] != 1)
                {

                    return;
                }

                // Step 8: Mark land as visited/safe
                // WHY:
                // This land can reach boundary → not enclave
                grid[i][j] = -1;

                // Step 9: Explore all 4 directions
                Helper(grid, i - 1, j);
                Helper(grid, i, j + 1);
                Helper(grid, i + 1, j);
                Helper(grid, i, j - 1);
            }


            return result;
        }

        // Leetcode 207. Course Schedule
        public static bool CanFinish(int numCourses, int[][] prerequisites)
        {

            // Step 1: Build adjacency list
            // Edge: prerequisite -> course
            List<List<int>> adjList = new List<List<int>>();

            for (int i = 0; i < numCourses; i++)
            {
                adjList.Add(new List<int>());
            }

            // Step 2: Add directed edges
            for (int i = 0; i < prerequisites.Length; i++)
            {
                adjList[prerequisites[i][1]].Add(prerequisites[i][0]);
            }

            // Step 3: Track visited nodes
            bool[] visited = new bool[adjList.Count];

            // Step 4: Track nodes in current DFS path
            // WHY: Detect back edge (cycle)
            bool[] path = new bool[adjList.Count];

            // DFS function
            bool Helper(int currentNode)
            {

                // Mark node as visited
                visited[currentNode] = true;

                // Add node to current recursion path
                path[currentNode] = true;

                // Traverse neighbors
                for (int i = 0; i < adjList[currentNode].Count; i++)
                {

                    int neighbour = adjList[currentNode][i];

                    // Case 1: If not visited → explore
                    if (!visited[neighbour])
                    {

                        if (Helper(neighbour))
                        {
                            return true; // cycle found
                        }
                    }

                    // Case 2: Neighbor already in current path
                    // 👉 Back edge detected → cycle
                    else if (path[neighbour])
                    {
                        return true;
                    }
                }

                // Step 5: Remove node from current path while backtracking
                path[currentNode] = false;

                return false;
            }

            // Step 6: Handle disconnected graph
            for (int i = 0; i < adjList.Count; i++)
            {

                if (!visited[i])
                {

                    // If cycle exists → cannot finish courses
                    if (Helper(i))
                    {
                        return false;
                    }
                }
            }

            // No cycle found
            return true;
        }


        public static bool IsBipartiteDFS (int[][] graph)
        {

            int n = graph.Length;

            // Step 1: Color array
            // 0  -> unvisited
            // 1  -> first color
            // -1 -> second color
            int[] color = new int[n];

            // DFS function
            bool DFS(int node, int currentColor)
            {
                // Step 2: Assign color
                color[node] = currentColor;

                // Step 3: Traverse neighbors
                foreach (int neighbour in graph[node])
                {
                    // If neighbor has same color
                    // graph is not bipartite
                    if (color[neighbour] == currentColor)
                    {
                        return false;
                    }

                    // If uncolored → color with opposite color
                    if (color[neighbour] == 0)
                    {
                        if (!DFS(neighbour, -currentColor))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            // Step 4: Handle disconnected graph
            for (int i = 0; i < n; i++)
            {
                if (color[i] == 0)
                {
                    if (!DFS(i, 1))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // Leetcode 1091. Shortest Path in Binary Matrix
        public static int ShortesPathinBinaryCircuit(int[][] matrix)
        {
            if (matrix[0][0] == 1 || matrix[1][1] == 1)
            {
                return -1;
            }

            bool[][] visited = new bool[matrix.Length][];
            for(int i = 0;i<visited.Length;i++)
            {
                visited[i] = new bool[matrix[i].Length];
            }
            Queue<(int x, int y, int dist)> queue = new Queue<(int x, int y, int dist)>();
            queue.Enqueue((0, 0, 1));
            visited[0][0] = true;
            var dx = new int[] { -1,-1,-1, 0 , 0, 1, 1, 1 };
            var dy = new int[] { -1,0,1,-1,1,-1,0,1 };
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.x == matrix.Length - 1 && current.y == matrix[0].Length - 1)
                {
                    return current.dist;
                }
                for(int i = 0; i < 4; i++)
                {
                    int newX = current.x + dx[i];
                    int newY = current.y + dy[i];
                    if (newX >= 0 && newY >= 0 && newX < matrix.Length && newY < matrix[0].Length
                                               && matrix[newX][newY] == 0 && !visited[newX][newY])
                    {
                        visited[newX][newY] = true;
                        queue.Enqueue((newX, newY, current.dist + 1));
                    }
                }
            }
            return -1;

        }

        // Leetcode 127. Word Ladder
        public static int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {

            // Step 1: Store words in HashSet
            // WHY:
            // O(1) lookup + efficient removal
            HashSet<string> set = new HashSet<string>(wordList);

            // Step 2: If endWord does not exist → impossible
            if (!set.Contains(endWord))
            {
                return 0;
            }

            // Step 3: BFS queue
            // Stores current word + transformation level
            Queue<(string word, int level)> queue = new Queue<(string, int)>();

            // Start BFS from beginWord
            queue.Enqueue((beginWord, 1));

            // Step 4: BFS traversal
            while (queue.Count > 0)
            {

                (string word, int level) = queue.Dequeue();

                // Convert string to char array for modification
                char[] arr = word.ToCharArray();

                // Try changing every character position
                for (int i = 0; i < arr.Length; i++)
                {

                    // Save original character
                    char original = arr[i];

                    // Step 5: Replace current character with 'a' to 'z'
                    for (char ch = 'a'; ch <= 'z'; ch++)
                    {
                        arr[i] = ch;

                        // Generate transformed word
                        string newWord = new string(arr);

                        // Step 6: If target reached
                        if (newWord == endWord)
                        {
                            return level + 1;
                        }

                        // Step 7: If valid unvisited word exists
                        if (set.Contains(newWord))
                        {
                            // Add to BFS queue
                            queue.Enqueue((newWord, level + 1));

                            // Remove from set
                            // WHY:
                            // Prevent revisiting same word
                            set.Remove(newWord);
                        }
                    }

                    // Restore original character
                    // WHY:
                    // Prepare for next position transformation
                    arr[i] = original;
                }
            }

            // No transformation possible
            return 0;
        }


        // Leetcode 785. Is Graph Bipartite?
        public static bool IsBipartiteBFS (int[][] graph)
        {
            int n = graph.Length;

            // colors[i] will store the color assigned to node i.
            // 0: uncolored
            // 1: color group A
            // 2: color group B
            int[] colors = new int[n];

            // Iterate through all nodes. This ensures that even if the graph
            // is disconnected, we attempt to color every connected component.
            for (int i = 0; i < n; i++)
            {
                // If the node 'i' has not been colored yet, it means it belongs to an
                // unvisited connected component. Start a BFS (or DFS) from it.
                if (colors[i] == 0)
                {
                    // Use a queue for Breadth-First Search (BFS).
                    Queue<int> queue = new Queue<int>();
                    queue.Enqueue(i);

                    // Assign the starting node of this component to color group A (1).
                    colors[i] = 1;

                    while (queue.Count > 0)
                    {
                        int u = queue.Dequeue();
                        int currentColor = colors[u];

                        // The color for neighbors should be opposite to the current node's color.
                        // If current node 'u' is color 1, its neighbors should be color 2.
                        // If current node 'u' is color 2, its neighbors should be color 1.
                        int neighborColor = (currentColor == 1) ? 2 : 1;

                        // Explore all neighbors of node 'u'.
                        foreach (int v in graph[u])
                        {
                            if (colors[v] == 0)
                            {
                                // If neighbor 'v' is uncolored, assign it the opposite color
                                // and add it to the queue for further exploration.
                                colors[v] = neighborColor;
                                queue.Enqueue(v);
                            }
                            else if (colors[v] == currentColor)
                            {
                                // If neighbor 'v' is already colored and has the SAME color as 'u',
                                // then we have found an edge connecting two nodes of the same color.
                                // This indicates the presence of an odd-length cycle, which means
                                // the graph is not bipartite.
                                return false;
                            }
                            // If colors[v] != 0 and colors[v] != currentColor,
                            // it means colors[v] is already set to neighborColor, which is consistent.
                            // No action is needed in this case as it's already correctly colored.
                        }
                    }
                }
            }

            // If the entire graph (all connected components) has been traversed
            // without encountering any conflicts, then the graph is bipartite.
            return true;
        }

        public static int[] Dijkstra( List<List<(int to, int weight)>> graph,int source)
        {
            int n = graph.Count;

            int[] dist = new int[n];

            Array.Fill(dist, int.MaxValue);

            dist[source] = 0;

            // (node, priority)
            PriorityQueue<int, int> pq =
                new PriorityQueue<int, int>();

            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                pq.TryDequeue(out int node, out int currDist);

                // Skip old entries
                if (currDist > dist[node])
                    continue;

                foreach (var (neighbor, weight) in graph[node])
                {
                    int newDist = currDist + weight;

                    if (newDist < dist[neighbor])
                    {
                        dist[neighbor] = newDist;

                        pq.Enqueue(neighbor, newDist);
                    }
                }
            }

            return dist;
        }

        //Leetcode 802. Find Eventual Safe States
        public static IList<int> EventualSafeNodes(int[][] graph)
        {
            // Step 1: Number of nodes
            int n = graph.Length;

            // Step 2: Reverse graph
            // WHY:
            // We want to traverse backwards
            List<int>[] reverse = new List<int>[n];

            for (int i = 0; i < n; i++)
            {
                reverse[i] = new List<int>();
            }

            // Step 3: Store outdegree of each node
            int[] outDegree = new int[n];

            for (int node = 0; node < n; node++)
            {
                // Number of outgoing edges
                outDegree[node] = graph[node].Length;

                // Build reverse graph
                foreach (int nei in graph[node])
                {
                    reverse[nei].Add(node);
                }
            }

            // Step 4: Queue for terminal nodes
            Queue<int> q = new Queue<int>();

            for (int i = 0; i < n; i++)
            {
                // Terminal nodes are automatically safe
                if (outDegree[i] == 0)
                {
                    q.Enqueue(i);
                }
            }

            // Step 5: Store safe nodes
            List<int> safe = new List<int>();

            // Step 6: BFS traversal
            while (q.Count > 0)
            {
                int node = q.Dequeue();

                // Current node is safe
                safe.Add(node);

                // Traverse reverse neighbors
                foreach (int prev in reverse[node])
                {
                    // Remove outgoing edge leading to unsafe path
                    outDegree[prev]--;

                    // If all outgoing edges removed
                    // node becomes safe
                    if (outDegree[prev] == 0)
                    {
                        q.Enqueue(prev);
                    }
                }
            }

            // Step 7: Return sorted result
            safe.Sort();

            return safe;
        }

        //Leetcode 1631. Path With Minimum Effort
        public static int MinimumEffortPath(int[][] heights)
        {

            // Step 1: Grid dimensions
            int rows = heights.Length;
            int cols = heights[0].Length;

            // Step 2: Effort matrix
            // effort[i,j] = minimum effort needed to reach cell
            int[,] effort = new int[rows, cols];

            // Initialize all efforts to infinity
            for (int i = 0; i < rows; i++)
            {

                for (int j = 0; j < cols; j++)
                {

                    effort[i, j] = int.MaxValue;
                }
            }

            // Step 3: Min heap (priority queue)
            // Stores cell positions with current minimum effort
            PriorityQueue<(int x, int y), int> pq =
                new PriorityQueue<(int x, int y), int>();

            // Start from top-left cell
            pq.Enqueue((0, 0), 0);

            effort[0, 0] = 0;

            // Direction vectors
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            // Step 4: Dijkstra traversal
            while (pq.Count > 0)
            {

                // Extract cell with minimum effort
                pq.TryDequeue(out var node, out int currEffort);

                int x = node.x;
                int y = node.y;

                // Step 5: Destination reached
                // WHY:
                // Dijkstra guarantees first reach is optimal
                if (x == rows - 1 && y == cols - 1)
                {

                    return currEffort;
                }

                // Step 6: Explore all 4 directions
                for (int i = 0; i < 4; i++)
                {

                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    // Boundary check
                    if (nx < 0 || ny < 0 || nx >= rows || ny >= cols)
                        continue;

                    // Step 7: Current edge cost
                    int edgeDiff =
                        Math.Abs(heights[x][y] - heights[nx][ny]);

                    // Step 8: Path effort calculation
                    // Path effort = maximum edge difference seen so far
                    int newEffort =
                        Math.Max(currEffort, edgeDiff);

                    // Step 9: Relaxation step
                    if (newEffort < effort[nx, ny])
                    {

                        effort[nx, ny] = newEffort;

                        pq.Enqueue((nx, ny), newEffort);
                    }
                }
            }

            return 0;
        }

        //Leetcode 787. Cheapest Flights Within K Stops
        public static  int FindCheapestPrice(int n, int[][] flights, int src, int dst, int k)
        {
            List<List<(int to, int price)>> graph = new List<List<(int, int)>>();
            // Initialize adjacency list for all cities
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<(int, int)>());
            }
            // Build graph using flights array
            for (int i = 0; i < flights.Length; i++)
            {
                // Extract source city
                int from = flights[i][0];
                // Extract destination city
                int to = flights[i][1];
                // Extract flight price
                int price = flights[i][2];
                // Add directed edge into graph
                graph[from].Add((to, price));
            }
            // Distance array to store minimum cost for each city
            int[] dist = new int[n];
            // Initialize all costs to infinity
            Array.Fill(dist, int.MaxValue);
            // Source city cost is zero
            dist[src] = 0;
            // Queue stores (stops used, current node, total cost)
            Queue<(int stops, int node, int cost)> queue = new Queue<(int, int, int)>();
            // Start BFS from source city
            queue.Enqueue((0, src, 0));
            // BFS traversal
            while (queue.Count > 0)
            {
                // Remove current state from queue
                var current = queue.Dequeue();
                // Extract current number of stops
                int stops = current.stops;
                // Extract current city
                int node = current.node;
                // Extract current accumulated cost
                int cost = current.cost;
                // Ignore paths exceeding stop limit
                if (stops > k)
                {
                    continue;
                }
                // Traverse all neighboring flights
                foreach (var neighbour in graph[node])
                {
                    // Extract neighboring city
                    int nextNode = neighbour.to;
                    // Extract edge cost
                    int nextCost = neighbour.price;
                    // Calculate total path cost
                    int totalCost = cost + nextCost;
                    // Relax edge if cheaper path found
                    if (totalCost < dist[nextNode])
                    {
                        // Update minimum cost
                        dist[nextNode] = totalCost;
                        // Push updated state into queue
                        queue.Enqueue((stops + 1, nextNode, totalCost));
                    }
                }
            }
            // Return -1 if destination unreachable
            return dist[dst] == int.MaxValue ? -1 : dist[dst];
        }

        //Leetcode 1976. Number of Ways to Arrive at Destination
        public static int CountPaths(int n, int[][] roads)
        {

            // Mod value to avoid integer overflow
            var mod = 1000000007;

            // Create adjacency list storing (neighbour, travel time)
            List<List<(int to, int time)>> graph = new List<List<(int, int)>>();

            // Initialize adjacency list for all nodes
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<(int, int)>());
            }

            // Build undirected weighted graph
            for (int i = 0; i < roads.Length; i++)
            {

                // Extract source node
                int from = roads[i][0];

                // Extract destination node
                int to = roads[i][1];

                // Extract edge weight
                int time = roads[i][2];

                // Add forward edge
                graph[from].Add((to, time));

                // Add backward edge because graph is undirected
                graph[to].Add((from, time));
            }

            // Distance array stores shortest distance to each node
            long[] dist = new long[n];

            // Ways array stores number of shortest paths
            long[] ways = new long[n];

            // Initialize all distances to infinity
            Array.Fill(dist, long.MaxValue);

            // Source node distance is zero
            dist[0] = 0;

            // One way exists to reach source node
            ways[0] = 1;

            // Min heap stores (node, current shortest distance)
            PriorityQueue<(int val, long time), long> pq =
                new PriorityQueue<(int, long), long>();

            // Start Dijkstra from source node
            pq.Enqueue((0, 0), 0);

            // Dijkstra traversal
            while (pq.Count > 0)
            {

                // Remove node with minimum distance
                var node = pq.Dequeue();

                // Extract current node value
                int val = node.val;

                // Ignore outdated heap entries
                if (node.time > dist[val])
                {
                    continue;
                }

                // Traverse all neighboring nodes
                foreach (var neighbour in graph[val])
                {

                    // Extract neighboring node
                    int to = neighbour.to;

                    // Extract edge weight
                    int time = neighbour.time;

                    // Calculate new shortest distance
                    long newDist = dist[val] + time;

                    // Found strictly shorter path
                    if (newDist < dist[to])
                    {

                        // Update shortest distance
                        dist[to] = newDist;

                        // Copy number of shortest paths
                        ways[to] = ways[val];

                        // Push updated node into heap
                        pq.Enqueue((to, dist[to]), dist[to]);
                    }

                    // Found another shortest path
                    else if (newDist == dist[to])
                    {

                        // Add number of shortest paths
                        ways[to] = (ways[to] + ways[val]) % mod;
                    }
                }
            }

            // Return total shortest paths to destination
            return (int)(ways[n - 1] % mod);
        }

        public static int MinNumberOfMultiplications(int[] arr, int start, int end)
        {
            int mod = 100000;
            Queue<(int val, int steps)> queue = new Queue<(int, int)>();
            int[] dist = new int[mod];
            queue.Enqueue((start, 0));
            Array.Fill(dist, int.MaxValue);
            dist[start] = 0;
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                int val = current.val;
                int steps = current.steps;
                if (val == end)
                {
                    return steps;
                }
                for (int i = 0; i < arr.Length; i++)
                {
                    int newVal = (val * arr[i]) % mod;
                    if (steps + 1 < dist[newVal])
                    {
                        dist[newVal] = steps + 1;
                        queue.Enqueue((newVal, steps + 1));
                    }
                }
            }
            return -1;

        }

        public static int[] BellManFordAlgorthm(List<(int u, int v, int wt)> Edges, int source, int n)
        {
            int[] dist = new int[n];
            Array.Fill(dist, int.MaxValue);
            dist[source] = 0;
            for (int i = 0; i < n - 1; i++)
            {
                foreach (var (u, v, wt) in Edges)
                {
                    if (dist[u] != int.MaxValue && dist[u] + wt < dist[v])
                    {
                        dist[v] = dist[u] + wt;
                    }
                }
            }

            foreach (var (u, v, wt) in Edges)
            {
                if (dist[u] != int.MaxValue && dist[u] + wt < dist[v])
                {
                    Console.WriteLine("Negative weight cycle detected");
                }
            }

            return dist;
        }

        public static int[][] FloydWarshalAlgorithm(int[][] matrix)
        {
            for(int k=0;k< matrix.Length; k++)
            {
                for(int i=0;i<matrix.Length;i++)
                {
                    for(int j=0;j<matrix.Length;j++)
                    {
                        if (matrix[i][k] != int.MaxValue && matrix[k][j] != int.MaxValue)
                        {
                            matrix[i][j] = Math.Min(matrix[i][j], matrix[i][k] + matrix[k][j]);
                        }
                    }
                }
            }

            for(int i=0;i<matrix.Length; i++)
            {
                if (matrix[i][i] < 0)
                {
                    Console.WriteLine("Negative weight cycle detected");
                }
            }

            return matrix;
        }

        public static int FindTheCity(int n, int[][] edges, int distanceThreshold)
        {

            // Create adjacency matrix to store shortest distances
            int[][] matrix = new int[n][];

            // Initialize each row of matrix
            for (int i = 0; i < n; i++)
            {
                matrix[i] = new int[n];
            }

            // Initially assume all nodes are unreachable
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {

                    // Infinity represents no direct path
                    matrix[i][j] = int.MaxValue;
                }
            }

            // Distance from node to itself is always zero
            for (int i = 0; i < n; i++)
            {
                matrix[i][i] = 0;
            }

            // Fill direct edge distances
            for (int i = 0; i < edges.Length; i++)
            {

                // Extract source node
                int u = edges[i][0];

                // Extract destination node
                int v = edges[i][1];

                // Extract edge weight
                int weight = edges[i][2];

                // Store edge weight because graph is undirected
                matrix[u][v] = weight;
                matrix[v][u] = weight;
            }

            // Floyd Warshall Algorithm
            // Try every node as intermediate node
            for (int k = 0; k < n; k++)
            {

                // Traverse all source nodes
                for (int i = 0; i < n; i++)
                {

                    // Traverse all destination nodes
                    for (int j = 0; j < n; j++)
                    {

                        // Ignore impossible paths
                        if (matrix[i][k] != int.MaxValue &&
                           matrix[k][j] != int.MaxValue)
                        {

                            // Relax shortest distance using node k
                            matrix[i][j] =
                                Math.Min(
                                    matrix[i][k] + matrix[k][j],
                                    matrix[i][j]
                                );
                        }
                    }
                }
            }

            // Stores reachable cities count for every city
            int[] ways = new int[n];

            // Tracks minimum reachable cities found so far
            int minWays = int.MaxValue;

            // Final answer city
            int result = -1;

            // Traverse every city
            for (int i = 0; i < n; i++)
            {

                // Check all neighboring cities
                for (int j = 0; j < n; j++)
                {

                    // Ignore self node and unreachable cities
                    if (i == j || matrix[i][j] > distanceThreshold)
                    {
                        continue;
                    }

                    else
                    {

                        // Count reachable cities within threshold
                        ways[i]++;
                    }
                }

                // Update answer if smaller count found
                // Also handles tie by preferring larger index
                if (ways[i] <= minWays)
                {

                    // Store new minimum count
                    minWays = ways[i];

                    // Store current city
                    result = i;
                }
            }

            // Return required city
            return result;
        }

        public static int PrimsAlgorithm(List<(int u, int v, int weight)> edges, int n)
        {
            // Stores total weight of Minimum Spanning Tree
            int result = 0;

            // Adjacency list storing (neighbour, edge weight)
            List<List<(int to, int weight)>> graph = new List<List<(int, int)>>();

            // Stores MST edges if reconstruction is needed
            List<List<(int, int)>> mst = new List<List<(int, int)>>();

            // Initialize adjacency list for all nodes
            for (int i = 0; i < n; i++)
            {
                graph.Add(new List<(int, int)>());
            }

            // Build undirected weighted graph
            foreach (var edge in edges)
            {
                // Add forward edge
                graph[edge.u].Add((edge.v, edge.weight));

                // Add backward edge because graph is undirected
                graph[edge.v].Add((edge.u, edge.weight));
            }

            // Min heap stores (current node, parent node, edge weight)
            PriorityQueue<(int node, int parent, int wt), int> pq =
                new PriorityQueue<(int, int, int), int>();

            // Tracks nodes already included in MST
            bool[] visited = new bool[n];

            // Start Prim's Algorithm from node 0
            pq.Enqueue((0, -1, 0), 0);

            // Continue until all reachable nodes processed
            while (pq.Count > 0)
            {
                // Extract edge with minimum weight
                var current = pq.Dequeue();

                // Extract current node
                int node = current.node;

                // Extract parent node used to reach current node
                int parent = current.parent;

                // Extract edge weight
                int wt = current.wt;

                // Skip node if already included in MST
                if (visited[node])
                {
                    continue;
                }

                // Mark current node as part of MST
                visited[node] = true;

                // Add edge weight into final MST cost
                result += wt;

                // Ignore dummy starting edge
                if (parent != -1)
                {
                    // Store MST edge
                    mst.Add(new List<(int, int)> { (parent, node) });
                }

                // Traverse all neighboring edges
                foreach (var neighbour in graph[node])
                {
                    // Only consider unvisited nodes
                    if (!visited[neighbour.to])
                    {
                        // Push neighboring edge into min heap
                        pq.Enqueue(
                            (neighbour.to, node, neighbour.weight),
                            neighbour.weight);
                    }
                }
            }

            // Return total MST weight
            return result;
        }

        public class DisjointSetUnion
        {
            // Stores parent of each node
            private int[] parent;

            // Stores approximate tree height for union by rank
            private int[] rank;

            public DisjointSetUnion(int size)
            {
                // Initialize parent array
                parent = new int[size];

                // Initialize rank array
                rank = new int[size];

                // Initially every node is its own parent
                for (int i = 0; i < size; i++)
                {
                    parent[i] = i;

                    // Initial rank is zero
                    rank[i] = 0;
                }
            }

            public int Find(int x)
            {
                // If node is not its own parent keep moving upward
                if (parent[x] != x)
                {
                    // Path compression flattens tree for faster future queries
                    parent[x] = Find(parent[x]);
                }

                // Return ultimate parent of set
                return parent[x];
            }

            public void Union(int x, int y)
            {
                // Find representative parent of first node
                int rootX = Find(x);

                // Find representative parent of second node
                int rootY = Find(y);

                // Only union if both nodes belong to different sets
                if (rootX != rootY)
                {
                    // Attach smaller rank tree under larger rank tree
                    if (rank[rootX] > rank[rootY])
                    {
                        parent[rootY] = rootX;
                    }

                    // Attach smaller rank tree under larger rank tree
                    else if (rank[rootX] < rank[rootY])
                    {
                        parent[rootX] = rootY;
                    }

                    // If ranks equal choose one root and increase its rank
                    else
                    {
                        parent[rootY] = rootX;

                        // Increase rank because tree height increases
                        rank[rootX]++;
                    }
                }
            }
        }

        public static int KruskalsAlgorithm(
            List<(int u, int v, int weight)> edges,
            int n)
        {
            // Initialize Disjoint Set Union structure
            var set = new DisjointSetUnion(n);

            // Stores total MST weight
            int result = 0;

            // Stores MST edges if reconstruction needed
            List<List<(int, int)>> MST =
                new List<List<(int, int)>>();

            // Sort edges by increasing edge weight
            edges.Sort((a, b) => a.weight.CompareTo(b.weight));

            // Traverse edges in sorted order
            foreach (var edge in edges)
            {
                // Check if edge forms cycle
                if (set.Find(edge.u) != set.Find(edge.v))
                {
                    // Merge both components
                    set.Union(edge.u, edge.v);

                    // Add edge into MST
                    MST.Add(new List<(int, int)>
            {
                (edge.u, edge.v)
            });

                    // Add edge weight into MST cost
                    result += edge.weight;
                }
            }

            // Return total MST weight
            return result;
        }

        public static int MakeConnected(int n, int[][] connections)
        {

            // Initialize Disjoint Set Union for all computers
            DisjointSetUnion dsu = new DisjointSetUnion(n);

            // Counts extra edges that form cycles
            // These edges can later be reused to connect components
            int cyclicEdges = 0;

            // Traverse all given connections
            for (int i = 0; i < connections.Length; i++)
            {

                // Extract first computer
                int u = connections[i][0];

                // Extract second computer
                int v = connections[i][1];

                // If both nodes already belong to same component
                // then this edge is redundant
                if (dsu.Find(u) == dsu.Find(v))
                {

                    // Store extra reusable edge
                    cyclicEdges++;
                }

                else
                {

                    // Merge both disconnected components
                    dsu.Union(u, v);
                }
            }

            // Counts number of disconnected components
            int totalComponents = 0;

            // Traverse all nodes
            for (int i = 0; i < n; i++)
            {

                // Ultimate parent represents one component
                if (dsu.Find(i) == i)
                {

                    // Count connected component
                    totalComponents++;
                }
            }

            // Already fully connected
            if (totalComponents == 1)
            {
                return 0;
            }

            // To connect k components we need k-1 edges
            // Check whether extra edges are sufficient
            return cyclicEdges >= totalComponents - 1

                // Minimum operations needed
                ? Math.Min(cyclicEdges, totalComponents - 1)

                // Impossible to connect all computers
                : -1;
        }

        public static int RemoveStones(int[][] stones)
        {
            // Total number of stones
            int n = stones.Length;

            // Initialize Disjoint Set Union
            // Each stone initially belongs to its own component
            DisjointSetUnion dsu = new DisjointSetUnion(n);

            // Compare every pair of stones
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    // Stones can be connected if:
                    // same row OR same column
                    if (stones[i][0] == stones[j][0] ||
                        stones[i][1] == stones[j][1])
                    {
                        // Merge both stones into same component
                        dsu.Union(i, j);
                    }
                }
            }

            // Counts number of disconnected components
            int components = 0;

            // Traverse all stones
            for (int i = 0; i < n; i++)
            {
                // Ultimate parent represents one connected component
                if (dsu.Find(i) == i)
                {
                    // Count component
                    components++;
                }
            }

            // In one connected component:
            // all stones except one can be removed
            return n - components;
        }

        public static IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            // Initialize DSU for all account indices
            // Each account initially belongs to separate component
            DisjointSetUnion dsu = new DisjointSetUnion(accounts.Count);

            // Maps each email to the first account index where it appeared
            Dictionary<string, int> emailToIndex =
                new Dictionary<string, int>();

            // Traverse all accounts
            for (int i = 0; i < accounts.Count; i++)
            {
                // Start from 1 because index 0 stores account holder name
                for (int j = 1; j < accounts[i].Count; j++)
                {
                    // Extract current email
                    string email = accounts[i][j];

                    // First time seeing this email
                    if (!emailToIndex.ContainsKey(email))
                    {
                        // Store owner account index of this email
                        emailToIndex[email] = i;
                    }

                    else
                    {
                        // Same email already exists
                        // Means both accounts belong to same person
                        dsu.Union(i, emailToIndex[email]);
                    }
                }
            }

            // Groups emails according to DSU parent component
            Dictionary<int, List<string>> merged =
                new Dictionary<int, List<string>>();

            // Traverse all unique emails
            foreach (var item in emailToIndex)
            {
                // Extract email
                string email = item.Key;

                // Extract original account index of email
                int accountIndex = item.Value;

                // Find ultimate parent component
                int parent = dsu.Find(accountIndex);

                // Create new email bucket for this component if absent
                if (!merged.ContainsKey(parent))
                {
                    merged[parent] = new List<string>();
                }

                // Add email into merged component
                merged[parent].Add(email);
            }

            // Final merged answer list
            IList<IList<string>> result =
                new List<IList<string>>();

            // Traverse all merged components
            foreach (var item in merged)
            {
                // Extract DSU parent
                int parent = item.Key;

                // Extract email list belonging to this component
                List<string> emails = item.Value;

                // Sort emails in strict lexicographical order
                emails.Sort(StringComparer.Ordinal);

                // Stores current merged account
                List<string> current =
                    new List<string>();

                // Add account holder name
                // Any account inside component has same owner
                current.Add(accounts[parent][0]);

                // Add all sorted emails
                current.AddRange(emails);

                // Store merged account into final answer
                result.Add(current);
            }

            // Return all merged accounts
            return result;
        }
    }
}
