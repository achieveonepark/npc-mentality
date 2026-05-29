---
id: emotion
title: Emotion-Based Animation System
sidebar_label: 4. Emotion Animation
sidebar_position: 4
---

# Emotion-Based Animation System

One line sets movement speed, animator triggers, and blink rate automatically.

## Overview

```
Before:
  anim.Play("Idle");

NPC Mentality:
  emotion.Set(Emotion.Angry);
  → Auto: walk speed increase / head shake / expression change / blink decrease
```

## Component

`EmotionController` — MonoBehaviour.

| Field | Type | Description |
|-------|------|-------------|
| `Animator` | `Animator` | NPC animator |
| `Agent` | `NavMeshAgent` | Movement speed control (optional) |
| `CurrentEmotion` | `EmotionType` | Current emotion (read-only) |

---

## EmotionType Enum

```csharp
public enum EmotionType
{
    Neutral,
    Happy,
    Angry,
    Sad,
    Afraid,
    Surprised,
    Disgusted
}
```

---

## API

### Set

```csharp
void Set(EmotionType emotion)
```

Sets the emotion and applies effects immediately.

```csharp
emotion.Set(EmotionType.Angry);
emotion.Set(EmotionType.Happy);
emotion.Set(EmotionType.Neutral); // Reset
```

---

## Effects by Emotion

| Emotion | NavMesh Speed | Animator Trigger | BlinkRate |
|---------|---------------|-----------------|-----------|
| `Neutral` | ×1.0 (default) | `"Idle"` | `1.0` |
| `Happy` | ×1.0 | `"Happy"` | `1.0` |
| `Angry` | ×1.5 | `"Angry"` | `0.3` (decreased) |
| `Sad` | ×0.7 | `"Sad"` | `0.8` |
| `Afraid` | ×1.3 | `"Afraid"` | `1.5` (increased) |

> **Animator Parameters**: `BlinkRate` (Float), and a Trigger parameter for each emotion.

---

## Animator Setup

```
Parameters:
  BlinkRate   [Float]    — Connect to blink speed Blend Tree
  Idle        [Trigger]
  Happy       [Trigger]
  Angry       [Trigger]
  Sad         [Trigger]
  Afraid      [Trigger]
```

---

## Example

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

        if (attitude < -8f)
            _emotion.Set(EmotionType.Angry);
        else if (attitude < -2f)
            _emotion.Set(EmotionType.Disgusted);
        else if (attitude > 8f)
            _emotion.Set(EmotionType.Happy);
        else
            _emotion.Set(EmotionType.Neutral);
    }

    public void OnCombatStart() => _emotion.Set(EmotionType.Afraid);
    public void OnCombatEnd()   => _emotion.Set(EmotionType.Neutral);
}
```
