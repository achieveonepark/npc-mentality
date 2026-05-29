---
id: relationship
title: 관계 전염 시스템
sidebar_label: 2. 관계 전염 시스템
sidebar_position: 2
---

# 관계 전염 시스템

NPC 간 친구 관계를 통해 감정이 전파됩니다.

## 개요

```
A가 플레이어 싫어함
    ↓
A와 친한 B도 영향 (×0.5)
    ↓
B와 친한 C도 약간 영향 (×0.25)

결과: 상인 마을 전체가 날 싫어함
```

## RelationshipGraph

ScriptableObject로 NPC 간 친구 관계를 저장합니다.

**생성:** Project 창에서 우클릭 → Create → NPC Mentality → Relationship Graph

### API

```csharp
// 친구 관계 등록
graph.AddFriendship("merchant_a", "villager_b");
graph.AddFriendship("villager_b", "blacksmith_c");

// 특정 NPC의 친구 목록 조회
List<string> friends = graph.GetFriends("merchant_a");
```

---

## RelationshipContagion

정적 헬퍼 클래스. 태도 변화를 친구 네트워크로 전파합니다.

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

| 매개변수 | 설명 |
|----------|------|
| `sourceNpcId` | 감정 변화의 출처 NPC ID |
| `attitudeChange` | 전파할 태도 변화량 |
| `graph` | 친구 관계 그래프 |
| `allNpcs` | 전체 NPC의 `NPCMemory` 딕셔너리 |
| `depth` | 전파 깊이 (기본 2단계) |
| `decay` | 단계별 감쇠율 (기본 0.5 = 50%) |

### 전파 계산

```
depth 1 (직접 친구): attitudeChange × decay
depth 2 (친구의 친구): attitudeChange × decay²
```

예시: `attitudeChange = -10, decay = 0.5`
- 직접 친구: `-5`
- 친구의 친구: `-2.5`

---

## 예제

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

## 팁

- `decay`를 `0.3` 이하로 줄이면 소문이 빨리 희미해집니다.
- `depth`를 `3` 이상으로 늘리면 마을 전체가 영향받습니다.
- [루머 시스템](./rumor)과 함께 쓰면 소문 내용과 감정이 동시에 퍼집니다.
