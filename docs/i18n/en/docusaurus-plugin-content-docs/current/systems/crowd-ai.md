---
id: crowd-ai
title: Natural Crowd AI
sidebar_label: 3. Crowd AI
sidebar_position: 3
---

# Natural Crowd AI

NPCs perform autonomous, natural crowd behaviors using NavMeshAgent.

## Overview

```
Typical NPC:
  Move to target point

NPC Mentality:
  - Avoid each other
  - Browse shops
  - Look at nearby people
  - Stop suddenly
  - Detour around obstacles
```

## Component

`CrowdNPC` — MonoBehaviour. Requires a **NavMeshAgent** on the same GameObject.

| Field | Type | Default | Description |
|-------|------|---------|-------------|
| `WanderRadius` | `float` | `10f` | Wander radius (meters) |
| `BehaviorChangeCooldown` | `float` | `3f` | Behavior switch interval (seconds) |
| `CurrentBehavior` | `CrowdBehaviorType` | — | Current behavior (read-only) |

---

## CrowdBehaviorType Enum

```csharp
public enum CrowdBehaviorType
{
    Wander,          // Random movement within radius
    BrowseShop,      // Slowly move toward nearby shop
    LookAround,      // Stop and look around (1-3s)
    StopAndObserve,  // Complete stop (1-2s)
    AvoidObstacle,   // Detour around obstacle
    Hurt,            // Stumble slightly as if bumped
    Idle             // Stay in place
}
```

## Behavior Details

| Behavior | Speed | Description |
|----------|-------|-------------|
| `Wander` | Normal | Move to random destination within radius |
| `BrowseShop` | ×0.5 (slow) | Move near object tagged "Shop" |
| `LookAround` | 0 (stop) | Rotate slowly for 1~3 seconds |
| `StopAndObserve` | 0 (stop) | Complete stop for 1~2 seconds |
| `AvoidObstacle` | Normal | Move to slightly offset new destination |
| `Idle` | 0 (stop) | Do nothing |

---

## Example

### Basic Usage

```csharp
// 1. Add NavMeshAgent to GameObject
// 2. Add CrowdNPC component
// 3. Configure in Inspector, then press Play — works automatically
```

### Shop Setup

For `BrowseShop` to work, add the `Shop` tag to shop objects.

```csharp
// In Editor: Shop GameObject → Tag → "Shop"
```

---

## Tips

- Lower `BehaviorChangeCooldown` for more erratic behavior, higher for more purposeful.
- Connect with [WorldTimeSystem](./world-time) to increase Idle frequency at night.
- Combine with [EmotionController](./emotion) to change movement speed based on emotion.
