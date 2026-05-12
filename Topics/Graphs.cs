using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
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


        public static int NoOfIslands(int[][] grid)
        {
            int count = 0;
            var isVisited = new bool[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                isVisited[i] = new bool[grid[i].Length];
            }
            void DFSHelper(int row, int col)
            {
                if (row < 0 || row >= grid.Length || col < 0 || col >= grid[row].Length || grid[row][col] == 0 || isVisited[row][col])
                {
                    return;
                }
                isVisited[row][col] = true;
                DFSHelper(row - 1, col);
                DFSHelper(row + 1, col);
                DFSHelper(row, col - 1);
                DFSHelper(row, col + 1);
            }
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 1 && !isVisited[i][j])
                    {
                        count++;
                        DFSHelper(i, j);
                    }
                }
            }
            return count;
        }


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

        public static int[] BellManFordAlgorthm(List<(int u, int v, int wt)> Edges, int source, int n)
        {
            int[] dist = new int[n];
            Array.Fill(dist, int.MaxValue);
            dist[source] = 0;
            for(int i=0;i<n-1; i++)
            {
                foreach(var (u,v,wt) in Edges)
                {
                    if (dist[u] != int.MaxValue && dist[u] + wt < dist[v])
                    {
                        dist[v] = dist[u] + wt;
                    }
                }
            }

            foreach(var (u, v, wt) in Edges)
            {
                if (dist[u] != int.MaxValue && dist[u] + wt < dist[v])
                {
                    Console.WriteLine("Negative weight cycle detected");
                }
            }

            return dist;
        }
    }
}
