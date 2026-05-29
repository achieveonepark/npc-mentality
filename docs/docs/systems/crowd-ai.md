---
id: crowd-ai
title: 자연스러운 군중 AI
sidebar_label: 3. 군중 AI
sidebar_position: 3
---

# 자연스러운 군중 AI

NavMeshAgent 기반으로 NPC가 자율적이고 자연스러운 군중 행동을 수행합니다.

## 개요

```
보통 NPC:
  목표지점 이동

NPC Mentality:
  - 서로 피해감
  - 가게 구경
  - 주변 사람 쳐다봄
  - 갑자기 멈춤
  - 길 막히면 우회
```

## 컴포넌트

`CrowdNPC` — MonoBehaviour. **NavMeshAgent**가 함께 있어야 합니다.

| 필드 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `WanderRadius` | `float` | `10f` | 배회 반경 (미터) |
| `BehaviorChangeCooldown` | `float` | `3f` | 행동 전환 주기 (초) |
| `CurrentBehavior` | `CrowdBehaviorType` | — | 현재 행동 (읽기 전용) |

---

## CrowdBehaviorType 열거형

```csharp
public enum CrowdBehaviorType
{
    Wander,          // 반경 내 무작위 이동
    BrowseShop,      // 주변 상점으로 천천히 이동
    LookAround,      // 멈추고 주변 둘러보기 (1-3초)
    StopAndObserve,  // 완전 정지 (1-2초)
    AvoidObstacle,   // 장애물 우회
    Hurt,            // 부딪힌 척 살짝 비틀거림
    Idle             // 제자리
}
```

## 행동 상세

| 행동 | 이동 속도 | 설명 |
|------|----------|------|
| `Wander` | 기본 | 반경 내 무작위 목적지로 이동 |
| `BrowseShop` | ×0.5 (천천히) | "Shop" 태그 오브젝트 근처로 이동 |
| `LookAround` | 0 (정지) | 1~3초간 천천히 회전 |
| `StopAndObserve` | 0 (정지) | 1~2초간 완전 정지 |
| `AvoidObstacle` | 기본 | 약간 오프셋된 새 목적지로 이동 |
| `Idle` | 0 (정지) | 아무것도 하지 않음 |

---

## 예제

### 기본 사용

```csharp
// 1. GameObject에 NavMeshAgent 추가
// 2. CrowdNPC 컴포넌트 추가
// 3. Inspector에서 설정 후 Play — 자동 동작
```

### 상점 배치

`BrowseShop` 행동이 작동하려면 상점 오브젝트에 `Shop` 태그를 추가합니다.

```csharp
// 에디터에서: 상점 GameObject → Tag → "Shop"
```

---

## 팁

- `BehaviorChangeCooldown`을 낮추면 더 산만하게, 높이면 더 목적있게 보입니다.
- [WorldTimeSystem](./world-time)과 연동해 밤에는 Idle 비중을 높일 수 있습니다.
- [EmotionController](./emotion)와 함께 쓰면 감정에 따라 이동 속도도 변합니다.
