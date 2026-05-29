using System;

namespace NpcMentality
{
    [Serializable]
    public class MemoryEvent
    {
        public EventType Type;
        public float Timestamp;
        public float Weight;   // positive = good, negative = bad
        public string Context; // optional description

        public MemoryEvent(EventType type, float timestamp, float weight = 1f, string context = "")
        {
            Type = type;
            Timestamp = timestamp;
            Weight = weight;
            Context = context;
        }
    }
}
