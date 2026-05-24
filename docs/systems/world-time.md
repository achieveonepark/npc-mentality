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
| `TimeScale` | `float` | `1f` | 시간 배속 (1 = 실제 1초당 1게임분 경과) |
| `CurrentHour` | `float` | `0f` | 현재 시간 (0~24, 읽기 전용) |

### 시간 계산

```
1 실제 초 × TimeScale → CurrentHour += TimeScale / 60 시간

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

시간 스케줄을 등록합니다.

```csharp
WorldTimeSystem.Instance.Register(
    "Shop",
    openHour: 8,
    closeHour: 22,
    onOpen:  () => shopkeeper.Open(),
    onClose: () => shopkeeper.Close()
);

WorldTimeSystem.Instance.Register("Thief", openHour: 2, closeHour: 4,
    onOpen: () => SpawnThief());
```

### Unregister

```csharp
void Unregister(string id)
```

스케줄을 제거합니다.

```csharp
WorldTimeSystem.Instance.Unregister("Shop");
```

### IsOpen

```csharp
bool IsOpen(string id)
```

현재 시간에 해당 스케줄이 열려있는지 확인합니다.

```csharp
if (WorldTimeSystem.Instance.IsOpen("Shop"))
    ShowShopUI();
```

> 자정을 넘는 스케줄도 지원합니다. (`openHour: 22, closeHour: 6` → 22시~익일 6시)

### OnHourChanged 이벤트

```csharp
event Action<float> OnHourChanged
```

매 정각(게임 내 1시간)마다 발생합니다.

```csharp
WorldTimeSystem.Instance.OnHourChanged += hour =>
{
    Debug.Log($"현재 시간: {Mathf.FloorToInt(hour):D2}:00");
};
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

        wt.Register("Day",   openHour: 6,  closeHour: 22,
            onOpen:  StartDayRoutine,
            onClose: StartNightRoutine);

        wt.Register("Lunch", openHour: 12, closeHour: 13,
            onOpen:  () => foreach (var v in villagers) v.GoToTavern());

        wt.Register("Thief", openHour: 2,  closeHour: 4,
            onOpen:  () => Instantiate(thiefPrefab, GetAlleyPosition(), Quaternion.identity),
            onClose: () => DespawnThieves());

        wt.OnHourChanged += UpdateClockUI;
    }

    private void StartDayRoutine()
    {
        foreach (var v in villagers) v.enabled = true;
    }

    private void StartNightRoutine()
    {
        foreach (var v in villagers) v.enabled = false;
    }

    private void UpdateClockUI(float hour)
    {
        int h = Mathf.FloorToInt(hour);
        ClockUI.SetTime(h, 0);
    }
}
```

---

## 팁

- `TimeScale`을 런타임에 변경해 게임 내 시간 흐름 속도를 조절할 수 있습니다.
- [CrowdNPC](./crowd-ai)의 `enabled`를 낮과 밤에 따라 토글하면 자연스러운 인구 밀도를 표현할 수 있습니다.
