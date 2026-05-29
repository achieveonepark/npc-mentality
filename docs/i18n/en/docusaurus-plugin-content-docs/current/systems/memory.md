---
id: memory
title: NPC Memory System
sidebar_label: 1. NPC Memory
sidebar_position: 1
---

# NPC Memory System

NPCs remember player actions and calculate cumulative attitude.

## Overview

```
Player:
- Stole from merchant 3 times
- Saved a villager

NPCs:
  Merchant: "You again... keep your hands off my stuff."
  Villager: "Thanks for saving me last time."
```

## Component

`NPCMemory` — MonoBehaviour. Add to the NPC GameObject.

| Field | Type | Default | Description |
|-------|------|---------|-------------|
| `MaxMemories` | `int` | `20` | Max memory count. Oldest is deleted when exceeded. |
| `memories` | `List<MemoryEvent>` | — | Stored memory list (viewable in Inspector) |

## API

### Remember

```csharp
void Remember(EventType type, float weight = 1f, string context = "")
```

Records a player action.

| Parameter | Description |
|-----------|-------------|
| `type` | Action type (`EventType` enum) |
| `weight` | Attitude impact. Positive = friendly, Negative = hostile |
| `context` | Optional description string |

```csharp
npc.Remember(EventType.Steal,  weight: -3f);
npc.Remember(EventType.Help,   weight:  5f);
npc.Remember(EventType.Attack, weight: -10f);
npc.Remember(EventType.Gift,   weight:  3f);
```

### GetAttitude

```csharp
float GetAttitude()
```

Returns the sum of all memory weights.

```csharp
float attitude = npc.GetAttitude();

// attitude > 0  → friendly
// attitude == 0 → neutral
// attitude < 0  → hostile
```

### HasMemory

```csharp
bool HasMemory(EventType type)
```

Checks if a specific memory type exists.

```csharp
if (npc.HasMemory(EventType.Steal))
    dialogue.Say("You again...");
```

### ForgetOldest

```csharp
void ForgetOldest()
```

Manually removes the oldest memory. Called automatically when `MaxMemories` is exceeded.

---

## EventType Enum

```csharp
public enum EventType
{
    None,
    Steal,
    Help,
    Attack,
    Trade,
    Talk,
    Threaten,
    Gift,
    Kill,
    Rescue
}
```

## MemoryEvent Structure

```csharp
public class MemoryEvent
{
    public EventType Type;
    public float Timestamp;  // Based on Time.time
    public float Weight;
    public string Context;
}
```

---

## Example

```csharp
using NpcMentality;

public class ShopKeeper : MonoBehaviour
{
    private NPCMemory _memory;

    private void Awake() => _memory = GetComponent<NPCMemory>();

    public void OnPlayerSteals()
    {
        _memory.Remember(EventType.Steal, weight: -3f, context: "rare herb");

        float attitude = _memory.GetAttitude();
        if (attitude < -6f)
        {
            GuardSystem.Alert(transform.position);
        }
    }

    public string GetGreeting()
    {
        float attitude = _memory.GetAttitude();
        return attitude switch
        {
            < -5f => "Get out.",
            < 0f  => "What do you want?",
            < 5f  => "Welcome.",
            _     => "I wish I had more customers like you!"
        };
    }
}
```
