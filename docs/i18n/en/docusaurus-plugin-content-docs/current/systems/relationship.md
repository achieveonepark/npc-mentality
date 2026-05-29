---
id: relationship
title: Relationship Contagion System
sidebar_label: 2. Relationship Contagion
sidebar_position: 2
---

# Relationship Contagion System

Emotions spread through NPC friendship networks.

## Overview

```
A dislikes the player
    ↓
A's friend B is affected (×0.5)
    ↓
B's friend C is slightly affected (×0.25)

Result: the entire merchant village dislikes you
```

## RelationshipGraph

A ScriptableObject that stores friendship relationships between NPCs.

**Create:** Right-click in Project panel → Create → NPC Mentality → Relationship Graph

### API

```csharp
// Register friendship
graph.AddFriendship("merchant_a", "villager_b");
graph.AddFriendship("villager_b", "blacksmith_c");

// Get an NPC's friends list
List<string> friends = graph.GetFriends("merchant_a");
```

---

## RelationshipContagion

Static helper class. Propagates attitude changes through the friend network.

### Propagate

```csharp
static void Propagate(
    string sourceNpcId,
    float attitudeChange,
    RelationshipGraph graph,
    Dictionary<string, NPCMemory> allNpcs,
    int depth = 2,
    float decay = 0.5f
)
```

| Parameter | Description |
|-----------|-------------|
| `sourceNpcId` | Source NPC ID for the attitude change |
| `attitudeChange` | Attitude change amount to propagate |
| `graph` | Friendship graph |
| `allNpcs` | Dictionary of all NPCs' `NPCMemory` |
| `depth` | Propagation depth (default 2 levels) |
| `decay` | Decay rate per level (default 0.5 = 50%) |

### Propagation Calculation

```
depth 1 (direct friends): attitudeChange × decay
depth 2 (friends of friends): attitudeChange × decay²
```

Example: `attitudeChange = -10, decay = 0.5`
- Direct friends: `-5`
- Friends of friends: `-2.5`

---

## Example

```csharp
using NpcMentality;
using System.Collections.Generic;

public class VillageManager : MonoBehaviour
{
    public RelationshipGraph graph;
    private Dictionary<string, NPCMemory> _npcs = new();

    private void Start()
    {
        graph.AddFriendship("merchant", "innkeeper");
        graph.AddFriendship("innkeeper", "blacksmith");

        foreach (var npc in FindObjectsOfType<NPCMemory>())
            _npcs[npc.gameObject.name] = npc;
    }

    public void OnPlayerAttacksMerchant()
    {
        _npcs["merchant"].Remember(EventType.Attack, weight: -10f);

        RelationshipContagion.Propagate(
            sourceNpcId: "merchant",
            attitudeChange: -10f,
            graph: graph,
            allNpcs: _npcs,
            depth: 2,
            decay: 0.5f
        );
        // merchant: -10, innkeeper: -5, blacksmith: -2.5
    }
}
```

---

## Tips

- Lower `decay` to `0.3` or less for rumors that fade quickly.
- Increase `depth` to `3` or more to affect the entire village.
- Combine with the [Rumor System](./rumor) to spread both rumor content and emotion simultaneously.
