using System;
using System.Collections.Generic;
using UnityEngine;

namespace NpcMentality
{
    [AddComponentMenu("NPC Mentality/AI Quest Generator")]
    public class AIQuestGenerator : MonoBehaviour
    {
        public List<QuestData> GeneratedQuests { get; private set; } = new List<QuestData>();

        public Dictionary<EventType, List<string>> QuestTemplates { get; private set; }

        public event Action<QuestData> OnQuestGenerated;

        private void Awake()
        {
            QuestTemplates = new Dictionary<EventType, List<string>>
            {
                {
                    EventType.Kill, new List<string>
                    {
                        "A strange {0} has appeared near the forest.",
                        "The {0} population is growing dangerously."
                    }
                },
                {
                    EventType.Steal, new List<string>
                    {
                        "Someone robbed the merchant district.",
                        "Valuable goods have gone missing."
                    }
                },
                {
                    EventType.Help, new List<string>
                    {
                        "A grateful villager needs your help again.",
                        "Word of your kindness has spread."
                    }
                },
                {
                    EventType.Trade, new List<string>
                    {
                        "A merchant seeks a reliable trading partner.",
                        "Rare goods are available for the right price."
                    }
                },
                {
                    EventType.Rescue, new List<string>
                    {
                        "Someone has been taken captive near {0} territory.",
                        "A missing person was last seen near the {0}."
                    }
                },
                {
                    EventType.Attack, new List<string>
                    {
                        "The village is under threat from a {0}.",
                        "Protect the settlement from the rampaging {0}."
                    }
                },
                {
                    EventType.Talk, new List<string>
                    {
                        "An elder wishes to share wisdom about the {0}.",
                        "Rumors spread — speak to the locals about the {0}."
                    }
                },
                {
                    EventType.Threaten, new List<string>
                    {
                        "A dangerous figure threatens the peace.",
                        "Deal with the one who terrorizes the townspeople."
                    }
                },
                {
                    EventType.Gift, new List<string>
                    {
                        "Deliver a special gift to seal an alliance.",
                        "A precious item must reach its recipient safely."
                    }
                },
                {
                    EventType.None, new List<string>
                    {
                        "Something unusual is happening in the region.",
                        "An adventure awaits the bold."
                    }
                }
            };
        }

        public QuestData GenerateQuest(EventType trigger, string subject = "creature")
        {
            List<string> templates;
            if (!QuestTemplates.TryGetValue(trigger, out templates) || templates.Count == 0)
                templates = QuestTemplates[EventType.None];

            string template = templates[UnityEngine.Random.Range(0, templates.Count)];
            string description = string.Format(template, subject);

            // derive a short title from the trigger
            string title = $"{trigger} Quest: {subject}";

            var quest = new QuestData(title, description, trigger);
            GeneratedQuests.Add(quest);
            OnQuestGenerated?.Invoke(quest);
            return quest;
        }
    }
}
