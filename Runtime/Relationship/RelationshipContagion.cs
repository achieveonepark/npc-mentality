using System.Collections.Generic;

namespace NpcMentality
{
    public static class RelationshipContagion
    {
        public static void Propagate(
            string sourceNpcId,
            float attitudeChange,
            RelationshipGraph graph,
            Dictionary<string, NPCMemory> allNpcs,
            int depth = 2,
            float decay = 0.5f)
        {
            if (depth <= 0) return;

            var visited = new HashSet<string> { sourceNpcId };
            PropagateRecursive(sourceNpcId, attitudeChange, graph, allNpcs, depth, decay, visited);
        }

        private static void PropagateRecursive(
            string npcId,
            float attitudeChange,
            RelationshipGraph graph,
            Dictionary<string, NPCMemory> allNpcs,
            int remainingDepth,
            float decay,
            HashSet<string> visited)
        {
            if (remainingDepth <= 0) return;

            var friends = graph.GetFriends(npcId);
            float decayedChange = attitudeChange * decay;

            foreach (var friendId in friends)
            {
                if (visited.Contains(friendId)) continue;
                visited.Add(friendId);

                if (allNpcs.TryGetValue(friendId, out var memory))
                {
                    // encode the propagated attitude as a synthetic memory weight
                    EventType contagionEvent = decayedChange >= 0f ? EventType.Help : EventType.Threaten;
                    memory.Remember(contagionEvent, decayedChange, "relationship contagion");
                }

                PropagateRecursive(friendId, attitudeChange, graph, allNpcs, remainingDepth - 1, decay, visited);
            }
        }
    }
}
