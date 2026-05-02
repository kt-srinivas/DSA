using DSA.Topics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Design_Problems.Trees
{
    public class BSTIterator
    {
        public Stack<TreeNode> nodes;
        public BSTIterator(TreeNode root)
        {
            nodes = new Stack<TreeNode>();
            PushAllLNodes(root);
        }

        public void PushAllLNodes(TreeNode node)
        {
            while (node != null)
            {
                nodes.Push(node);
                node = node.left;
            }
        }
        public int Next()
        {
            var node = nodes.Pop();
            if (node.right != null)
            {
                PushAllLNodes(node.right);
            }
            return node.val;
        }

        public bool HasNext()
        {
            return nodes.Count > 0;
        }
    }
}
