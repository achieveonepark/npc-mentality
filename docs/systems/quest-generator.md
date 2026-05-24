# AI 퀘스트 생성기

플레이어 행동 패턴을 분석해 맥락에 맞는 퀘스트를 자동 생성합니다.

## 개요

```
플레이어:
  - 숲 자주 감
  - 늑대 많이 잡음

생성:
  "숲 근처에 이상한 늑대가 나타났다."

랜덤 퀘스트보다 훨씬 자연스러움.
```

## 컴포넌트

`AIQuestGenerator` — MonoBehaviour.

---

## API

### GenerateQuest

```csharp
QuestData GenerateQuest(EventType trigger, string subject = "creature")
```

행동 유형과 주제어를 기반으로 퀘스트를 생성합니다.

| 매개변수 | 설명 |
|----------|------|
| `trigger` | 퀘스트 트리거 행동 유형 |
| `subject` | 퀘스트 주제 (예: `"wolf"`, `"merchant"`) |

```csharp
QuestData q = generator.GenerateQuest(EventType.Kill, "wolf");
// → "숲 근처에 이상한 wolf가 나타났다."

QuestData q2 = generator.GenerateQuest(EventType.Steal, "merchant");
// → "누군가 상인 구역을 털었다."
```

### OnQuestGenerated 이벤트

```csharp
event Action<QuestData> OnQuestGenerated
```

```csharp
generator.OnQuestGenerated += quest =>
{
    QuestUI.Show(quest.Title, quest.Description);
    QuestLog.Add(quest);
};
```

---

## QuestData

```csharp
public class QuestData
{
    public string Title;
    public string Description;
    public EventType TriggerEvent;
    public float GeneratedAt;   // Time.time 기준
}
```

---

## 기본 템플릿

| EventType | 생성 예시 |
|-----------|----------|
| `Kill` | "숲 근처에 이상한 {subject}가 나타났다." |
| `Steal` | "상인 구역에서 물건이 사라졌다." |
| `Help` | "도움의 손길이 다시 필요하다." |
| `Rescue` | "위험에 처한 사람들이 있다." |

Inspector에서 `QuestTemplates` 딕셔너리를 수정해 직접 템플릿을 추가할 수 있습니다.

---

## 예제

```csharp
using NpcMentality;

public class PlayerBehaviorTracker : MonoBehaviour
{
    public AIQuestGenerator questGenerator;

    private int _wolfKillCount = 0;

    public void OnWolfKilled()
    {
        _wolfKillCount++;

        // 늑대를 5번 이상 잡으면 퀘스트 생성
        if (_wolfKillCount >= 5)
        {
            questGenerator.GenerateQuest(EventType.Kill, "wolf");
            _wolfKillCount = 0;
        }
    }

    private void Start()
    {
        questGenerator.OnQuestGenerated += quest =>
            Debug.Log($"[New Quest] {quest.Title}");
    }
}
```

---

## 팁

- [NPCMemory](./memory)로 플레이어 행동을 추적하고, 특정 `EventType`이 `N`번 이상 기록되면 퀘스트를 자동 생성하는 패턴이 효과적입니다.
- `GeneratedQuests` 리스트에서 기존 생성된 퀘스트 이력을 확인할 수 있습니다.
