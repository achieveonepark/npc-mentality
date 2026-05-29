---
id: emotion
title: 基于情感的动画系统
sidebar_label: 4. 情感动画
sidebar_position: 4
---

# 基于情感的动画系统

一行代码自动设置移动速度、动画器触发器和眨眼频率。

## 概述

```
传统:
  anim.Play("Idle");

NPC Mentality:
  emotion.Set(Emotion.Angry);
  → 自动: 步行速度增加 / 头部摇晃 / 表情变化 / 眨眼减少
```

## EmotionType 枚举

```csharp
public enum EmotionType
{
    Neutral, Happy, Angry, Sad, Afraid, Surprised, Disgusted
}
```

## 各情感效果

| 情感 | NavMesh速度 | 动画器触发器 | BlinkRate |
|------|------------|------------|-----------|
| `Neutral` | ×1.0 | `"Idle"` | `1.0` |
| `Happy` | ×1.0 | `"Happy"` | `1.0` |
| `Angry` | ×1.5 | `"Angry"` | `0.3`（减少） |
| `Sad` | ×0.7 | `"Sad"` | `0.8` |
| `Afraid` | ×1.3 | `"Afraid"` | `1.5`（增加） |

---

## 示例

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

    public void OnTalkWithPlayer()
    {
        float attitude = _memory.GetAttitude();

        if (attitude < -8f)      _emotion.Set(EmotionType.Angry);
        else if (attitude < -2f) _emotion.Set(EmotionType.Disgusted);
        else if (attitude > 8f)  _emotion.Set(EmotionType.Happy);
        else                     _emotion.Set(EmotionType.Neutral);
    }
}
```
