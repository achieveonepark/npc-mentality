using System;
using System.Collections.Generic;
using UnityEngine;

namespace NpcMentality
{
    [AddComponentMenu("NPC Mentality/Rumor System")]
    public class RumorSystem : MonoBehaviour
    {
        public List<RumorData> ActiveRumors { get; private set; } = new List<RumorData>();

        public float MutationChance = 0.3f;

        public List<string> MutationSuffixes = new List<string>
        {
            "I heard it was actually a wizard.",
            "Someone said it was the demon king.",
            "But maybe it wasn't real?",
            "The details got mixed up somehow."
        };

        public event Action<RumorData> OnRumorSpread;
        public event Action<RumorData, string> OnRumorPassed;

        public void SpreadRumor(string fact, string sourceNpcId)
        {
            var rumor = new RumorData(fact, sourceNpcId)
            {
                SpreadAt = Time.time
            };
            ActiveRumors.Add(rumor);
            OnRumorSpread?.Invoke(rumor);
        }

        public RumorData PassRumor(RumorData rumor, string receivingNpcId)
        {
            rumor.HopCount++;

            if (MutationSuffixes.Count > 0 && UnityEngine.Random.value < MutationChance)
            {
                string suffix = MutationSuffixes[UnityEngine.Random.Range(0, MutationSuffixes.Count)];
                rumor.CurrentVersion = $"{rumor.CurrentVersion} {suffix}";
            }

            OnRumorPassed?.Invoke(rumor, receivingNpcId);
            return rumor;
        }
    }
}
