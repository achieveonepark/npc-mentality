using System.Collections.Generic;
using UnityEngine;

namespace NpcMentality
{
    [CreateAssetMenu(menuName = "NPC Mentality/Relationship Graph", fileName = "RelationshipGraph")]
    public class RelationshipGraph : ScriptableObject
    {
        private Dictionary<string, List<string>> friendships = new Dictionary<string, List<string>>();

        public void AddFriendship(string npcA, string npcB)
        {
            if (!friendships.ContainsKey(npcA))
                friendships[npcA] = new List<string>();
            if (!friendships.ContainsKey(npcB))
                friendships[npcB] = new List<string>();

            if (!friendships[npcA].Contains(npcB))
                friendships[npcA].Add(npcB);
            if (!friendships[npcB].Contains(npcA))
                friendships[npcB].Add(npcA);
        }

        public List<string> GetFriends(string npcId)
        {
            if (friendships.TryGetValue(npcId, out var friends))
                return friends;
            return new List<string>();
        }
    }
}
