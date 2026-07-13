using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.Design_Problems
{
    public class Twitter
    {
        Dictionary<int, HashSet<int>> users;
        int count;
        Dictionary<int, List<(int order, int id)>> tweets;
        public Twitter()
        {
            users = new Dictionary<int, HashSet<int>>();
            count = 0;
            tweets = new Dictionary<int, List<(int order, int id)>>();
        }

        public void PostTweet(int userId, int tweetId)
        {
            if (!tweets.ContainsKey(userId))
            {
                tweets[userId] = new List<(int order, int id)>();
            }
            tweets[userId].Add((count, tweetId));
            count++;
        }

        public IList<int> GetNewsFeed(int userId)
        {
            List<int> result = new List<int>();
            if (!users.ContainsKey(userId))
            {
                users[userId] = new HashSet<int>();
            }
            PriorityQueue<(int tId, int index, int uId), int> queue = new PriorityQueue<(int tId, int index, int uId), int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
            users[userId].Add(userId);
            foreach (var user in users[userId])
            {
                if (tweets.ContainsKey(user) && tweets[user].Count > 0)
                {
                    int lastTweetIndex = tweets[user].Count - 1;
                    (int order, int id) = tweets[user][lastTweetIndex];
                    queue.Enqueue((id, lastTweetIndex, user), order);
                }
            }
            int tweetsCount = 0;
            while (queue.Count > 0 && tweetsCount < 10)
            {
                (int tId, int index, int uId) = queue.Dequeue();
                result.Add(tId);
                if (index > 0)
                {
                    (int order, int id) = tweets[uId][index - 1];
                    queue.Enqueue((id, index - 1, uId), order);
                }
                tweetsCount++;
            }
            return result;
        }

        public void Follow(int followerId, int followeeId)
        {
            if (!users.ContainsKey(followerId))
            {
                users[followerId] = new HashSet<int>();
            }
            users[followerId].Add(followeeId);
        }

        public void Unfollow(int followerId, int followeeId)
        {
            if (!users.ContainsKey(followerId))
            {
                return;
            }
            if (users[followerId].Contains(followeeId))
            {
                users[followerId].Remove(followeeId);
            }
        }
    }
}
