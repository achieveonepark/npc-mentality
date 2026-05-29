---
id: relationship
title: 关系传染系统
sidebar_label: 2. 关系传染系统
sidebar_position: 2
---

# 关系传染系统

情感通过 NPC 之间的朋友关系传播。

## 概述

```
A 讨厌玩家
    ↓
A 的朋友 B 也受影响 (×0.5)
    ↓
B 的朋友 C 略微受影响 (×0.25)

结果: 整个商人村都讨厌你
```

## RelationshipGraph

存储 NPC 间朋友关系的 ScriptableObject。

**创建：** 在 Project 面板右键 → Create → NPC Mentality → Relationship Graph

```csharp
graph.AddFriendship("merchant_a", "villager_b");
graph.AddFriendship("villager_b", "blacksmith_c");

List<string> friends = graph.GetFriends("merchant_a");
```

---

## RelationshipContagion

静态辅助类，将态度变化传播到朋友网络。

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

---

## 示例

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
