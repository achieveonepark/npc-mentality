---
id: world-time
title: 시간 흐름 세계 시스템
sidebar_label: 6. 시간 흐름 세계
sidebar_position: 6
---

# 시간 흐름 세계 시스템

게임 내 시간 흐름에 따라 상점 오픈/닫힘, NPC 스케줄을 자동으로 관리합니다.

## 개요

```
08:00 → 상점 오픈
12:00 → NPC 식사
22:00 → 상점 닫힘
02:00 → 도둑 등장
```

## 컴포넌트

`WorldTimeSystem` — MonoBehaviour 싱글턴. 씬에 하나만 배치합니다.

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `TimeScale` | `float` | `1f` | 시간 배속 |
| `CurrentHour` | `float` | `0f` | 현재 시간 (0~24, 읽기 전용) |

### 시간 계산

```
TimeScale = 1  → 1초당 1분 경과  → 하루 = 24분
TimeScale = 60 → 1초당 1시간 경과 → 하루 = 24초
```

---

## API

### Register

```csharp
void Register(
    string id,
    int openHour,
    int closeHour,
    Action onOpen = null,
    Action onClose = null
)
```

```csharp
WorldTimeSystem.Instance.Register(
    "Shop",
    openHour: 8,
    closeHour: 22,
    onOpen:  () => shopkeeper.Open(),
    onClose: () => shopkeeper.Close()
);
```

### IsOpen

```csharp
bool IsOpen(string id)
```

```csharp
if (WorldTimeSystem.Instance.IsOpen("Shop"))
    ShowShopUI();
```

### OnHourChanged 이벤트

```csharp
event Action<float> OnHourChanged
```

```csharp
WorldTimeSystem.Instance.OnHourChanged += hour =>
    Debug.Log($"현재 시간: {Mathf.FloorToInt(hour):D2}:00");
```

---

## 예제

```csharp
using NpcMentality;

public class VillageScheduler : MonoBehaviour
{
    public CrowdNPC[] villagers;
    public GameObject thiefPrefab;

    private void Start()
    {
        var wt = WorldTimeSystem.Instance;

        wt.Register("Day", openHour: 6, closeHour: 22,
            onOpen:  StartDayRoutine,
            onClose: StartNightRoutine);

        wt.Register("Thief", openHour: 2, closeHour: 4,
            onOpen:  () => Instantiate(thiefPrefab, GetAlleyPosition(), Quaternion.identity),
            onClose: () => DespawnThieves());
    }

    private void StartDayRoutine()
    {
        foreach (var v in villagers) v.enabled = true;
    }

    private void StartNightRoutine()
    {
        foreach (var v in villagers) v.enabled = false;
    }
}
```
