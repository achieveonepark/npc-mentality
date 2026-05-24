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

새 루머를 생성하고 전파를 시작합니다.

```csharp
rumorSystem.SpreadRumor("플레이어가 용을 잡았다", sourceNpcId: "player");
```

### PassRumor

```csharp
RumorData PassRumor(RumorData rumor, string receivingNpcId)
```

루머를 다음 NPC에게 전달합니다. `MutationChance` 확률로 내용이 변형됩니다.

```csharp
var rumor = rumorSystem.ActiveRumors[0];

rumorSystem.PassRumor(rumor, "npc_01");
rumorSystem.PassRumor(rumor, "npc_02");
rumorSystem.PassRumor(rumor, "npc_03");
```

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
    public string OriginalFact;   // 원본 사실
    public string CurrentVersion; // 현재 변형된 버전
    public int HopCount;          // 거친 NPC 수
    public float SpreadAt;        // 최초 생성 시각 (Time.time)
    public string SourceNpcId;    // 최초 출처 NPC ID
}
```

---

## 기본 변형 문장

```csharp
"I heard it was actually a wizard."
"Someone said it was the demon king."
"But maybe it wasn't real?"
"The details got mixed up somehow."
```

Inspector에서 `MutationSuffixes`를 수정해 게임 세계관에 맞는 문장으로 교체하세요.

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
        rumorSystem.OnRumorSpread += r =>
            Debug.Log($"[루머 발생] \"{r.CurrentVersion}\" (출처: {r.SourceNpcId})");

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
            yield return new WaitForSeconds(2f); // NPC 간 전달 딜레이
            rumorSystem.PassRumor(rumor, npcId);
        }
    }
}
```

### 출력 예시

```
[루머 발생] "누군가 용을 잡았다" (출처: player)
[blacksmith] hop 1: "누군가 용을 잡았다"
[innkeeper]  hop 2: "누군가 용을 잡았다 I heard it was actually a wizard."
[farmer]     hop 3: "누군가 용을 잡았다 I heard it was actually a wizard. Someone said it was the demon king."
[guard]      hop 4: "... (원형을 알아볼 수 없음)"
```

---

## 팁

- `MutationChance = 0f`로 설정하면 변형 없이 사실만 전파됩니다.
- `MutationChance = 1f`로 설정하면 매 전달마다 반드시 변형됩니다.
- [RelationshipContagion](./relationship)과 함께 쓰면 루머 내용과 감정이 동시에 마을에 퍼집니다.
- `HopCount`를 기반으로 "이 루머는 신뢰할 수 없다" UI를 구현할 수 있습니다.
