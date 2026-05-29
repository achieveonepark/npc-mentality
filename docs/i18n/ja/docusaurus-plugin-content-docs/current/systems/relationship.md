---
id: relationship
title: 関係伝染システム
sidebar_label: 2. 関係伝染システム
sidebar_position: 2
---

# 関係伝染システム

NPC 間の友人関係を通じて感情が伝播します。

## 概要

```
A がプレイヤーを嫌う
    ↓
A の友人 B も影響を受ける (×0.5)
    ↓
B の友人 C も少し影響を受ける (×0.25)

結果: 商人の村全体が敵対的になる
```

## RelationshipGraph

NPC 間の友人関係を保存する ScriptableObject です。

**作成:** Project パネルで右クリック → Create → NPC Mentality → Relationship Graph

### API

```csharp
graph.AddFriendship("merchant_a", "villager_b");
graph.AddFriendship("villager_b", "blacksmith_c");

List<string> friends = graph.GetFriends("merchant_a");
```

---

## RelationshipContagion

静的ヘルパークラス。態度変化を友人ネットワーク全体に伝播します。

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

| パラメーター | 説明 |
|-------------|------|
| `sourceNpcId` | 態度変化の発生源 NPC ID |
| `attitudeChange` | 伝播する態度変化量 |
| `graph` | 友人関係グラフ |
| `allNpcs` | 全 NPC の `NPCMemory` 辞書 |
| `depth` | 伝播深度（デフォルト 2） |
| `decay` | 段階ごとの減衰率（デフォルト 0.5） |

---

## サンプル

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
