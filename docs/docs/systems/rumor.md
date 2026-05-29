---
id: rumor
title: 루머 시스템
sidebar_label: 7. 루머 시스템
sidebar_position: 7
---

# 루머 시스템

정보가 NPC 사이를 돌면서 내용이 점점 변형됩니다.

## 개요

```
플레이어가 보스 잡음
    ↓
NPC1: "누가 용을 잡았다던데?"
    ↓
NPC2: "아니던데? 마법사래"
    ↓
NPC3: "마왕 죽었대"
```

## 컴포넌트

`RumorSystem` — MonoBehaviour.

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `MutationChance` | `float` | `0.3f` | 루머가 변형될 확률 (0~1) |
| `MutationSuffixes` | `List<string>` | 기본 4개 | 변형 시 추가될 문장 목록 |
| `ActiveRumors` | `List<RumorData>` | — | 현재 진행 중인 루머 목록 |

---

## API

### SpreadRumor

```csharp
void SpreadRumor(string fact, string sourceNpcId)
```

```csharp
rumorSystem.SpreadRumor("플레이어가 용을 잡았다", sourceNpcId: "player");
```

### PassRumor

```csharp
RumorData PassRumor(RumorData rumor, string receivingNpcId)
```

`MutationChance` 확률로 내용이 변형됩니다.

### 이벤트

```csharp
event Action<RumorData> OnRumorSpread          // 루머 최초 생성 시
event Action<RumorData, string> OnRumorPassed  // 다음 NPC에게 전달될 때마다
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

## 예제

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
        rumorSystem.SpreadRumor("누군가 용을 잡았다", "player");
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

## 팁

- `MutationChance = 0f` — 변형 없이 사실만 전파됩니다.
- `MutationChance = 1f` — 매 전달마다 반드시 변형됩니다.
- [RelationshipContagion](./relationship)과 함께 쓰면 루머 내용과 감정이 동시에 퍼집니다.
