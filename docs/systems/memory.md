# NPC 기억 시스템

NPC가 플레이어의 행동을 기억하고 누적 태도(Attitude)를 계산합니다.

## 개요

```
플레이어:
- 상인 물건 3번 훔침
- 마을 주민 구해줌

NPC:
  상인: "또 왔네... 내 물건은 건들지 마."
  주민: "지난번엔 고마웠어."
```

## 컴포넌트

`NPCMemory` — MonoBehaviour. NPC GameObject에 추가합니다.

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `MaxMemories` | `int` | `20` | 최대 기억 수. 초과 시 가장 오래된 것 삭제 |
| `memories` | `List<MemoryEvent>` | — | 저장된 기억 목록 (Inspector에서 확인 가능) |

## API

### Remember

```csharp
void Remember(EventType type, float weight = 1f, string context = "")
```

플레이어 행동을 기억합니다.

| 매개변수 | 설명 |
|----------|------|
| `type` | 행동 유형 (`EventType` 열거형) |
| `weight` | 태도 영향. 양수 = 우호적, 음수 = 적대적 |
| `context` | 선택적 설명 문자열 |

```csharp
npc.Remember(EventType.Steal,  weight: -3f);  // 훔침
npc.Remember(EventType.Help,   weight:  5f);  // 도움
npc.Remember(EventType.Attack, weight: -10f); // 공격
npc.Remember(EventType.Gift,   weight:  3f);  // 선물
```

### GetAttitude

```csharp
float GetAttitude()
```

모든 기억의 weight 합계를 반환합니다.

```csharp
float attitude = npc.GetAttitude();

// attitude > 0  → 우호적
// attitude == 0 → 중립
// attitude < 0  → 적대적
```

### HasMemory

```csharp
bool HasMemory(EventType type)
```

특정 유형의 기억이 있는지 확인합니다.

```csharp
if (npc.HasMemory(EventType.Steal))
    dialogue.Say("또 왔네...");
```

### ForgetOldest

```csharp
void ForgetOldest()
```

가장 오래된 기억을 수동으로 삭제합니다. `MaxMemories` 초과 시 자동 호출됩니다.

---

## EventType 열거형

```csharp
public enum EventType
{
    None,
    Steal,    // 훔침
    Help,     // 도움
    Attack,   // 공격
    Trade,    // 거래
    Talk,     // 대화
    Threaten, // 협박
    Gift,     // 선물
    Kill,     // 처치
    Rescue    // 구조
}
```

## MemoryEvent 구조

```csharp
public class MemoryEvent
{
    public EventType Type;
    public float Timestamp;  // Time.time 기준
    public float Weight;
    public string Context;
}
```

---

## 예제

```csharp
using NpcMentality;

public class ShopKeeper : MonoBehaviour
{
    private NPCMemory _memory;

    private void Awake() => _memory = GetComponent<NPCMemory>();

    public void OnPlayerSteals()
    {
        _memory.Remember(EventType.Steal, weight: -3f, context: "귀한 약초");

        float attitude = _memory.GetAttitude();
        if (attitude < -6f)
        {
            // 경비 호출
            GuardSystem.Alert(transform.position);
        }
    }

    public string GetGreeting()
    {
        float attitude = _memory.GetAttitude();
        return attitude switch
        {
            < -5f => "꺼져.",
            < 0f  => "뭐가 필요해?",
            < 5f  => "어서오세요.",
            _     => "당신 같은 손님이 더 왔으면 해요!"
        };
    }
}
```
