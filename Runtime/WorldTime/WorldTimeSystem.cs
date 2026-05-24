using System;
using System.Collections.Generic;
using UnityEngine;

namespace NpcMentality
{
    [AddComponentMenu("NPC Mentality/World Time System")]
    public class WorldTimeSystem : MonoBehaviour
    {
        public static WorldTimeSystem Instance { get; private set; }

        // 1 real second * TimeScale advances CurrentHour by TimeScale/60 hours
        // Default: TimeScale=1 means 1 game minute per real second (full day = 24 real minutes)
        public float TimeScale = 1f;

        public float CurrentHour { get; private set; } = 0f;

        public event Action<float> OnHourChanged;

        private readonly Dictionary<string, TimeSchedule> _schedules = new Dictionary<string, TimeSchedule>();
        private int _lastWholeHour = -1;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            float previousHour = CurrentHour;
            CurrentHour += Time.deltaTime * TimeScale / 60f;

            if (CurrentHour >= 24f)
                CurrentHour -= 24f;

            int currentWholeHour = Mathf.FloorToInt(CurrentHour);
            if (currentWholeHour != _lastWholeHour)
            {
                _lastWholeHour = currentWholeHour;
                OnHourChanged?.Invoke(CurrentHour);
                FireScheduleCallbacks(currentWholeHour);
            }
        }

        private void FireScheduleCallbacks(int hour)
        {
            foreach (var schedule in _schedules.Values)
            {
                if (schedule.OpenHour == hour)
                    schedule.OnOpen?.Invoke();
                else if (schedule.CloseHour == hour)
                    schedule.OnClose?.Invoke();
            }
        }

        public void Register(string id, int openHour, int closeHour, Action onOpen = null, Action onClose = null)
        {
            _schedules[id] = new TimeSchedule
            {
                Id = id,
                OpenHour = openHour,
                CloseHour = closeHour,
                OnOpen = onOpen,
                OnClose = onClose
            };
        }

        public void Unregister(string id)
        {
            _schedules.Remove(id);
        }

        public bool IsOpen(string id)
        {
            if (!_schedules.TryGetValue(id, out var schedule))
                return false;

            int hour = Mathf.FloorToInt(CurrentHour);

            if (schedule.OpenHour <= schedule.CloseHour)
                return hour >= schedule.OpenHour && hour < schedule.CloseHour;

            // wraps midnight (e.g. open 22, close 6)
            return hour >= schedule.OpenHour || hour < schedule.CloseHour;
        }
    }
}
