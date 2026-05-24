# 감정 기반 애니메이션 시스템

감정 설정 한 줄로 이동속도·애니메이터·눈 깜빡임이 자동으로 변경됩니다.

## 개요

```
기존:
  anim.Play("Idle");

NPC Mentality:
  emotion.Set(Emotion.Angry);
  → 자동: 걷기 속도 증가 / 머리 흔들림 / 표정 변화 / 눈 깜빡임 감소
```

## 컴포넌트

`EmotionController` — MonoBehaviour.

| 필드 | 타입 | 설명 |
|------|------|------|
| `Animator` | `Animator` | NPC 애니메이터 |
| `Agent` | `NavMeshAgent` | 이동 속도 제어 (선택) |
| `CurrentEmotion` | `EmotionType` | 현재 감정 (읽기 전용) |

---

## EmotionType 열거형

```csharp
public enum EmotionType
{
    Neutral,    // 기본 상태
    Happy,      // 행복
    Angry,      // 분노
    Sad,        // 슬픔
    Afraid,     // 두려움
    Surprised,  // 놀람
    Disgusted   // 혐오
}
```

---

## API

### Set

```csharp
void Set(EmotionType emotion)
```

감정을 설정하고 효과를 즉시 적용합니다.

```csharp
emotion.Set(EmotionType.Angry);
emotion.Set(EmotionType.Happy);
emotion.Set(EmotionType.Neutral); // 초기화
```

---

## 감정별 효과

| 감정 | NavMesh 속도 | Animator 트리거 | BlinkRate |
|------|-------------|----------------|-----------|
| `Neutral` | ×1.0 (기본) | `"Idle"` | `1.0` |
| `Happy` | ×1.0 | `"Happy"` | `1.0` |
| `Angry` | ×1.5 | `"Angry"` | `0.3` (감소) |
| `Sad` | ×0.7 | `"Sad"` | `0.8` |
| `Afraid` | ×1.3 | `"Afraid"` | `1.5` (증가) |

> **Animator 파라미터**: `BlinkRate` (Float), 각 감정별 Trigger 파라미터가 필요합니다.

---

## Animator 설정

Animator Controller에 다음 파라미터를 추가하세요:

```
Parameters:
  BlinkRate   [Float]    — 눈 깜빡임 속도 Blend Tree에 연결
  Idle        [Trigger]
  Happy       [Trigger]
  Angry       [Trigger]
  Sad         [Trigger]
  Afraid      [Trigger]
```

---

## 예제

```csharp
using NpcMentality;

public class NPCReaction : MonoBehaviour
{
    private NPCMemory _memory;
    private EmotionController _emotion;

    private void Awake()
    {
        _memory  = GetComponent<NPCMemory>();
        _emotion = GetComponent<EmotionController>();
    }

    // 플레이어와 대화 시작 시 호출
    public void OnTalkWithPlayer()
    {
        float attitude = _memory.GetAttitude();

        if (attitude < -8f)
            _emotion.Set(EmotionType.Angry);
        else if (attitude < -2f)
            _emotion.Set(EmotionType.Disgusted);
        else if (attitude > 8f)
            _emotion.Set(EmotionType.Happy);
        else
            _emotion.Set(EmotionType.Neutral);
    }

    // 전투 상황
    public void OnCombatStart() => _emotion.Set(EmotionType.Afraid);
    public void OnCombatEnd()   => _emotion.Set(EmotionType.Neutral);
}
```

---

## 팁

- [NPCMemory](./memory)의 `GetAttitude()` 결과를 기반으로 감정을 자동 결정할 수 있습니다.
- [WorldTimeSystem](./world-time)의 `OnHourChanged` 이벤트와 연결해 밤에는 `Afraid`로 변경할 수 있습니다.
