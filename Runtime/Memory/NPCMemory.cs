using System.Collections.Generic;
using UnityEngine;

namespace NpcMentality
{
    [AddComponentMenu("NPC Mentality/NPC Memory")]
    public class NPCMemory : MonoBehaviour
    {
        public List<MemoryEvent> memories = new List<MemoryEvent>();
        public int MaxMemories = 20;

        public void Remember(EventType type, float weight = 1f, string context = "")
        {
            if (memories.Count >= MaxMemories)
                ForgetOldest();

            memories.Add(new MemoryEvent(type, Time.time, weight, context));
        }

        public float GetAttitude()
        {
            float total = 0f;
            foreach (var m in memories)
                total += m.Weight;
            return total;
        }

        public bool HasMemory(EventType type)
        {
            foreach (var m in memories)
            {
                if (m.Type == type)
                    return true;
            }
            return false;
        }

        public void ForgetOldest()
        {
            if (memories.Count == 0) return;

            int oldestIndex = 0;
            for (int i = 1; i < memories.Count; i++)
            {
                if (memories[i].Timestamp < memories[oldestIndex].Timestamp)
                    oldestIndex = i;
            }
            memories.RemoveAt(oldestIndex);
        }
    }
}
