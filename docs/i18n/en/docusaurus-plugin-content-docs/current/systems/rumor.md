---
id: rumor
title: Rumor System
sidebar_label: 7. Rumor System
sidebar_position: 7
---

# Rumor System

Information mutates as it travels between NPCs.

## Overview

```
Player kills the boss
    ↓
NPC1: "I heard someone slew a dragon?"
    ↓
NPC2: "Actually, I heard it was a wizard."
    ↓
NPC3: "The demon king is dead, they say."
```

## Component

`RumorSystem` — MonoBehaviour.

| Field | Type | Default | Description |
|-------|------|---------|-------------|
| `MutationChance` | `float` | `0.3f` | Probability of mutation (0~1) |
| `MutationSuffixes` | `List<string>` | 4 defaults | Sentences appended on mutation |
| `ActiveRumors` | `List<RumorData>` | — | Currently active rumors |

---

## API

### SpreadRumor

```csharp
void SpreadRumor(string fact, string sourceNpcId)
```

```csharp
rumorSystem.SpreadRumor("Someone slew a dragon", sourceNpcId: "player");
```

### PassRumor

```csharp
RumorData PassRumor(RumorData rumor, string receivingNpcId)
```

Content mutates with `MutationChance` probability.

### Events

```csharp
event Action<RumorData> OnRumorSpread          // When rumor first created
event Action<RumorData, string> OnRumorPassed  // Each time rumor is passed
```

---

## RumorData

```csharp
public class RumorData
{
    public string OriginalFact;
    public string CurrentVersion;
    public int HopCount;
    public float SpreadAt;
    public string SourceNpcId;
}
```

---

## Example

```csharp
using NpcMentality;

public class RumorDemo : MonoBehaviour
{
    public RumorSystem rumorSystem;
    public string[] npcIds = { "blacksmith", "innkeeper", "farmer", "guard" };

    private void Start()
    {
        rumorSystem.OnRumorPassed += (r, npcId) =>
            Debug.Log($"[{npcId}] hop {r.HopCount}: \"{r.CurrentVersion}\"");
    }

    public void OnPlayerKillsDragon()
    {
        rumorSystem.SpreadRumor("Someone slew a dragon", "player");
        StartCoroutine(SpreadToVillage());
    }

    private System.Collections.IEnumerator SpreadToVillage()
    {
        var rumor = rumorSystem.ActiveRumors[^1];
        foreach (string npcId in npcIds)
        {
            yield return new WaitForSeconds(2f);
            rumorSystem.PassRumor(rumor, npcId);
        }
    }
}
```

---

## Tips

- `MutationChance = 0f` — facts spread without any mutation.
- `MutationChance = 1f` — every pass guarantees a mutation.
- Combine with [RelationshipContagion](./relationship) to spread both content and emotion through the village simultaneously.
- Use `HopCount` to implement "this rumor is unreliable" UI.
