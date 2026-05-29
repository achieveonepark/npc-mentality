---
id: crowd-ai
title: 自然群体AI
sidebar_label: 3. 群体AI
sidebar_position: 3
---

# 自然群体AI

NPC 使用 NavMeshAgent 执行自主、自然的群体行为。

## 概述

```
普通 NPC:
  移动到目标点

NPC Mentality:
  - 互相避让
  - 逛商店
  - 环顾四周
  - 突然停下
  - 绕道而行
```

## 组件

`CrowdNPC` — MonoBehaviour。需要 **NavMeshAgent**。

| 字段 | 类型 | 默认值 | 说明 |
|------|------|-------|------|
| `WanderRadius` | `float` | `10f` | 漫游半径（米） |
| `BehaviorChangeCooldown` | `float` | `3f` | 行为切换间隔（秒） |

## CrowdBehaviorType 枚举

```csharp
public enum CrowdBehaviorType
{
    Wander, BrowseShop, LookAround,
    StopAndObserve, AvoidObstacle, Hurt, Idle
}
```

---

## 提示

- 降低 `BehaviorChangeCooldown` 让行为更随机，增大则更有目的性。
- 与 [WorldTimeSystem](./world-time) 联动，夜晚增加 Idle 比例。
- 与 [EmotionController](./emotion) 结合，根据情感改变移动速度。
