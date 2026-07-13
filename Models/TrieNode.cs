using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Models
{
    public class TrieNode
    {
       public TrieNode[] children;
       public bool isEndOfWord;

        public TrieNode()
        {
            children = new TrieNode[26];
            isEndOfWord = false;
        }

        public bool ContainsKey(char ch)
        {
            return children[ch - 'a'] != null;
        }

        public void Put(char ch, TrieNode node)
        {
            children[ch - 'a'] = node;
        }

        public TrieNode Get(char ch)
        {
            return children[ch - 'a'];
        }
    }

    public class BitTrieNode
    {
        public BitTrieNode[] children;
        public BitTrieNode()
        {
            children = new BitTrieNode[2];
        }

        public bool ContainsKey(int bit)
        {
            return children[bit] != null;
        }

        public void Put(int bit, BitTrieNode node)
        {
            children[bit] = node;
        }

        public BitTrieNode Get(int bit)
        {
            return children[bit];
        }

    }
}
