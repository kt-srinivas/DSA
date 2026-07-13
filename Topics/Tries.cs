using DSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Topics
{
    public static class Tries
    {
        class Trie
        {
            public TrieNode root;
            public Trie()
            {
                root = new TrieNode();
            }

            public void InsertWord(string word)
            {
                var node = root;
                for(int i=0;i< word.Length; i++)
                {
                    char ch = word[i];
                    if (!node.ContainsKey(ch))
                    {
                        node.Put(ch, new TrieNode());
                    }
                    node = node.Get(ch);
                }
                node.isEndOfWord = true;
            }

            public bool Search(string word)
            {
                var node = root;
                for (int i = 0; i < word.Length; i++)
                {
                    char ch = word[i];
                    if (!node.ContainsKey(ch))
                    {
                        return false;
                    }
                    node = node.Get(ch);
                }
                return node.isEndOfWord;
            }

            public bool StartsWith(string prefix)
            {
                var node = root;
                for (int i = 0; i < prefix.Length; i++)
                {
                    char ch = prefix[i];
                    if (!node.ContainsKey(ch))
                    {
                        return false;
                    }
                    node = node.Get(ch);
                }
                return true;
            }

            public bool AllPrefixesExist(string word)
            {
                var node = root;
                for (int i = 0; i < word.Length; i++)
                {
                    char ch = word[i];
                    if (!node.ContainsKey(ch))
                    {
                        return false;
                    }
                    node = node.Get(ch);
                    if (!node.isEndOfWord)
                    {
                        return false;
                    }
                }
                return true;
            }

            public void Delete(string word)
            {
                DeleteHelper(root, word, 0);
            }

            void DeleteHelper(TrieNode node, string word, int index)
            {
                if (index == word.Length)
                {
                    node.isEndOfWord = false;
                    return;
                }

                char ch = word[index];
                if (!node.ContainsKey(ch))
                {
                    return;
                }

                DeleteHelper(node.Get(ch), word, index + 1);

                // Check if the child node can be deleted
                TrieNode childNode = node.Get(ch);
                if (!childNode.isEndOfWord && IsEmpty(childNode))
                {
                    node.Put(ch, null);
                }
            }
            public bool IsEmpty(TrieNode node)
            {
                return node.children.All(child => child == null);
            }

        }


        // Given an array of strings words, find the longest string in words such that every prefix of it is also in words. For example, if words = ["a", "ap", "app", "appl", "apple"], then "apple" is the longest string because all its prefixes ("a", "ap", "app", "appl") are also in words. If there are multiple answers, return the one that is lexicographically smallest. If there is no answer, return an empty string.
        //Time complexity: O(n * m) where n is the number of words and m is the average length of the words. This is because we need to insert each word into the trie and check for prefixes, which takes O(m) time for each word.
        //Space complexity: O(n * m) in the worst case, where n is the number of words and m is the average length of the words. This is because in the worst case, all words could be unique and require separate nodes in the trie.
        public static string LongestPrefixString(string[] words)
        {
            Trie trie = new Trie();
            foreach (var word in words)
            {
                trie.InsertWord(word);
            }

            string result = string.Empty;
            foreach (var word in words)
            {
                if (trie.AllPrefixesExist(word))
                {
                    if (word.Length > result.Length || (word.Length == result.Length && string.Compare(word, result) < 0))
                    {
                        result = word;
                    }
                }
            }
            return result;

        }

        public static int CountDistinctSubstrings(string s)
        {
            Trie trie = new Trie();
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                var node = trie.root;
                for (int j = i; j < s.Length; j++)
                {
                    char ch = s[j];
                    if (!node.ContainsKey(ch))
                    {
                        node.Put(ch, new TrieNode());
                        count++;
                    }
                    node = node.Get(ch);
                }
            }
            return count;
        }

        class BitTrie
        {
            public BitTrieNode root;

            public BitTrie()
            {
                root = new BitTrieNode();
            }

            public void Insert(int num)
            {
                var node = root;
                for (int i = 31; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    if (!node.ContainsKey(bit))
                    {
                        node.Put(bit, new BitTrieNode());
                    }
                    node = node.Get(bit);
                }
            }

            public int MaxXOR(int num)
            {
                var node = root;
                int maxXOR = 0;
                for (int i = 31; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    int toggledBit = 1 - bit;
                    if (node.ContainsKey(toggledBit))
                    {
                        maxXOR |= (1 << i);
                        node = node.Get(toggledBit);
                    }
                    else
                    {
                        node = node.Get(bit);
                    }
                }
                return maxXOR;
            }
        }

        public static int GetMaxXOR(int[] nums)
        {
            BitTrie trie = new BitTrie();
            int maxXOR = 0;
            foreach (var num in nums)
            {
                trie.Insert(num);
            }
            foreach (var num in nums)
            {
                maxXOR = Math.Max(maxXOR, trie.MaxXOR(num));
            }
            return maxXOR;
        }
    }
}
