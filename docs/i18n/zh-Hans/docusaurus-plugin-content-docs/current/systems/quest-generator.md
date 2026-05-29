---
id: quest-generator
title: AI任务生成器
sidebar_label: 5. AI任务生成器
sidebar_position: 5
---

# AI任务生成器

分析玩家行为模式，自动生成符合情境的任务。

## 概述

```
玩家:
  - 经常去森林
  - 杀了很多狼

生成:
  "森林附近出现了奇怪的狼。"

比随机任务自然得多。
```

## API

### GenerateQuest

```csharp
QuestData GenerateQuest(EventType trigger, string subject = "creature")
```

```csharp
QuestData q = generator.GenerateQuest(EventType.Kill, "wolf");
// → "森林附近出现了奇怪的狼。"
```

### OnQuestGenerated 事件

```csharp
generator.OnQuestGenerated += quest =>
{
    QuestUI.Show(quest.Title, quest.Description);
    QuestLog.Add(quest);
};
```

---

## 默认模板

| EventType | 生成示例 |
|-----------|---------|
| `Kill` | "森林附近出现了奇怪的 \{subject\}。" |
| `Steal` | "商人区的货物消失了。" |
| `Help` | "再次需要援手。" |
| `Rescue` | "有人身处险境。" |

---

## 示例

```csharp
using NpcMentality;

public class PlayerBehaviorTracker : MonoBehaviour
{
    public AIQuestGenerator questGenerator;
    private int _wolfKillCount = 0;

    public void OnWolfKilled()
    {
        _wolfKillCount++;
        if (_wolfKillCount >= 5)
        {
            questGenerator.GenerateQuest(EventType.Kill, "wolf");
            _wolfKillCount = 0;
        }
    }
}
```
