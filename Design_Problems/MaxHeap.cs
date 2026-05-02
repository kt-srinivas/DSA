public class MaxHeap
{
    public List<int> heap;

    public MaxHeap()
    {
        // Step 1: Initialize empty list
        // WHY: Array representation of heap
        heap = new List<int>();
    }

    public void Insert(int val)
    {
        // Step 2: Add element at the end (maintains complete tree property)
        heap.Add(val);

        // Step 3: Heapify Up (bubble up)
        // WHY: Fix violation of max-heap property
        int i = heap.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;

            // If parent is smaller → swap
            if (heap[parent] < heap[i])
            {
                int temp = heap[parent];
                heap[parent] = heap[i];
                heap[i] = temp;

                // Move up to parent index
                i = parent;
            }
            else
            {
                // Heap property satisfied
                break;
            }
        }
    }

    public void Remove()
    {
        // Step 4: Handle empty heap
        if (heap.Count == 0)
        {
            return;
        }

        // Step 5: Replace root with last element
        // WHY: Maintain complete binary tree structure
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);

        // Step 6: Heapify Down (bubble down)
        // WHY: Restore max-heap property
        int i = 0;

        while (i < heap.Count)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            int largest = i;

            // Check left child
            if (left < heap.Count && heap[left] > heap[largest])
            {
                largest = left;
            }

            // Check right child
            if (right < heap.Count && heap[right] > heap[largest])
            {
                largest = right;
            }

            // If one of children is larger → swap
            if (largest != i)
            {
                int temp = heap[i];
                heap[i] = heap[largest];
                heap[largest] = temp;

                // Move down to swapped child
                i = largest;
            }
            else
            {
                // Heap property restored
                break;
            }
        }
    }

    public int GetMax()
    {
        // Step 7: Return root (maximum element)
        if (heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty");
        }

        return heap[0];
    }
}

// ========================= BINARY TREE (ARRAY REPRESENTATION) =========================

// 👉 Used mainly for COMPLETE BINARY TREES (like heaps)
// 👉 Stored in array/list in LEVEL ORDER (BFS)

// -----------------------------------------------------------------------------
// 🧠 CORE IDEA
// -----------------------------------------------------------------------------

// Nodes are stored level by level from left to right
// No gaps → ensures complete structure

// Example:
// Tree:
//         10
//        /  \
//       5    8
//      / \  /
//     2  3 6

// Array:
// [10, 5, 8, 2, 3, 6]

// -----------------------------------------------------------------------------
// 🧠 INDEX RELATIONSHIPS (0-BASED INDEXING)
// -----------------------------------------------------------------------------

// For a node at index i:

// Parent index → (i - 1) / 2
// Left child   → 2*i + 1
// Right child  → 2*i + 2

// -----------------------------------------------------------------------------
// ⚠️ IMPORTANT CONDITIONS
// -----------------------------------------------------------------------------

// Root node (index 0) has NO parent

// A child exists ONLY IF its index < array size

// Left child exists  → (2*i + 1) < n
// Right child exists → (2*i + 2) < n

// -----------------------------------------------------------------------------
// 🧠 WHY THESE FORMULAS WORK
// -----------------------------------------------------------------------------

// Because tree is filled level by level (like BFS)
// Each level doubles nodes:

// Level 0 → 1 node
// Level 1 → 2 nodes
// Level 2 → 4 nodes

// This doubling leads to:
// Left = 2*i + 1
// Right = 2*i + 2

// -----------------------------------------------------------------------------
// 🧠 LEAF NODE IDENTIFICATION
// -----------------------------------------------------------------------------

// A node is a leaf if it has NO children

// Condition:
// 2*i + 1 >= n

// Meaning: no left child → no children at all

// -----------------------------------------------------------------------------
// 🧠 LAST NON-LEAF NODE
// -----------------------------------------------------------------------------

// Index of last node that has at least one child:

// (n / 2) - 1

// WHY:
// All nodes after this index are leaves

// -----------------------------------------------------------------------------
// 🧠 HEIGHT OF TREE
// -----------------------------------------------------------------------------

// Height of complete binary tree:

// h = floor(log2(n))

// WHY:
// Nodes double at each level

// -----------------------------------------------------------------------------
// 🧠 WHEN TO USE ARRAY REPRESENTATION
// -----------------------------------------------------------------------------

// ✅ Best for:
// - Heaps (Min/Max Heap)
// - Priority Queues
// - Complete Binary Trees

// ❌ Not good for:
// - Sparse trees
// - Skewed trees
// (wastes space due to empty positions)

// -----------------------------------------------------------------------------
// 🧠 INSERTION (HEAP CONTEXT)
// -----------------------------------------------------------------------------

// Step 1: Insert at end (keeps tree complete)
// Step 2: Move upward (heapify up)

// -----------------------------------------------------------------------------
// 🧠 DELETION (HEAP CONTEXT)
// -----------------------------------------------------------------------------

// Step 1: Replace root with last element
// Step 2: Remove last element
// Step 3: Move downward (heapify down)

// -----------------------------------------------------------------------------
// 🧠 TRAVERSAL NOTE
// -----------------------------------------------------------------------------

// Array representation naturally gives LEVEL ORDER (BFS)

// For:
// - Inorder
// - Preorder
// - Postorder

// You need recursion or stack (array alone not enough)

// -----------------------------------------------------------------------------
// 🧠 1-BASED INDEXING (ALTERNATIVE)
// -----------------------------------------------------------------------------

// If indexing starts from 1:

// Parent → i / 2
// Left   → 2*i
// Right  → 2*i + 1

// Easier math but wastes index 0

// -----------------------------------------------------------------------------
// 🔁 QUICK REVISION TRIGGER
// -----------------------------------------------------------------------------

// Parent → (i-1)/2
// Left   → 2*i+1
// Right  → 2*i+2

// Works ONLY for complete trees

// -----------------------------------------------------------------------------
// 🧠 ONE-LINE MEMORY TRICK
// -----------------------------------------------------------------------------

// “Double for children, halve (minus one) for parent”
// =============================================================================