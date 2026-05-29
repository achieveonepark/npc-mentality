---
id: world-time
title: World Time System
sidebar_label: 6. World Time
sidebar_position: 6
---

# World Time System

Automatically manages shop open/close times and NPC schedules based on in-game time flow.

## Overview

```
08:00 → Shop opens
12:00 → NPCs eat lunch
22:00 → Shop closes
02:00 → Thief spawns
```

## Component

`WorldTimeSystem` — MonoBehaviour singleton. Place only one per scene.

| Field | Type | Default | Description |
|-------|------|---------|-------------|
| `TimeScale` | `float` | `1f` | Time speed multiplier |
| `CurrentHour` | `float` | `0f` | Current time (0~24, read-only) |

### Time Calculation

```
TimeScale = 1  → 1 game-minute per real-second  → 1 day = 24 real-minutes
TimeScale = 60 → 1 game-hour per real-second     → 1 day = 24 real-seconds
```

---

## API

### Register

```csharp
void Register(
    string id,
    int openHour,
    int closeHour,
    Action onOpen = null,
    Action onClose = null
)
```

```csharp
WorldTimeSystem.Instance.Register(
    "Shop",
    openHour: 8,
    closeHour: 22,
    onOpen:  () => shopkeeper.Open(),
    onClose: () => shopkeeper.Close()
);
```

### Unregister

```csharp
void Unregister(string id)
```

### IsOpen

```csharp
bool IsOpen(string id)
```

```csharp
if (WorldTimeSystem.Instance.IsOpen("Shop"))
    ShowShopUI();
```

> Schedules spanning midnight are supported. (`openHour: 22, closeHour: 6` → 22:00 to next-day 06:00)

### OnHourChanged Event

```csharp
event Action<float> OnHourChanged
```

```csharp
WorldTimeSystem.Instance.OnHourChanged += hour =>
    Debug.Log($"Current time: {Mathf.FloorToInt(hour):D2}:00");
```

---

## Example

```csharp
using NpcMentality;

public class VillageScheduler : MonoBehaviour
{
    public CrowdNPC[] villagers;
    public GameObject thiefPrefab;

    private void Start()
    {
        var wt = WorldTimeSystem.Instance;

        wt.Register("Day", openHour: 6, closeHour: 22,
            onOpen:  StartDayRoutine,
            onClose: StartNightRoutine);

        wt.Register("Thief", openHour: 2, closeHour: 4,
            onOpen:  () => Instantiate(thiefPrefab, GetAlleyPosition(), Quaternion.identity),
            onClose: () => DespawnThieves());
    }

    private void StartDayRoutine()
    {
        foreach (var v in villagers) v.enabled = true;
    }

    private void StartNightRoutine()
    {
        foreach (var v in villagers) v.enabled = false;
    }
}
```
