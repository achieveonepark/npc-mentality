---
id: quest-generator
title: AI Quest Generator
sidebar_label: 5. AI Quest Generator
sidebar_position: 5
---

# AI Quest Generator

Analyzes player behavior patterns to automatically generate contextual quests.

## Overview

```
Player:
  - Frequently visits the forest
  - Kills many wolves

Generated:
  "Strange wolves have appeared near the forest."

Far more natural than random quests.
```

## Component

`AIQuestGenerator` — MonoBehaviour.

---

## API

### GenerateQuest

```csharp
QuestData GenerateQuest(EventType trigger, string subject = "creature")
```

| Parameter | Description |
|-----------|-------------|
| `trigger` | Quest trigger action type |
| `subject` | Quest subject (e.g., `"wolf"`, `"merchant"`) |

```csharp
QuestData q = generator.GenerateQuest(EventType.Kill, "wolf");
// → "Strange wolves have appeared near the forest."

QuestData q2 = generator.GenerateQuest(EventType.Steal, "merchant");
// → "Someone has been raiding the merchant district."
```

### OnQuestGenerated Event

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
    public float GeneratedAt;
}
```

---

## Default Templates

| EventType | Generated Example |
|-----------|------------------|
| `Kill` | "Strange {subject} have appeared near the forest." |
| `Steal` | "Items have gone missing in the merchant district." |
| `Help` | "A helping hand is needed again." |
| `Rescue` | "People are in danger." |

Modify the `QuestTemplates` dictionary in the Inspector to add custom templates.

---

## Example

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

    private void Start()
    {
        questGenerator.OnQuestGenerated += quest =>
            Debug.Log($"[New Quest] {quest.Title}");
    }
}
```
