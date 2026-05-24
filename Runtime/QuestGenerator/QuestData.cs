using System;
using UnityEngine;

namespace NpcMentality
{
    [Serializable]
    public class QuestData
    {
        public string Title;
        public string Description;
        public EventType TriggerEvent;
        public float GeneratedAt;

        public QuestData(string title, string description, EventType trigger)
        {
            Title = title;
            Description = description;
            TriggerEvent = trigger;
            GeneratedAt = Time.time;
        }
    }
}
