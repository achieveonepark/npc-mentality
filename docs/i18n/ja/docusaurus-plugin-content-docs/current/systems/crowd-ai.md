---
id: crowd-ai
title: 自然な群衆AI
sidebar_label: 3. 群衆AI
sidebar_position: 3
---

# 自然な群衆AI

NavMeshAgent を利用して NPC が自律的で自然な群衆行動を行います。

## 概要

```
一般的な NPC:
  目標地点へ移動

NPC Mentality:
  - お互いを避ける
  - お店を見て回る
  - 周囲の人を眺める
  - 突然立ち止まる
  - 道を迂回する
```

## コンポーネント

`CrowdNPC` — MonoBehaviour。**NavMeshAgent** が必要です。

| フィールド | 型 | デフォルト | 説明 |
|------------|-----|-----------|------|
| `WanderRadius` | `float` | `10f` | 徘徊半径（メートル） |
| `BehaviorChangeCooldown` | `float` | `3f` | 行動切替間隔（秒） |
| `CurrentBehavior` | `CrowdBehaviorType` | — | 現在の行動（読み取り専用） |

---

## CrowdBehaviorType 列挙型

```csharp
public enum CrowdBehaviorType
{
    Wander,          // 半径内をランダム移動
    BrowseShop,      // 周辺の店へゆっくり移動
    LookAround,      // 止まって周囲を見渡す (1-3秒)
    StopAndObserve,  // 完全停止 (1-2秒)
    AvoidObstacle,   // 障害物を迂回
    Hurt,            // ぶつかったように少しよろめく
    Idle             // その場で待機
}
```

---

## ヒント

- `BehaviorChangeCooldown` を低くするとより落ち着きなく、高くするとより目的を持って見えます。
- [WorldTimeSystem](./world-time) と連携して夜は Idle の割合を増やせます。
- [EmotionController](./emotion) と組み合わせて感情に応じた移動速度変化も可能です。
