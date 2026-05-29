---
id: memory
title: NPC记忆系统
sidebar_label: 1. NPC记忆系统
sidebar_position: 1
---

# NPC记忆系统

NPC 记住玩家的行为并计算累积态度（Attitude）。

## 概述

```
玩家:
- 偷了商人的东西3次
- 救了村民

NPC:
  商人: "你又来了...别动我的东西。"
  村民: "上次谢谢你的帮助。"
```

## 组件

`NPCMemory` — MonoBehaviour。添加到 NPC GameObject。

| 字段 | 类型 | 默认值 | 说明 |
|------|------|-------|------|
| `MaxMemories` | `int` | `20` | 最大记忆数，超出时删除最旧的 |
| `memories` | `List<MemoryEvent>` | — | 记忆列表（可在 Inspector 中查看） |

## API

### Remember

```csharp
void Remember(EventType type, float weight = 1f, string context = "")
```

| 参数 | 说明 |
|------|------|
| `type` | 行为类型（`EventType` 枚举） |
| `weight` | 态度影响。正数 = 友好，负数 = 敌对 |
| `context` | 可选描述字符串 |

```csharp
npc.Remember(EventType.Steal,  weight: -3f);
npc.Remember(EventType.Help,   weight:  5f);
npc.Remember(EventType.Attack, weight: -10f);
npc.Remember(EventType.Gift,   weight:  3f);
```

### GetAttitude

```csharp
float GetAttitude()
```

返回所有记忆的 weight 总和。

```csharp
float attitude = npc.GetAttitude();
// attitude > 0  → 友好
// attitude == 0 → 中立
// attitude < 0  → 敌对
```

### HasMemory / ForgetOldest

```csharp
bool HasMemory(EventType type)
void ForgetOldest()
```

---

## 示例

```csharp
using NpcMentality;

public class ShopKeeper : MonoBehaviour
{
    private NPCMemory _memory;

    private void Awake() => _memory = GetComponent<NPCMemory>();

    public string GetGreeting()
    {
        return _memory.GetAttitude() switch
        {
            < -5f => "滚出去。",
            < 0f  => "你要买什么？",
            < 5f  => "欢迎光临。",
            _     => "希望多来几个像你这样的顾客！"
        };
    }
}
```
