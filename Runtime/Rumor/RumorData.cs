using System;

namespace NpcMentality
{
    [Serializable]
    public class RumorData
    {
        public string OriginalFact;
        public string CurrentVersion;
        public int HopCount;         // how many NPCs it passed through
        public float SpreadAt;
        public string SourceNpcId;

        public RumorData(string fact, string sourceNpcId)
        {
            OriginalFact = fact;
            CurrentVersion = fact;
            HopCount = 0;
            SpreadAt = 0f;
            SourceNpcId = sourceNpcId;
        }
    }
}
