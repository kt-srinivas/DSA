using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Models
{
    public class Node
    {
        public int data;
        public Node next;
        public Node child;
        public Node random;
        public Node(int data)
        {
            this.data = data;
            next = null;
            child = null;
            random = null;
        }
    }
}
